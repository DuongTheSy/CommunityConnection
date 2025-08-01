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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User Login(LoginModel loginModel)
        {
            return _userRepository.Login(loginModel);
        }
    }
}
