using Domain.Entities;

namespace Application.Controllers
{
    public class GoalRequest
    {
        public Guid UserId { get; set; }
        public double GoalValue { get; set; }
    }
}