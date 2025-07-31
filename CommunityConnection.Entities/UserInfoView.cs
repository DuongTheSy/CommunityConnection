using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities
{
    public class UserInfoView
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string? GoalName { get; set; }
        public string? FieldName { get; set; }
        public string? CommunityName { get; set; }
        public int? Role { get; set; }
    }

}
