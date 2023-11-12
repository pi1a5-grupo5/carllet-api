namespace Application.ViewModels.Expense
{
    public class ExpenseRequest
    {
        public Guid UserVehicleId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Value { get; set; }
    }
}