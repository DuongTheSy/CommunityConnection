using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class SubGoalDto
    {
        public long user_id { get; set; }
        public long Id { get; set; }
        public long GoalId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? OrderIndex { get; set; }
    }
}
