using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IConnectionRequestService
    {
        // gửi yêu cầu kết nối 
        Task<ApiResponse<bool>> SendConnectionRequestAsync(long senderUserId, SendConnectionRequestDto dto);

        // Xác nhận yêu cầu kết nối với người dùng khác
        Task<ApiResponse<bool>> HandleConnectionRequestAsync(long userId, HandleConnectionRequestDto dto);

        // tạo room chat 2 người dùng từ kênh với communityId == null
        Task<ApiResponse<bool>> CreateChatRoomFromChannel(long userId1, long userId2);
        // lấy danh sách yêu cầu kết nối đã nhận
        Task<List<ConnectionRequest>> GetReceivedRequestsAsync(long receiverUserId);
        // lấy danh sách yêu cầu kết nối đã gửi
        Task<List<ConnectionRequest>> GetSentRequestsAsync(long SenderUserId);
        // lấy danh sách bạn bè
        Task<List<ConnectionRequest>> GetFriendsList(long SenderUserId);

    }
}
