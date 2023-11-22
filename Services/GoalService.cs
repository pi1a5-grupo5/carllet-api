using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

namespace Services;

public class GoalService : IGoalService
{
    private readonly CarlletDbContext _dbContext;

    public GoalService(CarlletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Goal> Create(Goal goal)
    {
        var createdGoal = _dbContext.Goals.Add(goal);
        if (createdGoal == null)
        {
            return null;
        }
        _dbContext.SaveChanges();

        return goal;
    }

    public async Task<Goal> Delete(Guid GoalId)
    {
        var goal = _dbContext.Goals.Find(GoalId);
        if (goal == null)
        {
            return null;
        }

        _dbContext.Goals.Remove(goal);
        _dbContext.SaveChanges();
        return goal;
    }

    public async Task<Goal> GetGoal(Guid GoalId)
    {
        return _dbContext.Goals.Find(GoalId);
    }

    public async Task<Goal> GetGoalByUser(Guid userId)
    {
        var goal = _dbContext.Goals.Where(g => g.UserId == userId).OrderByDescending(g => g.GoalInsertionDate).FirstOrDefault();
        if (goal == null)
        {
            return null;
        }
        return goal;
    }

    public async Task<Goal> Update(Guid GoalId, Goal goal)
    {
        var existingGoal = await _dbContext.Goals.FindAsync(GoalId);
        if (existingGoal == null)
        {
            throw new Exception("Goal not found");
        }

        _dbContext.Entry(existingGoal).CurrentValues.SetValues(goal);
        await _dbContext.SaveChangesAsync();

        return existingGoal;
    }
}
