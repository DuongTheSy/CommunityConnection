using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnection.Infrastructure.Repository
{

    public class GoalRepository : IGoalRepository 
    {
        private readonly ThesisContext _context;

        public GoalRepository(ThesisContext context)
        {
            _context = context;
        }

        public async Task<Goal> CreateGoal(Goal goal)
        {
            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();
            return goal;
        }
        public async Task<IEnumerable<Goal>> GetGoalsByUserId(long userId)
        {
            return await _context.Goals
                .Where(g => g.UserId == userId && g.Status != 0)
                .ToListAsync();
        }
        public async Task<Goal?> GetGoalById(long goalId)
        {
            return await _context.Goals.FindAsync(goalId);
        }

        public async Task UpdateGoal(Goal goal)
        {
            _context.Goals.Update(goal);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Goal>> GetAllGoalsAsync()
        {
            return await _context.Goals.ToListAsync();
        }
    }
}
