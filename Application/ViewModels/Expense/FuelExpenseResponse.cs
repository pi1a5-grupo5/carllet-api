﻿namespace Application.ViewModels.Expense
{
    public class FuelExpenseResponse : ExpenseResponse
    {
        public decimal Liters { get; set; }
        public string FuelTypeName { get; set; }
    }
}