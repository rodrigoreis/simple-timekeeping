using System;

namespace Timekeeping.Services.Abstractions.Dtos
{
    public class TimeEntryDto : IDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public ProjectDto Project { get; set; }
        public UserDto User { get; set; }
    }
}
