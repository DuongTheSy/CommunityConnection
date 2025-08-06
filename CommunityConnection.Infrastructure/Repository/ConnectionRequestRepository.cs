using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.Infrastructure.Repository
{
    public class ConnectionRequestRepository : IConnectionRequestRepository
    {
        private readonly ThesisContext _db;

        public ConnectionRequestRepository(ThesisContext db)
        {
            _db = db;
        }

        public async Task<bool> ExistsPendingRequestAsync(long senderUserId, long receiverUserId)
        {
            return await _db.ConnectionRequests.AnyAsync(r =>
                r.SenderUserId == senderUserId &&
                r.ReceiverUserId == receiverUserId &&
                (r.Status == null || r.Status == 0)); // Đang chờ duyệt
        }

        public async Task<bool> CreateConnectionRequestAsync(ConnectionRequest request)
        {
            _db.ConnectionRequests.Add(request);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<ConnectionRequest?> GetByIdAsync(long id)
        {
            return await _db.ConnectionRequests.FindAsync(id);
        }

        public async Task<bool> UpdateConnectionRequestAsync(ConnectionRequest request)
        {
            _db.ConnectionRequests.Update(request);
            return await _db.SaveChangesAsync() > 0;
        }
        //public async Task<long?> GetRoomChatIdAsync(long user1Id, long user2Id)
        //{
        //    var result = await this.Set<PrivateRoomChat>()
        //        .FromSqlRaw("EXEC GetRoomChatId @User1Id = {0}, @User2Id = {1}", user1Id, user2Id)
        //        .ToListAsync();

        //    return result.FirstOrDefault()?.ChannelId;
        //}
    }

}
