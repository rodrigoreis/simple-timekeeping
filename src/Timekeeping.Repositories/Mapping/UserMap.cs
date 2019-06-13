using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timekeeping.Repositories.Abstractions.Models;

namespace Timekeeping.Repositories.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder
                .ToTable("users");

            builder
                .Property(m => m.Id)
                .HasColumnName("user_id")
                .HasColumnType("INTEGER")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(m => m.ProjectId)
                .HasColumnName("project_id")
                .HasColumnType("INTEGER");

            builder
                .Property(m => m.Name)
                .HasColumnName("user_name")
                .HasColumnType("TEXT")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(m => m.Email)
                .HasColumnName("user_email")
                .HasColumnType("TEXT")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasMany(m => m.TimeEntries)
                .WithOne(n => n.User)
                .HasForeignKey(m => m.UserId)
                .HasConstraintName("fk_users_time_entries_user_id");

            builder
                .HasKey(m => m.Id)
                .HasName("pk_users_user_id");

            builder
                .HasIndex(m => m.Email)
                .HasName("ix_users_user_email");
        }
    }
}
