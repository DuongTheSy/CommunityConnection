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

            return await _repository.CreateGoal(goal);
        }
        public async Task<IEnumerable<Goal>> GetGoalsByUser(long userId)
        {
            return await _repository.GetGoalsByUserId(userId);
        }
        public async Task<Goal?> UpdateGoal(long userId, UpdateGoalDto dto)
        {
            var existingGoal = await _repository.GetGoalById(dto.Id);

            if (existingGoal == null || existingGoal.UserId != userId)
                return null;

            if(dto.Description != null)
                existingGoal.Description = dto.Description;
            if(dto.Status != null)
                existingGoal.Status = dto.Status;
            if(dto.PriorityLevel != null)
                existingGoal.PriorityLevel = dto.PriorityLevel;

            existingGoal.GoalName = dto.GoalName;
            existingGoal.CompletionDate = dto.CompletionDate;

            await _repository.UpdateGoal(existingGoal);

            return existingGoal;
        }
        public async Task<bool> SoftDeleteGoal(long userId, long goalId)
        {
            var goal = await _repository.GetGoalById(goalId);

            if (goal == null || goal.UserId != userId)
                return false;

            goal.Status = 0;

            await _repository.UpdateGoal(goal);

            return true;
        }


    }
}
