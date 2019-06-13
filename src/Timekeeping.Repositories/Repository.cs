using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Timekeeping.Repositories.Abstractions;

namespace Timekeeping.Repositories
{
    public abstract class Repository<TDbContext> : IRepository
        where TDbContext : DbContext
    {
        protected DbContext DbContext { get; }

        protected Repository(TDbContext context)
        {
            DbContext = context;
        }

        #region Private

        private void Detach<TModel>(Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel
        {
            var current = DbContext.Set<TModel>().Local.FirstOrDefault(predicate.Compile());
            if (current != null)
                DbContext.Entry(current).State = EntityState.Detached;
        }

        private IQueryable<TModel> GetQueryable<TModel>(params string[] expands)
            where TModel : class, IModel
        {
            var query = DbContext.Set<TModel>().AsNoTracking();
            return query.Inflate(expands);
        }

        private IEnumerable<(string Name, Type Type)> GetPkByModelType(Type modelType)
        {
            return DbContext.Model.FindEntityType(modelType)?.GetProperties()
                .Where(x => x.IsKey())
                .Select(x => (x.Name, Type: x.PropertyInfo.PropertyType)) ?? new List<(string Name, Type Type)>();
        }

        #endregion

        public Task<IQueryable<TModel>> GetQueryableAsync<TModel>(params string[] expands)
            where TModel : class, IModel
        {
            return Task.Run(() => GetQueryable<TModel>(expands));
        }

        public Task<IEnumerable<TModel>> GetAllAsync<TModel>(params string[] expands)
            where TModel : class, IModel
        {
            return Task.Run(() => GetQueryable<TModel>(expands).AsEnumerable());
        }

        public Task<TModel> GetAsync<TModel>(Expression<Func<TModel, bool>> predicate, params string[] expands)
            where TModel : class, IModel
        {
            return DbContext.Set<TModel>().AsNoTracking().Where(predicate).Inflate(expands).SingleOrDefaultAsync();
        }

        public async Task AddAsync<TModel>(TModel model, Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel
        {
            Detach(predicate);
            await DbContext.Set<TModel>().AddAsync(model);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync<TModel>(TModel model, Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel
        {
            Detach(predicate);
            DbContext.Set<TModel>().Update(model);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<TModel>(TModel model, Expression<Func<TModel, bool>> predicate)
            where TModel : class, IModel
        {
            Detach(predicate);

            if (model == null)
                return;

            DbContext.Set<TModel>().Remove(model);
            await DbContext.SaveChangesAsync();
        }
    }
}

public static class QueryInflaterExtension
{
    public static IQueryable<T> Inflate<T>(this IQueryable<T> query, IEnumerable<string> expands) where T : class
    {
        if (expands?.Any() != true)
            return query;

        foreach (var expand in expands)
        {
            var parts = expand.Split('.');
            var properties =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.FlattenHierarchy |
                                        BindingFlags.Instance);
            InflateProperties(ref query, parts, properties, expand);
        }

        return query;
    }

    private static void InflateProperties<T>(ref IQueryable<T> query, string[] parts, PropertyInfo[] properties,
        string expand) where T : class
    {
        for (var i = 0; i < parts.Length; i++)
        {
            var property = properties.FirstOrDefault(x =>
                string.Compare(x.Name, parts[i], StringComparison.OrdinalIgnoreCase) == 0);
            if (property == null)
                break;

            if (InflateProperty(ref query, parts, ref properties, expand, property, i))
                break;
        }
    }

    private static bool InflateProperty<T>(ref IQueryable<T> query, string[] parts, ref PropertyInfo[] properties,
        string expand,
        PropertyInfo property, int i) where T : class
    {
        var type = property.PropertyType;
        if (type.IsArray && type.GetElementType()?.IsValueType == true)
            return false;

        if (IsList(property.PropertyType) && IncludeList(ref query, parts, ref properties, expand, property, i))
            return true;

        return IncludeClass(ref query, parts, ref properties, expand, i, property);
    }

    private static bool IsList(Type type)
    {
        return type.IsGenericType && typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition());
    }

    private static bool IncludeClass<T>(ref IQueryable<T> query, IReadOnlyCollection<string> parts, ref PropertyInfo[] properties,
        string expand,
        int i, PropertyInfo property) where T : class
    {
        if (i == parts.Count - 1 &&
            property.PropertyType.IsClass &&
            property.PropertyType != typeof(string))
        {
            query = query.Include(expand);
            return true;
        }

        var propertyType = property.PropertyType;

        if (IsList(propertyType))
            propertyType = propertyType.GetGenericArguments().First();

        properties =
            propertyType.GetProperties(
                BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
        return false;
    }

    private static bool IncludeList<T>(ref IQueryable<T> query, string[] parts, ref PropertyInfo[] properties,
        string expand,
        PropertyInfo property, int i) where T : class
    {
        var prop = property.PropertyType.GenericTypeArguments.FirstOrDefault(a => a.IsClass);

        if (prop == null)
            return true;

        if (i == parts.Length - 1)
        {
            query = query.Include(expand);
            return true;
        }

        properties =
            prop.GetProperties(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
        return false;
    }
}
