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
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _db.Users.Select(u => new UserModel
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                AvatarUrl = u.AvatarUrl,
                Description = u.Description,
                Status = u.Status,
                CreatedAt = u.CreatedAt,
                DescriptionSkill = u.DescriptionSkill,
                RoleId = u.RoleId,
                Address = u.Address
            }).ToList();
        }

        public UserModel? GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public string Login(LoginModel loginModel)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == loginModel.UserName && u.Password == loginModel.Password);
            if (user == null)
            {
                return null; 
            }
            // Giả sử bạn có một phương thức để tạo token
            var jwtHelper = new JwtHelper();
            return jwtHelper.GenerateToken(user.Username, user.RoleId == 1 ? "User" : "Admin", user.Id);
        }
    }
}
