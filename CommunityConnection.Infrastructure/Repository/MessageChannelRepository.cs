using CommunityConnection.Common;
using CommunityConnection.Infrastructure.Data;
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

        public MessageChannelRepository(ThesisContext context)
        {
            _db = context;
        }

        public async Task<List<MessageResponse>> GetMessagesAsync(long userId, long channelId)
        {

            // Check if the user is a member of the community
            var isMember = await _db.ChannelMembers
                .Where(c => c.ChannelId == channelId && userId == c.UserId)
                .AnyAsync(m => m.UserId == userId);
            if (!isMember)
            {
                return null;
            }

            var messages = await _db.Messages
                .Where(m => m.ChannelId == channelId)
                .OrderBy(m => m.SentAt)
                .Select(m => new MessageResponse
                {
                    messageId = m.Id,
                    userId = m.UserId,
                    senderName = m.User.FullName,
                    message = new MessageDetail
                    {
                        content = m.Content,
                        sentAt = m.SentAt,
                    }
                })
                .ToListAsync();

            return messages;
        }

        public async Task<bool> IsChannelMemberAsync(long channelId, long userId)
        {
            return await _db.ChannelMembers
                .AnyAsync(cm => cm.ChannelId == channelId && cm.UserId == userId);
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            return message;
        }

    }
}
