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
    public class ConnectionRequestService : IConnectionRequestService
    {
        private readonly IConnectionRequestRepository _repo;
        private readonly IChannelRepository _channelRepository;

        public ConnectionRequestService(IConnectionRequestRepository repo, IChannelRepository channelRepository)
        {
            _repo = repo;
            _channelRepository = channelRepository;
        }

        public async Task<ApiResponse<bool>> SendConnectionRequestAsync(long senderUserId, SendConnectionRequestDto dto)
        {
            if (senderUserId == dto.ReceiverUserId)
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Không thể gửi yêu cầu kết nối cho chính mình",
                    data = false
                };
            }

            var exists = await _repo.ExistsPendingRequestAsync(senderUserId, dto.ReceiverUserId);
            if (exists)
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Yêu cầu kết nối đã được gửi và đang chờ xử lý",
                    data = false
                };
            }

            var request = new ConnectionRequest
            {
                SenderUserId = senderUserId,
                ReceiverUserId = dto.ReceiverUserId,
                Message = dto.Message,
                Status = 0, // Pending
                CreatedAt = DateTime.UtcNow
            };

            var result = await _repo.CreateConnectionRequestAsync(request);

            return new ApiResponse<bool>
            {
                status = result,
                message = result ? "Gửi yêu cầu kết nối thành công" : "Gửi yêu cầu thất bại",
                data = result
            };
        }
        public async Task<ApiResponse<bool>> HandleConnectionRequestAsync(long userId, HandleConnectionRequestDto dto)
        {
            var request = await _repo.GetByIdAsync(dto.ConnectionRequestId);
            if (request == null)
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Yêu cầu kết nối không tồn tại",
                    data = false
                };
            }

            if (request.ReceiverUserId != userId)
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Bạn không có quyền xử lý yêu cầu này",
                    data = false
                };
            }

            if (request.Status != null && request.Status != 0)
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Yêu cầu này đã được xử lý",
                    data = false
                };
            }

            request.Status = dto.IsApproved ? 1 : -1; // 1: chấp nhận, -1: từ chối
            request.UpdatedAt = DateTime.UtcNow;

            var result = await _repo.UpdateConnectionRequestAsync(request);
            if (result && dto.IsApproved)
            {
                // Tạo room chat nếu yêu cầu được chấp nhận
                await CreateChatRoomFromChannel(request.SenderUserId, request.ReceiverUserId);
            }

            return new ApiResponse<bool>
            {
                status = result,
                message = result ? "Đã xử lý yêu cầu kết nối" : "Xử lý thất bại",
                data = result
            };
        }

        public async Task<ApiResponse<bool>> CreateChatRoomFromChannel(long userId1, long userId2)
        {
            var channel = new Channel
            {
                ChannelName = $"{userId1} + {userId2}",
                Description = "RoomChat",
                CommunityId = null
            };
            var newChannel = await _channelRepository.CreateChannelAsync(channel);
            
            var members = new List<ChannelMember>
            {
                new ChannelMember
                {
                    ChannelId = newChannel.Id,
                    UserId = userId1,
                    Role = 5 
                },
                new ChannelMember
                {
                    ChannelId = newChannel.Id,
                    UserId = userId2,
                    Role = 5 
                }
            };
            await _channelRepository.AddChannelMembersAsync(members);
            return new ApiResponse<bool>
            {
                status = true,
                message = $"Tạo roomchat cho {userId1}, {userId2} thành công",
                data = true
            };
        }
        public async Task<List<ConnectionRequest>> GetReceivedRequestsAsync(long receiverUserId)
        {
            return await _repo.GetReceivedRequestsAsync(receiverUserId);
        }
    }

}
