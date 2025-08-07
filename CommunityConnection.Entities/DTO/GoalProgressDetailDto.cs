using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class GoalProgressDetailDto
    {
        public string GoalName { get; set; } = null!;
        public DateTime? CompletionDate { get; set; }
        public int? Status { get; set; }
        public int CompletedSubGoals { get; set; }  // Số lượng đã hoàn thành
        public List<SubGoalProgressDto> SubGoals { get; set; } = new();
    }

}
