using CommunityConnection.Common;
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
        public async Task<ApiResponse<MessageChannelResponse>> GetMessagesAsync(long userId,int communityId, int channelId)
        {
            try
            {
                var messages = await _repository.GetMessagesAsync(userId, communityId, channelId);
                return new ApiResponse<MessageChannelResponse>
                {
                    status = "true",
                    message = "Thành công",
                    data = new MessageChannelResponse
                    {
                        CommunityId = communityId,
                        ChannelId = channelId,
                        Messages = messages
                    }
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<MessageChannelResponse>
                {
                    status = "false",
                    message = ex + "",
                };
            }
        }

    }
}
