using CommunityConnection.Common;
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
        Task<ListChannelResponse> GetChannelsForUserAsync(long userId, long communityId);

    }
}
