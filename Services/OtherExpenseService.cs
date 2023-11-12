using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;

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
            var expense = _dbContext.OtherExpenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);

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
            var expense = _dbContext.OtherExpenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);
            return expense;
        }

        public async Task<List<OtherExpense>> GetExpensesList()
        {
            var expenses = _dbContext.OtherExpenses.ToList();
            return expenses;
        }
        public Task<List<OtherExpense>> GetExpenseByUserId(Guid driver)
        {
            throw new NotImplementedException();
        }

        public Task<List<OtherExpense>> GetExpenseByUserID(Guid driver, DateTime StartSearch, DateTime EndSearch)
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
            if (expense is OtherExpense)
            {
                _dbContext.OtherExpenses.Add(expense as OtherExpense);
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
