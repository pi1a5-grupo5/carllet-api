using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IExpenseService<T> where T : Expense
    {
        Task<T> RegisterExpense(T expense);
        Task<T> DeleteExpense(Guid ExpenseId);
        Task<List<T>> GetExpensesList();
        Task<T> GetExpense(Guid ExpenseId);
        Task<List<T>> GetExpenseByUserId(Guid driver);
        Task<List<T>> GetExpenseByUserID(Guid driver, DateOnly StartSearch, DateOnly EndSearch);
        Task<List<T>> GetExpenseByUserVehicleId(Guid UserVehicleId);
        Task<List<T>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch);
        Task<T> UpdateExpense(T expense);
        void AddExpenseType<U>(U expense) where U : ExpenseType;
        Task<List<U>> GetExpenseTypes<U>() where U : ExpenseType;
    }
}
