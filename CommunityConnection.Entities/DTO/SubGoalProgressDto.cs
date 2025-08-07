using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class SubGoalProgressDto
    {
        public string Title { get; set; } = null!;
        public DateTime? CompletionDate { get; set; }
        public int? Status { get; set; }
    }
}
