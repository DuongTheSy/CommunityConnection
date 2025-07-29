using CommunityConnection.Common;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _repository;


        public CommunityService(ICommunityRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<ListCommunityResponse>> GetUserCommunities(long userId)
        {
            try
            {
                var list_communities = await _repository.GetUserCommunitiesAsync(userId);
                return new ApiResponse<ListCommunityResponse>
                {
                    status = true,
                    message = "Thành công",
                    data = list_communities
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ListCommunityResponse>
                {
                    status = false,
                    message = ex + "",
                };
            }
        }
        public async Task<ListChannelResponse> GetChannelsForUserAsync(long userId, long communityId)
        {
            return await _repository.GetChannelsOfCommunityByUserAsync(userId, communityId);
        }


    }
}
