using CommunityConnection.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.Infrastructure.Repository
{
    public class SubGoalRepository : ISubGoalRepository
    {
        private readonly ThesisContext _db;

        public SubGoalRepository(ThesisContext context)
        {
            _db = context;
        }

        public async Task<SubGoal> CreateAsync(SubGoal subGoal)
        {
            _db.SubGoals.Add(subGoal);
            await _db.SaveChangesAsync();
            return subGoal;
        }
        public async Task<SubGoal> CreateFullAsync(SubGoal subGoalWithActivities)
        {
            _db.SubGoals.Add(subGoalWithActivities);
            await _db.SaveChangesAsync();
            return subGoalWithActivities;
        }

        public async Task<bool> DeleteSubGoalAsync(long id)
        {
            var subGoal = await _db.SubGoals.FindAsync(id);
            if (subGoal == null) return false;

            _db.SubGoals.Remove(subGoal);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<SubGoal>> GetSubGoalsByGoalIdAsync(long goalId)
        {
            return await _db.SubGoals
                                 .Where(s => s.GoalId == goalId)
                                 .OrderBy(s => s.OrderIndex)
                                 .ToListAsync();
        }

        public async Task<SubGoal?> GetByIdAsync(long id)
        {
            return await _db.SubGoals.FindAsync(id);
        }

        public async Task UpdateAsync(SubGoal subGoal)
        {
            _db.SubGoals.Update(subGoal);
            await _db.SaveChangesAsync();
        }

    }
}
