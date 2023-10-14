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
        Task<Expense> DeleteExpense(Expense expense);
        Task<Expense> GetExpense(Guid ExpenseId);
        Task<List<Earning>> GetExpenseByUser(Guid driver);
        Task<List<Earning>> GetExpenseByUser(Guid driver, DateTime StartSearch, DateTime EndSearch);
        Task<Expense> UpdateExpense(Expense expense);
    }
}
