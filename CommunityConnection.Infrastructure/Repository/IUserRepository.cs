using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(int page, int pagesize);
        User? GetUserById(int id);
        public User Login(LoginModel loginModel);
        Task<List<User>> GetUsersByIdsAsync(List<long> userIds);

        // admin
        Task<User?> GetByIdAsync(long userId);
        Task UpdateAsync(User user);
    }
}
