using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface ICommunityRepository
    {
        Task<ListCommunityResponse> GetUserCommunitiesAsync(long userId);
        Task<ListChannelResponse> GetChannelsOfCommunityByUserAsync(long userId, long communityId);


    }

}
