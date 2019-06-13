using System.Collections.Generic;

namespace Timekeeping.Repositories.Abstractions.Models
{
    public class UserModel : IModel
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ProjectModel Project { get; set; }
        public virtual ICollection<TimeEntryModel> TimeEntries { get; set; }
    }
}
