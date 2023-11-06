using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;

namespace Services
{
    public class ExpenseService : IExpenseService<Expense>
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

        public async Task<List<Expense>> GetExpensesList()
        {
            var expenses = _dbContext.Expenses.ToList();
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserId(Guid driver)
        {
            var expenses = _dbContext.UserVehicles
            .Where(uv => uv.UserId == driver)
            .SelectMany(uv => uv.Expenses)
            .ToList();
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserID(Guid driver, DateOnly StartSearch, DateOnly EndSearch)
        {
            var expenses = _dbContext.UserVehicles
                .Where(uv => uv.UserId == driver)
                .SelectMany(uv => uv.Expenses)
                .Where(e => e.ExpenseDate <= StartSearch && e.ExpenseDate >= EndSearch)
                .ToList();

            if(expenses == null || expenses.Count == 0){
                return null;
            }
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.Expenses.Where(e => e.UserVehicleId == userVehicleId).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
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
            _dbContext.SaveChanges();
            return expense;
        }

        public Task<Expense> UpdateExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public void AddExpenseType<U>(U expense) where U : ExpenseType
        {
            throw new NotImplementedException();
        }

        public Task<List<U>> GetExpenseTypes<U>() where U : ExpenseType
        {
            throw new NotImplementedException();
        }
    }
}
