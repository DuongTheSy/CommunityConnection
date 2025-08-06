using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CommunityConnection.Infrastructure.Repository;

public class JoinRequestRepository : IJoinRequestRepository
{
    private readonly ThesisContext _db;

    public JoinRequestRepository(ThesisContext db)
    {
        _db = db;
    }

    public async Task AddAsync(JoinRequest request)
    {
        _db.JoinRequests.Add(request);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(long userId, long communityId)
    {
        return await _db.JoinRequests.AnyAsync(j => j.SenderUserId == userId && j.CommunityId == communityId);
    }

    // lấy danh sách
    public async Task<List<JoinRequest>> GetJoinRequestsByCommunityIdAsync(long communityId)
    {
        return await _db.JoinRequests
            .Where(j => j.CommunityId == communityId)
            .Include(j => j.SenderUser)
            .Include(j => j.ReceiverUser)
            .ToListAsync();
    }

    public async Task<JoinRequest?> GetJoinRequestByIdAsync(long id)
    {
        return await _db.JoinRequests
            .Include(j => j.SenderUser)
            .Include(j => j.ReceiverUser)
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    // xóa theo id
    public async Task<bool> DeleteJoinRequestAsync(long id)
    {
        var joinRequest = await _db.JoinRequests.FindAsync(id);
        if (joinRequest == null)
            return false;

        _db.JoinRequests.Remove(joinRequest);
        await _db.SaveChangesAsync();
        return true;
    }
}
