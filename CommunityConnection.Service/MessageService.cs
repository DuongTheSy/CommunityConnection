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
        private readonly IChannelRepository _channelRepository;

        public MessageService(IMessageChannelRepository repository, IChannelRepository channelRepository)
        {
            _repository = repository;
            _channelRepository = channelRepository;
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
        public async Task<ApiResponse<MessageChannelResponse>> GetPrivateChatMessagesAsync(long userId1, long userId2)
        {
            try
            {
                var channelId = await _channelRepository.GetPrivateRoomChannelIdAsync(userId1, userId2);
                var messages = await _repository.GetMessagesAsync(userId1, channelId);
                return new ApiResponse<MessageChannelResponse>
                {
                    status = true,
                    message = "Lấy tin nhắn thành công",
                    data = new MessageChannelResponse
                    {
                        ChannelId = channelId,
                        Messages = messages
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<MessageChannelResponse>
                {
                    status = false,
                    message = ex + "",
                };
            }
        }
    }
}
