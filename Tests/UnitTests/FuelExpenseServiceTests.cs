using Domain.Entities;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services;

namespace Tests.UnitTests
{
    public class ExpenseControllerTests
    {
        private readonly IExpenseService<FuelExpense> _expenseService;
        private readonly CarlletDbContext _dbContext;

        public ExpenseControllerTests()
        {
            var options = new DbContextOptionsBuilder<CarlletDbContext>()
                 .UseInMemoryDatabase(databaseName: "ExpensesTestDatabase")
                 .EnableSensitiveDataLogging()
                 .LogTo(Console.WriteLine, LogLevel.Information).Options;
             _dbContext = new CarlletDbContext(options);
            _expenseService = new FuelExpenseService(_dbContext);
        }

        [Fact]
        public async Task CreateFuelExpense_ValidFuelExpense_ReturnFuelExpenseCreatedAsync()
        {
            var fuelExpense = new FuelExpense { UserVehicleId = new Guid("be801137-fb11-41b6-b2f3-dfe88d59bd39"), Value = 50, ExpenseDate = new DateTime(), FuelTypeId = 1, Liters = 50 };
            Expense result = await _expenseService.RegisterExpense(fuelExpense);

            Assert.NotNull(result);
            Assert.Equal(result.Value, fuelExpense.Value);
        }

        [Fact]
        public async Task CreateFuelExpense_InvalidFuelExpense_ReturnNull()
        {
            var fuelExpense = new FuelExpense { UserVehicleId = new Guid("be801137-fb11-41b6-b2f3-dfe88d59bd00"), Value = 50, ExpenseDate = new DateTime(), FuelTypeId = 1, Liters = 50 };
            Expense result = await _expenseService.RegisterExpense(fuelExpense);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllFuelExpenses_ReturnListOffAllExpenses()
        {
            var result = _expenseService.GetExpensesList();
            Assert.NotNull(result);
        }
    }
}
