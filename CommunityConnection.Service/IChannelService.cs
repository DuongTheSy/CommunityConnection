using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IChannelService
    {
        Task<ChannelDto> CreateChannelAsync(long userId, ChannelCreateDto dto);
        Task<ListChannelResponse> GetChannelsForUserAsync(long idToken, long communityId);
    }

}
