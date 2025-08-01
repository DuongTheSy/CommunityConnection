using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
