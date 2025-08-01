using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IActivityScheduleRepository
    {
        Task<ActivitySchedule> CreateAsync(ActivitySchedule schedule);
    }
}
