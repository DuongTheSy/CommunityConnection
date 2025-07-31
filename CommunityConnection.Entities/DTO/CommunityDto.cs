using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class CommunityDto
    {
        public long Id { get; set; }

        public string CommunityName { get; set; } = null!;

        public string? Description { get; set; }

        public int? AccessStatus { get; set; }

        public int? MemberCount { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? SkillLevel { get; set; }

        public ChannelDto? DefaultChannel { get; set; } // nếu muốn trả luôn kênh mặc định
    }

}
