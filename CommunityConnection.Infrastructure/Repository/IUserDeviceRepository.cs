using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IUserDeviceRepository
    {
        Task<bool> IsDeviceTokenExistAsync(string deviceToken);
        Task AddDeviceTokenAsync(long userId, string deviceToken);
        Task<List<string>> GetDeviceTokensByUserIdAsync(long userId);

    }
}
