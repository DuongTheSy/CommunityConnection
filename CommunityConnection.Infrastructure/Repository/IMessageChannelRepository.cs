using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IMessageChannelRepository
    { 
        Task<List<MessageResponse>> GetMessagesAsync(long userId, long channelId);
        Task<bool> IsChannelMemberAsync(long channelId, long userId);
        Task<Message> CreateMessageAsync(Message message);


    }
}
