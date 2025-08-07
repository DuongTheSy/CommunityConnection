using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class SuggestMentor
    {
        public long CommunityId { get; set; }
        public string CommunityName { get; set; } = null!;
        public string? Description { get; set; }
        public long MemberUserId { get; set; }
        public int MemberRole { get; set; }
    }
}
