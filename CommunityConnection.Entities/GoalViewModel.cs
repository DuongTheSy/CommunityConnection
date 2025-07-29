using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities
{
    public class GoalViewModel
    {
        public long UserId { get; set; }
        public string GoalName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? Status { get; set; }
        public string? PriorityLevel { get; set; }
    }
    public class CreateGoalDto
    {
        public string GoalName { get; set; } = null!;
        public DateTime? CompletionDate { get; set; }
    }
    public class UpdateGoalDto
    {
        public long Id { get; set; } // Id của goal cần sửa
        public string GoalName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? Status { get; set; }
        public string? PriorityLevel { get; set; }
    }
}
