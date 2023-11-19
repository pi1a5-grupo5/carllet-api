namespace Application.ViewModels.Expense
{
    public class FuelExpenseRequest : ExpenseRequest
    {
        public decimal Liters { get; set; }
        public int FuelExpenseTypeId { get; set; }
    }
}