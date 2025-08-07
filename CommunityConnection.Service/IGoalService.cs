using CommunityConnection.Entities;
using CommunityConnection.Entities.DTO;
using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface IGoalService
    {
        Task<Goal> CreateGoalAsync(GoalViewModel dto);
        Task<IEnumerable<Goal>> GetGoalsByUser(long userId);
        Task<Goal?> UpdateGoal(long userId, UpdateGoalDto dto);
        Task<bool> SoftDeleteGoal(long userId, long goalId);
        Task<List<GoalProgressDetailDto>> GetUserGoalProgressAsync(long userId);
    }
}
