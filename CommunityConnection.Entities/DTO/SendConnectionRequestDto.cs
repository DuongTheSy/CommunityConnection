using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class SendConnectionRequestDto
    {
        public long ReceiverUserId { get; set; }
        public string? Message { get; set; }
    }
}
