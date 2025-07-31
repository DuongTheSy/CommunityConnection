using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class UpdateSubGoalDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? OrderIndex { get; set; }
    }

}
