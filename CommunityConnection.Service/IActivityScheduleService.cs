using CommunityConnection.Common;
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
        Task<ApiResponse<ActivitySchedule>> CreateAsync(long userId, CreateActivityScheduleDto dto);
        Task<List<ActivitySchedule>> GetByUserIdAsync(long userId);
        Task<ApiResponse<ActivitySchedule>> UpdateAsync(long userId, long id, UpdateActivityScheduleDto dto);


        Task<ApiResponse<ReminderNotification>> CreateRemindAsync(long userId, CreateReminderNotificationDto dto);
        Task<ApiResponse<ReminderNotification>> DeleteReminderAsync(long id);


    }

}
