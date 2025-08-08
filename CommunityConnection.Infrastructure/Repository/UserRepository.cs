using CommunityConnection.Common.Helpers;
using CommunityConnection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
        public IEnumerable<User> GetAllUsers(int page, int pagesize)
        {
            if (page <= 0) page = 1;
            if (pagesize <= 0) pagesize = 5;

            return _db.Users
                      .OrderBy(u => u.Id) // Sắp xếp để phân trang ổn định
            .Skip((page - 1) * pagesize)
                      .Take(pagesize)
                      .ToList();
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
        public async Task<List<User>> GetUsersByIdsAsync(List<long> userIds)
        {
            return await _db.Users
                .Where(u => userIds.Contains(u.Id))
                .Include(u => u.Fields)
                .ToListAsync();

        }
        public async Task<User?> GetByIdAsync(long userId)
        {
            return await _db.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
