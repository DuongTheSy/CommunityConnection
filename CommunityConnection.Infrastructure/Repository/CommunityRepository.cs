using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommunityConnection.Entities.DTO;

namespace CommunityConnection.Infrastructure.Repository
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ThesisContext _db;

        public CommunityRepository(ThesisContext db)
        {
            _db = db;
        }

        public async Task<ListCommunityResponse> GetUserCommunitiesAsync(long userId)
        {
            var communities = await _db.CommunityMembers
                .Where(cm => cm.UserId == userId)
                .Include(cm => cm.Community)
                .Select(cm => new
                {
                    user_id = userId,
                    Community = new CommunityResponse
                    {
                        Id = cm.Community.Id,
                        Name = cm.Community.CommunityName,
                        Description = cm.Community.Description,
                        AccessStatus = cm.Community.AccessStatus // 0: private, 1: public
                    }
                }).ToListAsync();


            return new ListCommunityResponse
            {
                User_Id = userId,
                Communities = communities.Select(c => c.Community).ToList()
            };
        }
        public async Task<bool> IsMemberAsync(long userId, long communityId)
        {
            return await _db.CommunityMembers
                .AnyAsync(cm => cm.UserId == userId && cm.CommunityId == communityId);
        }

        public async Task AddMemberAsync(CommunityMember member)
        {
            _db.CommunityMembers.Add(member);
            await _db.SaveChangesAsync();
        }
        public async Task AddCommunityAsync(Community entity)
        {
            _db.Communities.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Community?> GetByIdAsync(long communityId)
        {
            return await _db.Communities.FindAsync(communityId);
        }

        public async Task<CommunityMember?> GetMemberAsync(long communityId, long userId)
        {
            return await _db.CommunityMembers
                .FirstOrDefaultAsync(cm => cm.CommunityId == communityId && cm.UserId == userId);
        }

        public async Task UpdateAsync(Community community)
        {
            _db.Communities.Update(community);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Community community)
        {
            _db.Communities.Remove(community);
            await _db.SaveChangesAsync();
        }
        public async Task<List<SuggestMentor>> GetCommunitiesWithRole2MembersAsync()
        {
            var result = await _db.CommunityMembers
                .Where(cm => cm.Role == 2) // quản trị viên
                .Select(cm => new SuggestMentor
                {
                    CommunityId = cm.Community.Id,
                    CommunityName = cm.Community.CommunityName,
                    MemberUserId = cm.UserId,
                    MemberRole = cm.Role ?? 0,
                    Description = cm.Community.Description
                })
                .ToListAsync();

            return result;
        }
        public async Task<bool> UpdateMemberRoleAsync(long communityId, long userId, int newRole)
        {
            var member = await GetMemberAsync(communityId, userId);
            if (member == null)
                return false;

            member.Role = newRole;
            await _db.SaveChangesAsync();
            return true;
        }

    }
}
