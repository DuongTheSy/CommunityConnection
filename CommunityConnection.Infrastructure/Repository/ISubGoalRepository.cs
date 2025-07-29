using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Infrastructure.Repository
{
    public interface ISubGoalRepository
    {
        Task<SubGoal> CreateAsync(SubGoal subGoal);
        Task<SubGoal> CreateFullAsync(SubGoal subGoalWithActivities);
        Task<bool> DeleteSubGoalAsync(long id);
        Task<IEnumerable<SubGoal>> GetSubGoalsByGoalIdAsync(long goalId);


    }
}
