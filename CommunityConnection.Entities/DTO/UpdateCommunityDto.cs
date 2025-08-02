using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class UpdateCommunityDto
    {
        public string? CommunityName { get; set; }
        public string? Description { get; set; }
        public int? AccessStatus { get; set; }
        public string? SkillLevel { get; set; }
    }
}
