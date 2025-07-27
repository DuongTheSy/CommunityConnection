using CommunityConnection.Entities;
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
        Task<IEnumerable<Goal>> GetGoalsByUserIdAsync(long userId);
    }
}
