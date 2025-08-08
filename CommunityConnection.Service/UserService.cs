using CommunityConnection.Entities;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDeviceRepository _userDeviceRepository;

        public UserService(IUserRepository userRepository, IUserDeviceRepository userDeviceRepository)
        {
            _userRepository = userRepository;
            _userDeviceRepository = userDeviceRepository;
        }

        public IEnumerable<User> GetAllUsers(int page, int pagesize)
        {
            return _userRepository.GetAllUsers(page, pagesize);
        }

        public async Task<User> LoginAsync(LoginModel loginModel)
        {
            var user = _userRepository.Login(loginModel);
            if (user == null)
            {
                return null;
            }

            if(user.Status == false)
            {
                throw new UnauthorizedAccessException("Tài khoản bị khóa (Status) = False");
            }

            if (!string.IsNullOrEmpty(loginModel.DeviceToken))
            {
                var exists = await _userDeviceRepository.IsDeviceTokenExistAsync(loginModel.DeviceToken);
                if (!exists)
                {
                    await _userDeviceRepository.AddDeviceTokenAsync(user.Id, loginModel.DeviceToken);
                }
            }
            return user;
        }
        public async Task<List<string>> GetUserDeviceTokensAsync(long userId)
        {
            return await _userDeviceRepository.GetDeviceTokensByUserIdAsync(userId);
        }


        public async Task<bool> UpdateUserStatusAsync(long userId, bool status)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.Status = status;
            await _userRepository.UpdateAsync(user);

            return true;
        }
    }

}
