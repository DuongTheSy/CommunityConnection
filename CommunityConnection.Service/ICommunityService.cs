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
    public interface ICommunityService
    {
        Task<ApiResponse<ListCommunityResponse>> GetUserCommunities(long userId);
        Task<bool> JoinCommunityAsync(long userId, long communityId);
        Task<ApiResponse<CommunityDto>> CreateCommunityAsync(long userId, CommunityCreateDto dto);
        Task<ApiResponse<Community>> UpdateCommunityAsync(long communityId, long userId, UpdateCommunityDto dto);
        Task<ApiResponse<string>> DeleteCommunityAsync(long communityId, long userId);
    }
}
