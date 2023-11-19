using Domain.Entities.Budget.Expenses;

namespace Application.ViewModels.Expense
{
    public class OtherExpenseRequest : ExpenseRequest
    {
        public int OtherExpenseTypeId { get; set; }
        public string Details { get; set; }
    }
}