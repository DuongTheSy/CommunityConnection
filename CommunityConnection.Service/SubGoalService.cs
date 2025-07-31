using CommunityConnection.Common;
using CommunityConnection.Entities;
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
    public class SubGoalService : ISubGoalService
    {
        private readonly ISubGoalRepository _repository;

        public SubGoalService(ISubGoalRepository repository)
        {
            _repository = repository;
        }

        public async Task<SubGoalDto> CreateSubGoalAsync(SubGoalViewModel request, long id_user)
        {
            var subGoal = new SubGoal
            {
                GoalId = request.goal_id,
                Title = request.Title,
                Description = request.Description,
                CompletionDate = request.CompletionDate,
                OrderIndex = request.OrderIndex
            };

            var created = await _repository.CreateAsync(subGoal);

            return new SubGoalDto
            { 
                
                Id = created.Id,
                user_id = id_user,
                GoalId = created.GoalId,
                Title = created.Title,
                Description = created.Description,
                CompletionDate = created.CompletionDate,
                OrderIndex = (int)created.OrderIndex
            };
        }
        public async Task<List<SubGoalDto>> CreateSubGoalsWithActivitiesAsync(List<CreateSubGoalWithActivitiesRequest> requests)
        {
            var result = new List<SubGoalDto>();

            foreach (var request in requests)
            {
                var subGoal = new SubGoal
                {
                    GoalId = request.GoalId,
                    Title = request.Title,
                    Description = request.Description,
                    CompletionDate = request.CompletionDate,
                    OrderIndex = request.OrderIndex,
                    SubGoalActivities = request.Activities.Select((act, index) => new SubGoalActivity
                    {
                        Activity = act,
                        OrderIndex = index + 1,
                        IsCompleted = false
                    }).ToList()
                };

                var created = await _repository.CreateFullAsync(subGoal);

                result.Add(new SubGoalDto
                {
                    Id = created.Id,
                    GoalId = created.GoalId,
                    Title = created.Title,
                    Description = created.Description,
                    CompletionDate = created.CompletionDate,
                    OrderIndex = created.OrderIndex,
                });
            }

            return result;
        }
        public async Task<bool> DeleteSubGoalAsync(long id)
        {
            return await _repository.DeleteSubGoalAsync(id);
        }

        public async Task<IEnumerable<SubGoalDto>> GetSubGoalsByGoalIdAsync(long goalId)
        {
            var subGoals = await _repository.GetSubGoalsByGoalIdAsync(goalId);

            // Chuyển về DTO nếu cần
            return subGoals.Select(s => new SubGoalDto
            {
                Id = s.Id,
                GoalId = s.GoalId,
                Title = s.Title,
                Description = s.Description,
                CompletionDate = s.CompletionDate,
                OrderIndex = s.OrderIndex,
            });
    }
        public async Task<SubGoal> UpdateSubGoalAsync(long id, UpdateSubGoalDto dto)
        {
            var subGoal = await _repository.GetByIdAsync(id);
            if (subGoal == null) return null;

            // Gán lại dữ liệu nếu có
            subGoal.Title = dto.Title ?? subGoal.Title;
            subGoal.Description = dto.Description ?? subGoal.Description;
            subGoal.CompletionDate = dto.CompletionDate ?? subGoal.CompletionDate;
            subGoal.OrderIndex = dto.OrderIndex ?? subGoal.OrderIndex;
            await _repository.UpdateAsync(subGoal);
            return subGoal;
        }



    }
}
