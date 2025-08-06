using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IConnectionRequestService
    {
        Task<ApiResponse<bool>> SendConnectionRequestAsync(long senderUserId, SendConnectionRequestDto dto);
        Task<ApiResponse<bool>> HandleConnectionRequestAsync(long userId, HandleConnectionRequestDto dto);

    }
}
