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

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public async Task<User> LoginAsync(LoginModel loginModel)
        {
            var user = _userRepository.Login(loginModel);
            if (user == null)
            {
                return null;
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
    }
}
