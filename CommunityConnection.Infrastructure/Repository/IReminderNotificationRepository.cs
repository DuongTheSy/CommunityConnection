using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IReminderNotificationRepository
    {
        Task<ReminderNotification?> GetByActivityIdAsync(long activityId);
        Task<ReminderNotification> CreateAsync(ReminderNotification reminder);
    }

}
