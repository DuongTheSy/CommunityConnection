using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityConnection.Entities.DTO;
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


        //public async Task<bool> IsFriend(long senderUserId, long receiverUserId)
        //{
        //    return await _db.ConnectionRequests.AnyAsync(r =>
        //        r.SenderUserId == senderUserId &&
        //        r.ReceiverUserId == receiverUserId &&
        //        (r.Status == null || r.Status == 0)); // Đang chờ duyệt
        //}

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
        public async Task<List<ConnectionRequest>> GetReceivedRequestsAsync(long receiverUserId)
        {
            return await _db.ConnectionRequests
                .Where(cr => cr.ReceiverUserId == receiverUserId && cr.Status == 0)
                .ToListAsync();
        }
        public async Task<List<ConnectionRequest>> GetSentRequestsAsync(long SenderUserId)
        {
            return await _db.ConnectionRequests
                .Where(cr => cr.SenderUserId == SenderUserId && cr.Status == 0)
                .ToListAsync();
        }
        public async Task<List<ConnectionRequest>> GetFriendsList(long userId)
        {
            return await _db.ConnectionRequests
                .Where(cr => cr.Status == 1 && (cr.SenderUserId == userId || cr.ReceiverUserId == userId))
                .Include(cr => cr.SenderUser)
                .Include(cr => cr.ReceiverUser)
                .ToListAsync();
        }

        public async Task<List<UserSearchResultDto>> SearchUsersAsync(string? keyword)
        {
            keyword = keyword?.ToLower();

            var query = _db.Users
                .Include(u => u.Goals)
                .Where(u => u.Status == true); // lọc người dùng đang hoạt động

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(u =>
                    u.FullName!.ToLower().Contains(keyword) ||
                    u.Username.ToLower().Contains(keyword) ||
                    u.Goals.Any(g => g.GoalName.ToLower().Contains(keyword))
                );
            }

            var result = await query
                .Select(u => new UserSearchResultDto
                {
                    UserId = u.Id,
                    FullName = u.FullName,
                    Username = u.Username,
                    Email = u.Email,
                    AvatarUrl = u.AvatarUrl,
                    Goals = u.Goals.Select(g => g.GoalName).ToList()
                })
                .ToListAsync();

            return result;
        }

    }

}
