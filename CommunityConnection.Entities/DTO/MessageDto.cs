using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class MessageDto
    {
        public long Id { get; set; }
        public long ChannelId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? UserAvatarUrl { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? SentAt { get; set; }
    }

}
