using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
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
        Task<bool> IsMemberAsync(long userId, long communityId);
        Task AddMemberAsync(CommunityMember member);
        Task AddCommunityAsync(Community entity);
        #region Xóa community
        Task<Community?> GetByIdAsync(long communityId);
        Task<CommunityMember?> GetMemberAsync(long communityId, long userId);
        Task UpdateAsync(Community community);
        #endregion

        Task DeleteAsync(Community community);
        Task<List<SuggestMentor>> GetCommunitiesWithRole2MembersAsync();

        Task<bool> UpdateMemberRoleAsync(long communityId, long userId, int newRole);
    }

}
