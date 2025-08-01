using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
namespace CommunityConnection.Infrastructure.Repository
{
    public class ReminderNotificationRepository : IReminderNotificationRepository
    {
        private readonly ThesisContext _db;

        public ReminderNotificationRepository(ThesisContext db)
        {
            _db = db;
        }

        public async Task<ReminderNotification?> GetByActivityIdAsync(long activityId)
        {
            return await _db.ReminderNotifications
                .FirstOrDefaultAsync(r => r.ActivityId == activityId);
        }

        public async Task<ReminderNotification> CreateAsync(ReminderNotification reminder)
        {
            _db.ReminderNotifications.Add(reminder);
            await _db.SaveChangesAsync();
            return reminder;
        }
    }

}
