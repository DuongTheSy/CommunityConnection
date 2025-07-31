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
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _repo;

        public ChannelService(IChannelRepository repo)
        {
            _repo = repo;
        }
        public async Task<ListChannelResponse> GetChannelsForUserAsync(long userId, long communityId)
        {
            return await _repo.GetChannelsOfCommunityByUserAsync(userId, communityId);
        }
        public async Task<ChannelDto> CreateChannelAsync(long userId, ChannelCreateDto dto)
        {
            var channel = new Channel
            {
                ChannelName = dto.ChannelName,
                Description = dto.Description,
                CommunityId = dto.CommunityId
            };

            var created = await _repo.CreateChannelAsync(channel);

            var member = new ChannelMember
            {
                ChannelId = created.Id,
                UserId = userId,
                Role = 0 // chủ sở hữu
            };

            await _repo.AddMemberAsync(member);

            return new ChannelDto
            {
                Id = created.Id,
                ChannelName = created.ChannelName,
                Description = created.Description,
                CommunityId = created.CommunityId
            };
        }
    }

}
