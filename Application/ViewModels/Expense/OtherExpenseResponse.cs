using Domain.Entities.Budget.Expenses;

namespace Application.ViewModels.Expense
{
    public class OtherExpenseResponse : ExpenseResponse
    {
        public string OtherTypeName { get; set; }
        public string Details { get; set; }
    }
}