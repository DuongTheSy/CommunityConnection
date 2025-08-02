using CommunityConnection.Common;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class ActivityScheduleService : IActivityScheduleService
    {
        private readonly IActivityScheduleRepository _repository;
        private readonly IReminderNotificationRepository _repoRemind;

        public ActivityScheduleService(IActivityScheduleRepository repository, IReminderNotificationRepository repoRemind)
        {
            _repository = repository;
            _repoRemind = repoRemind;
        }

        public async Task<ApiResponse<ActivitySchedule>> CreateAsync(long userId, CreateActivityScheduleDto dto)
        {
            if(dto.Date <= DateTime.Now)
            {
                return (new ApiResponse<ActivitySchedule>
                {
                    status = false,
                    message = "Ngày bắt đầu không hợp lệ",
                    data = null
                });
            }

            // chưa bắt lỗi thời gian bắt đầu và kết thúc

            var schedule = new ActivitySchedule
            {
                UserId = userId,
                ActivityName = dto.ActivityName,
                Description = dto.Description,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = 1  
            };

            return (new ApiResponse<ActivitySchedule>
            {
                status = true,
                message = "Bạn đã thêm lịch trình thành công",
                data = await _repository.CreateAsync(schedule)
            });
        }
        public async Task<List<ActivitySchedule>> GetByUserIdAsync(long userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task<ApiResponse<ReminderNotification>> CreateRemindAsync(long userId, CreateReminderNotificationDto dto)
        {
            // Kiểm tra đã có nhắc nhở chưa
            var existing = await _repoRemind.GetByActivityIdAsync(dto.ActivityId);
            if (existing != null)
            {
                return new ApiResponse<ReminderNotification>
                {
                    status = false,
                    message = "Hoạt động này đã có nhắc nhở rồi.",
                    data = null
                };
            }

            var reminder = new ReminderNotification
            {
                ActivityId = dto.ActivityId,
                Content = dto.Content,
                UserId = userId,
                SendTime = dto.SendTime,
                IsRead = false
            };

            var created = await _repoRemind.CreateAsync(reminder);

            return new ApiResponse<ReminderNotification>
            {
                status = true,
                message = "Đã tạo nhắc nhở thành công",
                data = created
            };
        }

        public async Task<ApiResponse<ActivitySchedule>> UpdateAsync(long userId, long id, UpdateActivityScheduleDto dto)
        {
            var schedule = await _repository.GetByIdAsync(id);
            if (schedule == null)
                return (new ApiResponse<ActivitySchedule>
                {
                    status = false,
                    message = "Lịch học không tồn tại",
                    data = null
                });

            if (schedule.UserId != userId)
                return (new ApiResponse<ActivitySchedule>
                {
                    status = false,
                    message = "Bạn không có quyền sửa lịch học này",
                    data = null
                });

            if (dto.ActivityName != "")
                schedule.ActivityName = dto.ActivityName;

            if (dto.Description != "")
                schedule.Description = dto.Description;

            if (dto.Date == null)
                schedule.Date = dto.Date;

            if (dto.StartTime != "")
                schedule.StartTime = dto.StartTime;

            if (dto.EndTime != "")
                schedule.EndTime = dto.EndTime;

            if (dto.Status.HasValue)
                schedule.Status = dto.Status;

            await _repository.UpdateAsync(schedule);

            return (new ApiResponse<ActivitySchedule>
            {
                status = true,
                message = "Cập nhật lịch học thành công",
                data = schedule
            });
        }

        public async Task<ApiResponse<ReminderNotification>> DeleteReminderAsync(long id)
        {
            var reminder = await _repoRemind.GetByIdAsync(id);
            if (reminder == null)
            {
                return (new ApiResponse<ReminderNotification>
                {
                    status = false,
                    message = "Không tìm thấy nhắc nhở",
                    data = null
                });
            }

            await _repoRemind.DeleteAsync(reminder);
            return (new ApiResponse<ReminderNotification>
            {
                status = true,
                message = "Xóa nhắc nhở thành công",
                data = null
            });
        }


    }

}
