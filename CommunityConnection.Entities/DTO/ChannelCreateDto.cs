using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class ChannelCreateDto
    {
        public string ChannelName { get; set; } = null!;
        public string? Description { get; set; }
        public long CommunityId { get; set; }
    }
}
