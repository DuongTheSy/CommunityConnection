using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class UserSearchResultDto
    {
        public long UserId { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public List<string> Goals { get; set; } = new();
    }


}
