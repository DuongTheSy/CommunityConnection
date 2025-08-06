using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class HandleJoinRequestDto
    {
        public long JoinRequestId { get; set; }
        public bool IsApproved { get; set; }
    }

}
