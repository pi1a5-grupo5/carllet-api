using Application.Controllers;
using Services;
using Domain.Interfaces;
using Xunit;
using Infra.Data;

namespace Tests
{
    public class ExpenseControllerTest
    {
        ExpenseController _controller;
        IExpenseService _service;
        CarlletDbContext _dbContext;

        public ExpenseControllerTest()
        {
            _service = new ExpenseService(_dbContext);
            _controller = new ExpenseController(_service);
        }

        [Fact]
        public void GET_Expense_ReturnOkExpensesListByUser()
        {
            var result
        }
    }
}