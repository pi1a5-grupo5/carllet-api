using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;

namespace Services
{
    public class MaintenanceExpenseService : IExpenseService<MaintenanceExpense>
    {
        private readonly CarlletDbContext _dbContext;

        public MaintenanceExpenseService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MaintenanceExpense> DeleteExpense(Guid ExpenseId)
        {
            var expense = _dbContext.MaintenanceExpenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);

            if (expense == null)
            {
                return null;
            }

            _dbContext.MaintenanceExpenses.Remove(expense);
            _dbContext.SaveChanges();
            return expense;
        }

        public async Task<MaintenanceExpense> GetExpense(Guid ExpenseId)
        {
            var expense = _dbContext.MaintenanceExpenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);
            return expense;
        }

        public async Task<List<MaintenanceExpense>> GetExpensesList()
        {
            var expenses = _dbContext.MaintenanceExpenses.ToList();
            return expenses;
        }

        public async Task<List<MaintenanceExpense>> GetExpenseByUserId(Guid driver)
        {
                var expenses = _dbContext.UserVehicles
        .Where(uv => uv.UserId == driver)
        .SelectMany(uv => uv.Expenses.OfType<MaintenanceExpense>())
        .ToList();

            return expenses;
        }

        public Task<List<MaintenanceExpense>> GetExpenseByUserID(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MaintenanceExpense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.MaintenanceExpenses.Where(e => e.UserVehicleId == userVehicleId).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<MaintenanceExpense>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses = _dbContext.MaintenanceExpenses.Where(u => u.UserVehicleId == UserVehicleId
                && u.ExpenseDate <= StartSearch
                && u.ExpenseDate >= EndSearch).ToList();

            if (expenses == null || expenses.Count == 0)
            {
                return null;
            }
            return expenses;
        }

        public async Task<MaintenanceExpense> RegisterExpense(MaintenanceExpense expense)
        {
            _dbContext.MaintenanceExpenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<MaintenanceExpense> UpdateExpense(MaintenanceExpense expense)
        {
            var expenseToUpdate = _dbContext.MaintenanceExpenses.Find(expense.ExpenseId);
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
            if (expense is MaintenanceExpenseType)
            {
                _dbContext.MaintenanceExpenses.Add(expense as MaintenanceExpense);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<U>> GetExpenseTypes<U>() where U : ExpenseType
        {
            if (typeof(U) == typeof(MaintenanceExpenseType))
            {
                var maintenanceExpenseTypes = _dbContext.MaintenanceExpenseTypes.ToList();
                return maintenanceExpenseTypes as List<U>;
            }

            return null;
        }

        public Task<Dictionary<DateTime, decimal>> GetExpensesByUserByDay(Guid userId, int days)
        {
            throw new NotImplementedException();
        }
    }
}
