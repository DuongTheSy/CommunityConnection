using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Common
{
    public class CreateSubGoalWithActivitiesRequest
    {
        public long GoalId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int OrderIndex { get; set; } = 0;
        public string? Activities { get; set; }

    }
}
