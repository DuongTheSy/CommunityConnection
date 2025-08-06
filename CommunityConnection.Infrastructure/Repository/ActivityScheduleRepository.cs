using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CommunityConnection.Infrastructure.Repository
{
    public class ActivityScheduleRepository : IActivityScheduleRepository
    {
        private readonly ThesisContext _db;

        public ActivityScheduleRepository(ThesisContext db)
        {
            _db = db;
        }

        public async Task<ActivitySchedule> CreateAsync(ActivitySchedule schedule)
        {
            _db.ActivitySchedules.Add(schedule);
            await _db.SaveChangesAsync();
            return schedule;
        }
        public async Task<List<ActivitySchedule>> GetByUserIdAsync(long userId, DateTime date)
        {
            return await _db.ActivitySchedules
                .Where(a => a.UserId == userId && a.Status != 0 && a.Date.HasValue && a.Date.Value.Date == date.Date)
                .Include(s => s.ReminderNotification)
                .OrderBy(a => a.Date).ThenBy(a => a.StartTime)
                .ToListAsync();
        }

        public async Task UpdateAsync(ActivitySchedule schedule)
        {
            _db.ActivitySchedules.Update(schedule);
            await _db.SaveChangesAsync();
        }
        public async Task<ActivitySchedule?> GetByIdAsync(long id)
        {
            return await _db.ActivitySchedules
                .Include(x => x.ReminderNotification)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
