using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IMessageService
    {
        Task<ApiResponse<MessageChannelResponse>> GetMessagesAsync(long userId, int channelId);
        Task<MessageDto> SendMessageAsync(long channelId, long userId, string content);
        // lấy tin nhắn của 2 người 
        Task<ApiResponse<MessageChannelResponse>> GetPrivateChatMessagesAsync(long userId1, long userId2);
    }
}
