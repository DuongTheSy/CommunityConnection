using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Common
{
    public class MessageChannelResponse
    {
        public long ChannelId { get; set; }
        public List<MessageResponse> Messages { get; set; }
    }
    public class MessageDetail
    {
        public string content { get; set; }
        public DateTime? sentAt { get; set; }
    }

    public class MessageResponse
    {
        public long messageId { get; set; }
        public long userId { get; set; }
        public string senderName { get; set; }
        public int votesCount { get; set; }
        public MessageDetail message { get; set; }
    }

}
