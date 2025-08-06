using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class HandleConnectionRequestDto
    {
        public long ConnectionRequestId { get; set; }
        public bool IsApproved { get; set; } = true; // true: đồng ý, false: từ chối
    }
}
