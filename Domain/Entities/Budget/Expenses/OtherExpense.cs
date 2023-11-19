namespace Domain.Entities.Budget.Expenses;

public class OtherExpense:Expense
{
        public int OtherExpenseTypeId { get; set; }
        public OtherExpenseType OtherExpenseType { get; set; }
        public string Details { get; set; }
}
