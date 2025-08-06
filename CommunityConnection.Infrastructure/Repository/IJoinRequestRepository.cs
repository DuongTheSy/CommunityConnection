using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IJoinRequestRepository
    {
        Task AddAsync(JoinRequest request);
        Task<bool> ExistsAsync(long userId, long communityId);
        Task<List<JoinRequest>> GetJoinRequestsByCommunityIdAsync(long communityId);
        Task<bool> DeleteJoinRequestAsync(long id);
        Task<JoinRequest?> GetJoinRequestByIdAsync(long id);
    }

}
