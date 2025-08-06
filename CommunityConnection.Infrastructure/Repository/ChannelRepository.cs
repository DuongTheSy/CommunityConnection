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


        // create 1 channel
        public async Task<Channel> CreateChannelAsync(Channel channel)
        {
            _db.Channels.Add(channel);
            await _db.SaveChangesAsync();
            return channel;
        }

        // create 1 channel member
        public async Task AddChannelMemberAsync(ChannelMember member)
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

        public async Task<CommunityMember?> GetCommunityOwnerAsync(long communityId)
        {
            return await _db.CommunityMembers
                .FirstOrDefaultAsync(cm => cm.CommunityId == communityId && cm.Role == 0);
        }

        public async Task AddChannelAsync(Channel channel)
        {
            _db.Channels.Add(channel);
            await _db.SaveChangesAsync();
        }

        public async Task AddChannelMembersAsync(List<ChannelMember> members)
        {
            _db.ChannelMembers.AddRange(members);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Channel>> GetChannelsByCommunityAsync(long communityId)
        {
            return await _db.Channels
                .Where(c => c.CommunityId == communityId)
                .ToListAsync();
        }
        public async Task<Channel?> GetDefaultChannelAsync(long communityId)
        {
            return await _db.Channels
                .Where(c => c.CommunityId == communityId && c.ChannelName == "Kênh chat chung")
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsUserInChannelAsync(long channelId, long userId)
        {
            return await _db.ChannelMembers
                .AnyAsync(cm => cm.ChannelId == channelId && cm.UserId == userId);
        }

        public async Task<long> GetPrivateRoomChannelIdAsync(long user1Id, long user2Id)
        {
            var privateChannel = await _db.Channels
                .Where(c => c.CommunityId == null)
                .Where(c =>
                    c.ChannelMembers.Any(cm => cm.UserId == user1Id) &&
                    c.ChannelMembers.Any(cm => cm.UserId == user2Id)
                )
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            return privateChannel;
        }
    }
}
