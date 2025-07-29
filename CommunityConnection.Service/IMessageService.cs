using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IMessageService
    {
        Task<ApiResponse<MessageChannelResponse>> GetMessagesAsync(long userId,int communityId, int channelId);
    }
}
