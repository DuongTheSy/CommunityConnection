using CommunityConnection.Common;
using CommunityConnection.Entities;
using CommunityConnection.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityConnection.Service
{
    public interface ISubGoalService
    {
        Task<SubGoalDto> CreateSubGoalAsync(SubGoalViewModel request, long id_user);
        Task<List<SubGoalDto>> CreateSubGoalsWithActivitiesAsync(List<CreateSubGoalWithActivitiesRequest> requests);
        Task<bool> DeleteSubGoalAsync(long id);
        Task<IEnumerable<SubGoalDto>> GetSubGoalsByGoalIdAsync(long goalId);

    }
}
