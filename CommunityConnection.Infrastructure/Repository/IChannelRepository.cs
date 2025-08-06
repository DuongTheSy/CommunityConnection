using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IChannelRepository
    {
        Task<Channel> CreateChannelAsync(Channel channel);
        Task AddChannelMemberAsync(ChannelMember member);
        Task<ListChannelResponse> GetChannelsOfCommunityByUserAsync(long userId, long communityId);

        Task<CommunityMember?> GetCommunityOwnerAsync(long communityId);
        Task AddChannelAsync(Channel channel);
        Task AddChannelMembersAsync(List<ChannelMember> members);
        Task<List<Channel>> GetChannelsByCommunityAsync(long communityId);
        Task<Channel?> GetDefaultChannelAsync(long communityId);
        Task<bool> IsUserInChannelAsync(long channelId, long userId);
    }   
}
