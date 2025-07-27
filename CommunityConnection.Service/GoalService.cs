using CommunityConnection.Entities;
using CommunityConnection.Infrastructure.Data;
using CommunityConnection.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _repository;

        public GoalService(IGoalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Goal> CreateGoalAsync(GoalViewModel dto)
        {
            var goal = new Goal
            {
                UserId = dto.UserId,
                GoalName = dto.GoalName,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                CompletionDate = dto.CompletionDate,
                Status = dto.Status,
                PriorityLevel = dto.PriorityLevel
            };

            return await _repository.CreateGoalAsync(goal);
        }
        public async Task<IEnumerable<Goal>> GetGoalsByUserIdAsync(long userId)
        {
            return await _repository.GetGoalsByUserIdAsync(userId);
        }
    }
}
