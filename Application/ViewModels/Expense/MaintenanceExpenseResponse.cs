namespace Application.ViewModels.Expense
{
    public class MaintenanceExpenseResponse : ExpenseResponse
    {
        public string MaintenanceTypeName { get; set; }
        public string Details { get; set; }
        public Guid OriginatingExpenseId { get; set; }
    }
}