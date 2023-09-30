namespace Domain.Entities.Budget.Expenses
{
    public class MaintenanceExpense : Expense
    {

        public int MaintenanceExpenseTypeId { get; set; }
        public MaintenanceExpenseType? MaintenanceExpenseType { get; set; }
        public string Details { get; set; }
        public Guid OriginatingExpenseId { get; set; }
        public MaintenanceExpense OriginatingExpense { get; set; }

    }
}

