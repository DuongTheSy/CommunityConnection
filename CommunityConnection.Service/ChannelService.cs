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

        public async Task<ApiResponse<ChannelDto>> CreateChannelFromOperatorOrOwnerAsync(ChannelCreateDto dto, long userId)
        {
            if(userId == 0)
            {
                return new ApiResponse<ChannelDto>
                {
                    status = false,
                    message = "Người dùng không hợp lệ",
                    data = null
                };
            }

            var owner = await _repo.GetCommunityOwnerAsync(dto.CommunityId);
            if (owner == null)
            {
                return new ApiResponse<ChannelDto>
                {
                    status = false,
                    message = "Không tìm thấy chủ sở hữu cộng đồng",
                    data = null
                };
            }

            var channel = new Channel
            {
                CommunityId = dto.CommunityId,
                ChannelName = dto.ChannelName,
                Description = dto.Description
            };

            await _repo.AddChannelAsync(channel);

            var members = new List<ChannelMember>();

            // Thêm chủ sở hữu cộng đồng
            members.Add(new ChannelMember
            {
                ChannelId = channel.Id,
                UserId = owner.UserId,
                Role = 0
            });

            // Nếu người tạo khác chủ sở hữu, thêm người tạo với Role = 1
            if (userId != owner.UserId)
            {
                members.Add(new ChannelMember
                {
                    ChannelId = channel.Id,
                    UserId = userId,
                    Role = 0 // Role = 0 cho người tạo kênh
                });
            }

            await _repo.AddChannelMembersAsync(members);

            return new ApiResponse<ChannelDto>
            {
                status = true,
                message = "Tạo kênh thành công",
                data = new ChannelDto
                {
                    Id = channel.Id,
                    ChannelName = channel.ChannelName,
                    Description = channel.Description,
                    CommunityId = channel.CommunityId
                }
            };
        }

    }

}
