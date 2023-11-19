namespace Domain.Entities.Budget.Expenses
{
    public class FuelExpense : Expense
    {
        public decimal Liters { get; set; }
        public int FuelExpenseTypeId { get; set; }
        public FuelExpenseType FuelExpenseType { get; set; }

    }
}
