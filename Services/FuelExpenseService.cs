using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class FuelExpenseService : IExpenseService<FuelExpense>
    {
        private readonly CarlletDbContext _dbContext;

        public FuelExpenseService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FuelExpense> DeleteExpense(Guid ExpenseId)
        {
            var expense = _dbContext.FuelExpenses.Where(e => e.ExpenseId == ExpenseId).FirstOrDefault();

            if (expense == null)
            {
                return null;
            }

            _dbContext.FuelExpenses.Remove(expense);
            _dbContext.SaveChanges();
            return expense;
        }

        public async Task<FuelExpense> GetExpense(Guid ExpenseId)
        {
            var expense = _dbContext.FuelExpenses.Where(e => e.ExpenseId == ExpenseId).Include(fe => fe.FuelExpenseType).FirstOrDefault();
            return expense;
        }

        public async Task<List<FuelExpense>> GetExpensesList()
        {
            var expenses = _dbContext.FuelExpenses.ToList();
            return expenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserId(Guid driver)
        {
            var fuelExpenses = _dbContext.UserVehicles
            .Where(uv => uv.UserId == driver)
            .SelectMany(uv => uv.Expenses.OfType<FuelExpense>())
            .Include(fe => fe.FuelExpenseType)
            .ToList();

            if (fuelExpenses == null || fuelExpenses.Count == 0)
            {
                return null;
            }

            return fuelExpenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserID(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            var fuelExpenses = _dbContext.UserVehicles
            .Where(uv => uv.UserId == driver)
            .SelectMany(uv => uv.Expenses.OfType<FuelExpense>())
            .Where(e => e.ExpenseDate <= StartSearch
                && e.ExpenseDate >= EndSearch)
            .Include(fe => fe.FuelExpenseType)
            .ToList();

            if (fuelExpenses == null || fuelExpenses.Count == 0)
            {
                return null;
            }
            
            return fuelExpenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.FuelExpenses.Where(e => e.UserVehicleId == userVehicleId).Include(fe => fe.FuelExpenseType).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses = _dbContext.FuelExpenses.Where(u => u.UserVehicleId == UserVehicleId
                && u.ExpenseDate <= StartSearch
                && u.ExpenseDate >= EndSearch).Include(fe => fe.FuelExpenseType).ToList();

            if (expenses == null || expenses.Count == 0)
            {
                return null;
            }
            return expenses;
        }

        public async Task<FuelExpense> RegisterExpense(FuelExpense expense)
        {
            _dbContext.FuelExpenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<FuelExpense> UpdateExpense(FuelExpense expense)
        {
            var expenseToUpdate = _dbContext.FuelExpenses.Find(expense.ExpenseId);
            if (expenseToUpdate == null)
            {
                return null;
            }

            _dbContext.Entry(expenseToUpdate).CurrentValues.SetValues(expense);
            await _dbContext.SaveChangesAsync();
            return expenseToUpdate;
        }   

        public void AddExpenseType<U>(U expense) where U : ExpenseType
        {
            if (expense is FuelExpenseType)
            {
                _dbContext.FuelExpenseTypes.Add(expense as FuelExpenseType);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<U>> GetExpenseTypes<U>() where U : ExpenseType
        {
            if (typeof(U) == typeof(FuelExpenseType))
            {
                var fuelExpenseTypes = _dbContext.FuelExpenseTypes.ToList();
                return fuelExpenseTypes as List<U>;
            }

            return null;
        }

        public Task<Dictionary<DateTime, decimal>> GetExpensesByUserByDay(Guid userId, int days)
        {
            throw new NotImplementedException();
        }
    }
}
