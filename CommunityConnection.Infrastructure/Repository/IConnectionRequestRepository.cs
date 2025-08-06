using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IConnectionRequestRepository
    {
        Task<bool> ExistsPendingRequestAsync(long senderUserId, long receiverUserId);
        Task<bool> CreateConnectionRequestAsync(ConnectionRequest request);
        Task<ConnectionRequest?> GetByIdAsync(long id);
        Task<bool> UpdateConnectionRequestAsync(ConnectionRequest request);
    }

}
