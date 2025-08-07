using CommunityConnection.Common.Helpers;
using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ThesisContext _db;
        public UserRepository(ThesisContext _context) {
            _db = _context;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            throw new NotImplementedException();
        }
        public User Login(LoginModel loginModel)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == loginModel.UserName && u.Password == loginModel.Password);
            if (user == null)
            {
                return null; 
            }
            var jwtHelper = new JwtHelper();
            user.Token = jwtHelper.GenerateToken(user.Username, user.RoleId == 8 ? "Admin" : "User", user.Id);
            return user;
        }
    }
}
