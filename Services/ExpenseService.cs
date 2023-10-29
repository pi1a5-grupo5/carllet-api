using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly CarlletDbContext _dbContext;

        public ExpenseService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> DeleteExpense(Guid ExpenseId)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);

            if (expense == null)
            {
                return null;
            }

            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();
            return expense;
        }

        public async Task<Expense> GetExpense(Guid ExpenseId)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);
            return expense;
        }

        public async Task<List<Expense>> GetExpenseByUser(Guid userVehicleId)
        {
            var expenses = _dbContext.Expenses.Where(e => e.UserVehicleId == userVehicleId).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUser(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
        {
            var expenses = _dbContext.Expenses.Where(u => u.UserVehicleId == UserVehicleId
                && u.ExpenseDate <= StartSearch
                && u.ExpenseDate >= EndSearch).ToList();

            if (expenses == null || expenses.Count == 0)
            {
                return null;
            }
            return expenses;
        }

        public async Task<Expense> RegisterExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }

        public Task<Expense> UpdateExpense(Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}
