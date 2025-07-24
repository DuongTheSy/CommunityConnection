using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
                RoleId = u.RoleId
            }).ToList();
        }
    }
}
