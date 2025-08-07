using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities.DTO
{
    public class ScheduleNotificationRequest
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string ScheduledTimeIsoString { get; set; } = null!;
    }
}
