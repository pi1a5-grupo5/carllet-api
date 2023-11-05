using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;

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
            var expense = _dbContext.FuelExpenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);

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
            var expense = _dbContext.FuelExpenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);
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
            .ToList();

            if (fuelExpenses == null || fuelExpenses.Count == 0)
            {
                return null;
            }

            return fuelExpenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserID(Guid driver, DateOnly StartSearch, DateOnly EndSearch)
        {
            var fuelExpenses = _dbContext.UserVehicles
            .Where(uv => uv.UserId == driver)
            .SelectMany(uv => uv.Expenses.OfType<FuelExpense>())
            .Where(e => e.ExpenseDate <= StartSearch
                && e.ExpenseDate >= EndSearch)
            .ToList();

            if (fuelExpenses == null || fuelExpenses.Count == 0)
            {
                return null;
            }
            
            return fuelExpenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.FuelExpenses.Where(e => e.UserVehicleId == userVehicleId).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<FuelExpense>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
        {
            var expenses = _dbContext.FuelExpenses.Where(u => u.UserVehicleId == UserVehicleId
                && u.ExpenseDate <= StartSearch
                && u.ExpenseDate >= EndSearch).ToList();

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
    }
}
