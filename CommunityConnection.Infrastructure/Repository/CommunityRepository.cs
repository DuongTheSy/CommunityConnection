using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

    }
}
