using Domain.Entities.Budget.Expenses;

namespace Application.ViewModels.Expense
{
    public class OtherExpenseResponse : ExpenseResponse
    {
        public int OtherTypeId { get; set; }
        public string OtherTypeName { get; set; }
        public string Details { get; set; }
    }
}