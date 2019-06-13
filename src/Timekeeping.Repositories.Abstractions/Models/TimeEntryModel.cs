using System;

namespace Timekeeping.Repositories.Abstractions.Models
{
    public class TimeEntryModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public ProjectModel Project { get; set; }
        public UserModel User { get; set; }
    }
}
