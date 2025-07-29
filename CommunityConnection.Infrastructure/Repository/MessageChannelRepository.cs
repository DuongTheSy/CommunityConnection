using CommunityConnection.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public class MessageChannelRepository : IMessageChannelRepository
    {
        private readonly ThesisContext _db;

        public MessageChannelRepository (ThesisContext context)
        {
            _db = context;
        }

        public async Task<List<MessageResponse>> GetMessagesAsync(long userId, int communityId, int channelId)
        {

            // Check if the user is a member of the community
            var isMember = await _db.ChannelMembers
                .Where(c => c.ChannelId == channelId && userId == c.UserId)
                .AnyAsync(m => m.UserId == userId);
            if(!isMember)
            {
                return null;
            }

            var messages = await _db.Messages
                .Where(m => m.ChannelId == channelId && m.Channel.CommunityId == communityId)
                .OrderBy(m => m.SentAt)
                .Select(m => new MessageResponse
                {
                    messageId = m.Id,
                    userId = m.UserId,
                    senderName = m.User.Username,
                    message = new MessageDetail
                    {
                        content = m.Content,
                        sentAt = m.SentAt,
                    }
                })
                .ToListAsync();

            return messages;
        }
    }
}
