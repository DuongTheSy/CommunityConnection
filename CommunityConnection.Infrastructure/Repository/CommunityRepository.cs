using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var communities = _db.CommunityMembers
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
                    }
                });


            return new ListCommunityResponse
            {
                User_Id = userId,
                Communities = communities.Select(c => c.Community).ToList()
            };
        }
        public async Task<ListChannelResponse> GetChannelsOfCommunityByUserAsync(long userId, long communityId)
        {
            var channels = _db.ChannelMembers
                 .Where(cm => cm.UserId == userId && cm.Channel.CommunityId == communityId)
                 .Select(cm => new ChannelResponse
                 {
                     Id = cm.Channel.Id,
                     Name = cm.Channel.ChannelName,
                     Description = cm.Channel.Description
                 })
                 .ToList(); // dùng sync

            var result = new ListChannelResponse
            {
                User_Id = userId,
                Community_Id = communityId,
                Channels = channels
            };

            return result;
        }


    }
}
