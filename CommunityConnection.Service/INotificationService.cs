using CommunityConnection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface INotificationService
    {
        Task<string> SendNotificationAsync(string deviceToken, string title, string body);
        Task ScheduleNotificationAsync(ScheduledNotificationData data);
    }
}
