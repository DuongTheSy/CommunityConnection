using CommunityConnection.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CommunityConnection.Infrastructure.Repository
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ThesisContext _db;

        public ChannelRepository(ThesisContext db)
        {
            _db = db;
        }

        public async Task<Channel> CreateChannelAsync(Channel channel)
        {
            _db.Channels.Add(channel);
            await _db.SaveChangesAsync();
            return channel;
        }

        public async Task AddMemberAsync(ChannelMember member)
        {
            _db.ChannelMembers.Add(member);
            await _db.SaveChangesAsync();
        }
        public async Task<ListChannelResponse> GetChannelsOfCommunityByUserAsync(long userId, long communityId)
        {
            var channels = await _db.ChannelMembers
                 .Where(cm => cm.UserId == userId && cm.Channel.CommunityId == communityId)
                 .Select(cm => new ChannelResponse
                 {
                     Id = cm.Channel.Id,
                     Name = cm.Channel.ChannelName,
                     Description = cm.Channel.Description
                     
                 })
                 .ToListAsync();

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
