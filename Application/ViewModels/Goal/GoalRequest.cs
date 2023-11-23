using Domain.Entities;

namespace Application.ViewModels
{
    public class GoalRequest
    {
        public Guid UserId { get; set; }
        public double GoalValue { get; set; }

    }
}