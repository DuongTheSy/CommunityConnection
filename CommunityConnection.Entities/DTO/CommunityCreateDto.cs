using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class CommunityCreateDto
    {
        public string CommunityName { get; set; } = null!;
        public string? Description { get; set; }
        public int? AccessStatus { get; set; } // 0: Public, 1: Private
        public string? SkillLevel { get; set; }
    }

}
