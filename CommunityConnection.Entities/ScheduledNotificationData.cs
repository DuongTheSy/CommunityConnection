using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Entities
{
    public class ScheduledNotificationData
    {
        public string DeviceToken { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string ScheduledTimeIsoString { get; set; } = null!;
    }


}
