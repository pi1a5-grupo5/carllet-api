using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Budget.Expenses;

public class OtherExpenseType: ExpenseType
{     
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OtherExpenseTypeId { get; set; }
    public string OtherExpenseName { get; set; }
}
