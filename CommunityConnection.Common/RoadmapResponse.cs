using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Common
{
    public class RoadmapResponse
    {

        public string goal { get; set; } = string.Empty;

        public List<SubGoalWithActivitiesResponse> subGoals { get; set; } = new List<SubGoalWithActivitiesResponse>();
        public List<string> Notes { get; set; }
    }

    public class SubGoalWithActivitiesResponse
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? ExpectedDays { get; set; }
        public int OrderIndex { get; set; } = 0;
        public List<string> Activities { get; set; } = new List<string>();
    }
}
