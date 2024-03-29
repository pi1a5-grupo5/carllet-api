using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class OtherExpenseService : IExpenseService<OtherExpense>
    {
        private readonly CarlletDbContext _dbContext;

        public OtherExpenseService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OtherExpense> DeleteExpense(Guid ExpenseId)
        {
            var expense = _dbContext.OtherExpenses.Where(e => e.ExpenseId == ExpenseId).Include(e => e.OtherExpenseType).FirstOrDefault();

            if (expense == null)
            {
                return null;
            }

            _dbContext.OtherExpenses.Remove(expense);
            _dbContext.SaveChanges();
            return expense;
        }

        public async Task<OtherExpense> GetExpense(Guid ExpenseId)
        {
            var expense = _dbContext.OtherExpenses.Where(e => e.ExpenseId == ExpenseId).Include(oe => oe.OtherExpenseType).FirstOrDefault();
            return expense;
        }

        public async Task<List<OtherExpense>> GetExpensesList()
        {
            var expenses = _dbContext.OtherExpenses.ToList();
            return expenses;
        }
        public async Task<List<OtherExpense>> GetExpenseByUserId(Guid driver)
        {
            var expenses = _dbContext.UserVehicles
            .Where(uv => uv.UserId == driver)
            .SelectMany(uv => uv.Expenses.OfType<OtherExpense>())
            .Include(oe => oe.OtherExpenseType)
            .ToList();

            return expenses;
        }

        public Task<List<OtherExpense>> GetExpenseByUserId(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OtherExpense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.OtherExpenses.Where(e => e.UserVehicleId == userVehicleId).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<OtherExpense>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses = _dbContext.OtherExpenses.Where(u => u.UserVehicleId == UserVehicleId
                && u.ExpenseDate <= StartSearch
                && u.ExpenseDate >= EndSearch).ToList();

            if (expenses == null || expenses.Count == 0)
            {
                return null;
            }
            return expenses;
        }

        public async Task<OtherExpense> RegisterExpense(OtherExpense expense)
        {
            _dbContext.OtherExpenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            expense = await GetExpense(expense.ExpenseId);
            return expense;
        }

        public async Task<OtherExpense> UpdateExpense(OtherExpense expense)
        {
            var expenseToUpdate = _dbContext.OtherExpenses.Find(expense.ExpenseId);
            if (expenseToUpdate == null)
            {
                return null;
            }

            _dbContext.Entry(expenseToUpdate).CurrentValues.SetValues(expense);
            await _dbContext.SaveChangesAsync();
            return expenseToUpdate;
        }

        public void AddExpenseType<U>(U expense) where U : ExpenseType        {
            if (expense is OtherExpenseType)
            {
                _dbContext.OtherExpenseTypes.Add(expense as OtherExpenseType);
                _dbContext.SaveChanges();

            }
        }


        public async Task<List<U>> GetExpenseTypes<U>() where U : ExpenseType
        {
            if (typeof(U) == typeof(OtherExpenseType))
            {
                var otherExpenseTypes = _dbContext.OtherExpenseTypes.ToList();
                return otherExpenseTypes as List<U>;
            }

            return null;
        }

        public Task<Dictionary<DateTime, decimal>> GetExpensesByUserByDay(Guid userId, int days)
        {
            throw new NotImplementedException();
        }
    }
}
