using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> RegisterExpense(Expense expense);
        Task<Expense> DeleteExpense(Guid ExpenseId);
        Task<Expense> GetExpense(Guid ExpenseId);
        Task<List<Expense>> GetExpenseByUser(Guid driver);
        Task<List<Expense>> GetExpenseByUser(Guid driver, DateOnly StartSearch, DateOnly EndSearch);
        Task<Expense> UpdateExpense(Expense expense);
    }
}
