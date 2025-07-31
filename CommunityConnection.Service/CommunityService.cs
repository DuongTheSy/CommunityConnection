using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.Service
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _repository;
        private readonly IChannelService _channelService;


        public CommunityService(ICommunityRepository repository, IChannelService channelService)
        {
            _repository = repository;
            _channelService = channelService;
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
        public async Task<bool> JoinCommunityAsync(long userId, long communityId)
        {
            var exists = await _repository.IsMemberAsync(userId, communityId);
            if (exists)
            {
                   return false; // Người dùng đã là thành viên của cộng đồng này
            }

            var member = new CommunityMember
            {
                UserId = userId,
                CommunityId = communityId,
                Role = 0 // mặc định là Member
            };

            await _repository.AddMemberAsync(member);
            return true;
        }
        public async Task<ApiResponse<CommunityDto>> CreateCommunityAsync(long userId, CommunityCreateDto dto)
        {
            var community = new Community
            {
                CommunityName = dto.CommunityName,
                Description = dto.Description,
                AccessStatus = dto.AccessStatus,
                SkillLevel = dto.SkillLevel,
                CreatedAt = DateTime.UtcNow,
                MemberCount = 1
            };

            await _repository.AddCommunityAsync(community);

            await _repository.AddMemberAsync(new CommunityMember
            {
                CommunityId = community.Id,
                UserId = userId,
                Role = 0     // Thêm người tạo vào CommunityMember (vai trò chủ sở hữu: 0)
            });

            // Tạo kênh mặc định
            var channelDto = await _channelService.CreateChannelAsync(userId, new ChannelCreateDto
            {
                CommunityId = community.Id,
                ChannelName = "Kênh chat chung",
                Description = "Kênh này để trao đổi chung trong cộng đồng"
            });


            var result = new CommunityDto
            {
                Id = community.Id,
                CommunityName = community.CommunityName,
                Description = community.Description,
                AccessStatus = community.AccessStatus,
                CreatedAt = community.CreatedAt,
                SkillLevel = community.SkillLevel,
                MemberCount = 1,
                DefaultChannel = channelDto // nếu channelService trả ApiResponse<ChannelDto>
            };

            return new ApiResponse<CommunityDto>
            {
                status = true,
                message = "Thành công",
                data = result
            };
        }

    }
}
