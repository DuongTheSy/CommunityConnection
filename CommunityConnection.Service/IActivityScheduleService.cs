using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IActivityScheduleService
    {
        Task<ActivitySchedule> CreateAsync(CreateActivityScheduleDto dto);
    }

}
