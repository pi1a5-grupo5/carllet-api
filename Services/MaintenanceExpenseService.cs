using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

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
            var expense = _dbContext.MaintenanceExpenses.Where(e => e.ExpenseId == ExpenseId).Include(e => e.MaintenanceExpenseType)
                .FirstOrDefault();

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
            var expense = _dbContext.MaintenanceExpenses.Where(e => e.ExpenseId == ExpenseId)
                .Include(me => me.MaintenanceExpenseType)
                .FirstOrDefault();
            return expense;
        }

        public async Task<List<MaintenanceExpense>> GetExpensesList()
        {
            var expenses = _dbContext.MaintenanceExpenses.ToList();
            return expenses;
        }

        public async Task<List<MaintenanceExpense>> GetExpenseByUserId(Guid driver)
        {
                var expenses = _dbContext.MaintenanceExpenses
                .Where(e => e.UserVehicle.UserId == driver).Include(me => me.MaintenanceExpenseType)
                .ToList();


            return expenses;
        }

        public async Task<List<MaintenanceExpense>> GetExpenseByUserId(Guid driver, DateTime StartSearch, DateTime EndSearch)
        { 
            var expenses = _dbContext.MaintenanceExpenses
                .Where(e => e.UserVehicle.UserId == driver && e.ExpenseDate >= StartSearch && e.ExpenseDate <= EndSearch).Include(me => me.MaintenanceExpenseType)
                .ToList();

            return expenses;
        }

        public async Task<List<MaintenanceExpense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.MaintenanceExpenses.Where(e => e.UserVehicleId == userVehicleId)
                .Include(me => me.MaintenanceExpenseType).ToList();
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
                && u.ExpenseDate >= EndSearch).Include(me => me.MaintenanceExpenseType).ToList();

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

            expense = await GetExpense(expense.ExpenseId);
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
                _dbContext.MaintenanceExpenseTypes.Add(expense as MaintenanceExpenseType);
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
