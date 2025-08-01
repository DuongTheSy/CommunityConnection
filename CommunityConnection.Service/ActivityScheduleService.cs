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

        public ActivityScheduleService(IActivityScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActivitySchedule> CreateAsync(CreateActivityScheduleDto dto)
        {
            var schedule = new ActivitySchedule
            {
                UserId = dto.UserId,
                ActivityName = dto.ActivityName,
                Description = dto.Description,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = 1  
            };

            return await _repository.CreateAsync(schedule);
        }
    }

}
