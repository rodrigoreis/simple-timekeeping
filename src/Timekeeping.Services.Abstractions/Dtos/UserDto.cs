﻿using System.Collections.Generic;

namespace Timekeeping.Services.Abstractions.Dtos
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ProjectDto Project { get; set; }
        public virtual ICollection<TimeEntryDto> TimeEntries { get; set; }
    }
}
