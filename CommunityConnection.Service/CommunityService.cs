using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.Service
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _repository;
        private readonly IChannelService _channelService;
        private readonly IChannelRepository _channelRepository;

        private readonly IJoinRequestRepository _joinRequestRepo;
        public CommunityService(ICommunityRepository repository, IChannelService channelService, IJoinRequestRepository joinRequestRepo, IChannelRepository channelRepository)
        {
            _repository = repository;
            _channelService = channelService;
            _joinRequestRepo = joinRequestRepo;
            _channelRepository = channelRepository;
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
            ApiResponse<ChannelDto> channelDto = await _channelService.CreateChannelFromOperatorOrOwnerAsync(new ChannelCreateDto
            {
                CommunityId = community.Id,
                ChannelName = "Kênh chat chung",
                Description = "Kênh này để trao đổi chung trong cộng đồng"
            },userId);


            var result = new CommunityDto
            {
                Id = community.Id,
                CommunityName = community.CommunityName,
                Description = community.Description,
                AccessStatus = community.AccessStatus,
                CreatedAt = community.CreatedAt,
                SkillLevel = community.SkillLevel,
                MemberCount = 1,
                DefaultChannel = channelDto.data // nếu channelService trả ApiResponse<ChannelDto>
            };

            return new ApiResponse<CommunityDto>
            {
                status = true,
                message = "Thành công",
                data = result
            };
        }
        public async Task<ApiResponse<Community>> UpdateCommunityAsync(long communityId, long userId, UpdateCommunityDto dto)
        {
            var member = await _repository.GetMemberAsync(communityId, userId);
            if (member == null || member.Role != 0)
            {
                return new ApiResponse<Community>
                {
                    status = false,
                    message = "Chỉ chủ sở hữu mới có quyền sửa cộng đồng",
                    data = null
                };
            }

            var community = await _repository.GetByIdAsync(communityId);
            if (community == null)
            {
                return new ApiResponse<Community>
                {
                    status = false,
                    message = "Cộng đồng không tồn tại",
                    data = null
                };
            }

            if (!string.IsNullOrWhiteSpace(dto.CommunityName)) community.CommunityName = dto.CommunityName;
            if (!string.IsNullOrWhiteSpace(dto.Description)) community.Description = dto.Description;
            if (dto.AccessStatus != null) community.AccessStatus = dto.AccessStatus;
            if (!string.IsNullOrWhiteSpace(dto.SkillLevel)) community.SkillLevel = dto.SkillLevel;

            await _repository.UpdateAsync(community);
            return new ApiResponse<Community>
            {
                status = true,
                message = "Cập nhật cộng đồng thành công",
                data = community
            };
        }
        public async Task<ApiResponse<string>> DeleteCommunityAsync(long communityId, long userId)
        {
            var member = await _repository.GetMemberAsync(communityId, userId);
            if (member == null || member.Role != 0)
            {
                return new ApiResponse<string>
                {
                    status = false,
                    message = "Chỉ chủ sở hữu mới có quyền xóa cộng đồng",
                    data = null
                };
            }

            var community = await _repository.GetByIdAsync(communityId);
            if (community == null)
            {
                return new ApiResponse<string>
                {
                    status = false,
                    message = "Cộng đồng không tồn tại",
                    data = null
                };
            }

            await _repository.DeleteAsync(community);

            return new ApiResponse<string>
            {
                status = true,
                message = "Xóa cộng đồng thành công",
                data = null
            };
        }

        public async Task<ApiResponse<string>> JoinCommunityAsyncv2(long userId, long communityId)
        {
            var community = await _repository.GetByIdAsync(communityId);
            if (community == null)
            {
                return new ApiResponse<string> { status = false, message = "Cộng đồng không tồn tại." };
            }

            var isMember = await _repository.IsMemberAsync(userId, communityId);
            if (isMember)
            {
                return new ApiResponse<string> { status = false, message = "Bạn đã là thành viên cộng đồng." };
            }

            if (community.AccessStatus == 1) // Công khai
            {
                // Thêm vào CommunityMember
                await _repository.AddMemberAsync(new CommunityMember
                {
                    UserId = userId,
                    CommunityId = communityId,
                    Role = 1 // Thành viên thông thường
                });

                // Lấy tất cả các kênh trong cộng đồng
                var channels = await _channelRepository.GetChannelsByCommunityAsync(communityId);

                // Thêm user vào tất cả các kênh
                var channelMembers = channels.Select(c => new ChannelMember
                {
                    UserId = userId,
                    ChannelId = c.Id,
                    Role = 1
                }).ToList();

                await _channelRepository.AddChannelMembersAsync(channelMembers);

                return new ApiResponse<string> { status = true, message = "Tham gia cộng đồng thành công." };
            }
            else // Riêng tư
            {
                bool exists = await _joinRequestRepo.ExistsAsync(userId, communityId);
                if (exists)
                {
                    return new ApiResponse<string> { status = false, message = "Bạn đã gửi yêu cầu tham gia." };
                }

                await _joinRequestRepo.AddAsync(new JoinRequest
                {
                    SenderUserId = userId,
                    CommunityId = communityId,
                    Message = "Xin hãy thu nhận em ạ =)))",
                    Status = 0, // hoặc 0
                    CreatedAt = DateTime.UtcNow
                });

                return new ApiResponse<string> { status = true, message = "Yêu cầu tham gia đã được gửi. Vui lòng chờ duyệt." };
            }
        }
        public async Task<ApiResponse<List<JoinRequest>>> GetJoinRequestsByCommunityIdAsync(long communityId, long userId)
        {
            var member = await _repository.GetMemberAsync(communityId, userId);
            if (member == null || ( member.Role != 0 && member.Role != 8)) // không là quản trị viên hoặc chủ sở hữu
            {
                return new ApiResponse<List<JoinRequest>>
                {
                    status = false,
                    message = "Chỉ quản trị viên mới xem được danh sách yêu cầu tham gia",
                    data = null
                };
            }
            return new ApiResponse<List<JoinRequest>>
            {
                status = true,
                message = "Lấy danh sách thành công",
                data = await _joinRequestRepo.GetJoinRequestsByCommunityIdAsync(communityId)
            };
        }

        public async Task<ApiResponse<bool>> HandleJoinRequestAsync(long idRequest, long userId, bool isApproved)
        {
            var joinRequest = await _joinRequestRepo.GetJoinRequestByIdAsync(idRequest);
            if (joinRequest == null)
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Yêu cầu tham gia không tồn tại",
                    data = false
                };
            }

            var user_handle = await _repository.GetMemberAsync(joinRequest.CommunityId, userId);
            if (user_handle == null || (user_handle.Role != 0 && user_handle.Role != 8)) // không là quản trị viên hoặc chủ sở hữu
            {
                return new ApiResponse<bool>
                {
                    status = false,
                    message = "Chỉ quản trị viên mới có quyền Reply yêu cầu tham gia",
                    data = false
                };
            }
            // thêm thành viên vào coocngj đồng riêng tư
            if (isApproved)
            {
                var exists = await _repository.IsMemberAsync(joinRequest.SenderUserId, joinRequest.CommunityId);
                if (exists)
                {
                    return new ApiResponse<bool>
                    {
                        status = false,
                        message = " Người dùng đã là thành viên của cộng đồng này",
                        data = false
                    };
                }
                // Thêm người dùng vào CommunityMember
                await _repository.AddMemberAsync(new CommunityMember
                {
                    UserId = joinRequest.SenderUserId,
                    CommunityId = joinRequest.CommunityId,
                    Role = 1
                });
                //// Thêm người dùng vào Channel Default
                var channelDefault = await _channelRepository.GetDefaultChannelAsync(joinRequest.CommunityId);
            
               await _channelRepository.AddChannelMemberAsync(new ChannelMember
                {
                    UserId = joinRequest.SenderUserId,
                    ChannelId = channelDefault.Id,
                    Role = 1
                });
            }

            bool result = await _joinRequestRepo.DeleteJoinRequestAsync(idRequest);
            return new ApiResponse<bool>
            {
                status = result,
                message = (result) ? "Thành công" : "Thất bại",
                data = result
            };
        }
        public async Task<bool> ChangeMemberRoleAsync(long actingUserId, long targetUserId, long communityId, int newRole)
        {
            var actingMember = await _repository.GetMemberAsync(communityId, actingUserId);
            var targetMember = await _repository.GetMemberAsync(communityId, targetUserId);

            if (actingMember == null || targetMember == null)
                return false;

            if (actingMember.Role == 0)
            {
                // Chủ sở hữu: toàn quyền
                return await _repository.UpdateMemberRoleAsync(communityId, targetUserId, newRole);
            }

            if (actingMember.Role == 8)
            {
                if (targetMember.Role == 0 || newRole == 0)
                    return false;

                return await _repository.UpdateMemberRoleAsync(communityId, targetUserId, newRole);
            }

            // Các role khác không được thay đổi
            return false;
        }
    }
}
