using Application.ViewModels.Expense;
using AutoMapper;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Entities.VehicleNS;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : HomeController
    {
        private readonly IExpenseService<Expense> _expenseService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService<Expense> expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExpense([FromBody] Guid expenseId)
        {
            var result = await _expenseService.DeleteExpense(expenseId);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("{ExpenseId:Guid}")]
        public async Task<IActionResult> GetExpenseById(Guid ExpenseId)
        {
            var result = await _expenseService.GetExpense(ExpenseId);
            if (result == null)
            {
                return BadRequest();

            }
            return Ok(result);
        }

        [HttpGet("ByUser/{UserVehicleId:Guid}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId)
        {
            var result = await _expenseService.GetExpenseByUserId(UserVehicleId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("ByUser/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
        {
            var result = await _expenseService.GetExpenseByUserId(UserVehicleId);
            if (result == null)
            {
                return BadRequest();
            }
                return Ok(result);
            }
        }
}
 