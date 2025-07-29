using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface IGoalRepository
    {
        Task<Goal> CreateGoal(Goal goal);
        Task<IEnumerable<Goal>> GetGoalsByUserId(long userId);
        Task<Goal?> GetGoalById(long goalId);
        Task UpdateGoal(Goal goal); // cập nhật goal và xóa mềm

    }
}
