using System.Collections.Generic;

namespace Timekeeping.Services.Abstractions.Dtos
{
    public class ProjectDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobJourneyCharge { get; set; }
        public virtual ICollection<UserDto> Users { get; set; }
        public virtual ICollection<TimeEntryDto> TimeEntries { get; set; }
    }
}
