using Domain.Entities;

namespace Application.ViewModels
{
    public class GoalResponse
    {
        public Guid GoalId { get; set; }
        public Guid UserId { get; set; }
        public double GoalValue { get; set; }
    }
}