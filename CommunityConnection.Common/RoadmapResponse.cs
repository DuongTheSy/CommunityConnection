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

        public List<SubGoal> subGoals { get; set; } = new List<SubGoal>();
        public List<string> Notes { get; set; }
    }

    public class SubGoal
    {
        public string title { get; set; } = string.Empty;
        public int expectedDays { get; set; }
        public string description { get; set; } = string.Empty;
        public List<string> activities { get; set; } = new List<string>();
    }
}
