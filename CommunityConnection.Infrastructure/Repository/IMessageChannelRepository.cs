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
        Task<List<MessageResponse>> GetMessagesAsync(int communityId, int channelId);
    }
}
