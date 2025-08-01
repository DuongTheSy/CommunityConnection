using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class CreateReminderNotificationDto
    {
        public long ActivityId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendTime { get; set; }
    }

}
