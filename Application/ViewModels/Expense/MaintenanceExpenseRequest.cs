namespace Application.ViewModels.Expense
{
    public class MaintenanceExpenseRequest : ExpenseRequest
    {
        public string MaintenanceExpenseTypeId { get; set; }
        public string Details { get; set; }
        public Guid? OriginatingExpenseId { get; set; }
    }
}