using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Budget.Expenses;

public class OtherExpenseType: ExpenseType
{     
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaintenanceExpenseTypeId { get; set; }
    public string MaintenanceName { get; set; }
}
