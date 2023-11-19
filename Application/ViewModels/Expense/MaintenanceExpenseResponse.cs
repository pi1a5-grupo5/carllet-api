namespace Application.ViewModels.Expense
{
    public class MaintenanceExpenseResponse : ExpenseResponse
    {
        public int MaintenanceTypeId { get; set; }
        public string MaintenanceTypeName { get; set; }
        public string Details { get; set; }
        public Guid? OriginatingExpenseId { get; set; }
    }
}