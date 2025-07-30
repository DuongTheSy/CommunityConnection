using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IMessageChannelRepository
    { 
        Task<List<MessageResponse>> GetMessagesAsync(long userId, int communityId, int channelId);
        Task<bool> IsChannelMemberAsync(long channelId, long userId);
        Task<Message> CreateMessageAsync(Message message);
    }
}
