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
        Task<Goal> CreateGoalAsync(Goal goal);
        Task<IEnumerable<Goal>> GetGoalsByUserIdAsync(long userId);
    }
}
