using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageChannelRepository _repository;

        public MessageService(IMessageChannelRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse<MessageChannelResponse>> GetMessagesAsync(long userId, int channelId)
        {
            try
            {
                var messages = await _repository.GetMessagesAsync(userId, channelId);
                return new ApiResponse<MessageChannelResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = new MessageChannelResponse
                    {
                        ChannelId = channelId,
                        Messages = messages
                    }
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<MessageChannelResponse>
                {
                    status = false,
                    message = ex + "",
                };
            }
        }

        public async Task<MessageDto> SendMessageAsync(long channelId, long userId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Nội dung không được để trống.");

            var isMember = await _repository.IsChannelMemberAsync(channelId, userId);
            if (!isMember)
                throw new UnauthorizedAccessException("Bạn không phải là thành viên của kênh.");

            var message = new Message
            {
                ChannelId = channelId,
                UserId = userId,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            var saved = await _repository.CreateMessageAsync(message);

            return new MessageDto
            {
                Id = saved.Id,
                ChannelId = saved.ChannelId,
                UserId = saved.UserId,
                Content = saved.Content,
                SentAt = saved.SentAt
            };
        }

    }
}
