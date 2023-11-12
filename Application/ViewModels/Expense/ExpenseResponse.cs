namespace Application.ViewModels.Expense
{
    public class ExpenseResponse
    {
        public Guid ExpenseId { get; set; }
        public Guid UserVehicleId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Value { get; set; }
    }
}