using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Goal
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid GoalId { get; set; }
    public double GoalValue { get; set; }
    public DateTime GoalInsertionDate { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
