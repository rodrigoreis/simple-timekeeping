using System.Collections.Generic;

namespace Timekeeping.Repositories.Abstractions.Models
{
    public class ProjectModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobJourneyCharge { get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
        public virtual ICollection<TimeEntryModel> TimeEntries { get; set; }
    }
}
