using Domain.Entities;

namespace Domain.Interfaces;

public interface IGoalService
{
    Task<Goal> Create(Goal goal);
    Task<Goal> GetGoal(Guid GoalId);
    Task<Goal> Update(Guid GoalId, Goal goal);
    Task<Goal> Delete(Guid GoalId);
}
