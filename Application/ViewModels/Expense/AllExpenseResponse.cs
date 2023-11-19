namespace Application.ViewModels.Expense
{
    public class AllExpenseResponse
    {
        public Guid UserId { get; set; } 
        public List<FuelExpenseResponse> FuelExpenses { get; set; }
        public List<MaintenanceExpenseResponse> MaintenanceExpenses { get; set; }
        public List<OtherExpenseResponse> OtherExpenses { get; set; }

    }
}
