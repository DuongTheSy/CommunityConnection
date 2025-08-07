using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public class UserDeviceRepository : IUserDeviceRepository
    {
        private readonly ThesisContext _db;
        public UserDeviceRepository(ThesisContext context)
        {
            _db = context;
        }

        public async Task<bool> IsDeviceTokenExistAsync(string deviceToken)
        {
            return await _db.UserDevices.AnyAsync(d => d.DeviceToken == deviceToken);
        }

        public async Task AddDeviceTokenAsync(long userId, string deviceToken)
        {
            var userDevice = new UserDevice
            {
                UserId = userId,
                DeviceToken = deviceToken,
                CreateAt = DateTime.UtcNow
            };

            _db.UserDevices.Add(userDevice);
            await _db.SaveChangesAsync();
        }
        public async Task<List<string>> GetDeviceTokensByUserIdAsync(long userId)
        {
            return await _db.UserDevices
                .Where(d => d.UserId == userId && !string.IsNullOrEmpty(d.DeviceToken))
                .Select(d => d.DeviceToken!)
                .ToListAsync();
        }
    }

}
