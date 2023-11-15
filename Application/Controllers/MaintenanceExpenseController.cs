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
    public class MaintenanceExpenseController : HomeController
    {
        private readonly IExpenseService<MaintenanceExpense> _expenseService;
        private readonly IMapper _mapper;

        public MaintenanceExpenseController(IExpenseService<MaintenanceExpense> expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterExpense([FromBody] MaintenanceExpenseRequest request)
        {
            var expense = _mapper.Map<MaintenanceExpense>(request);
            var result = await _expenseService.RegisterExpense(expense);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
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
            var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("ByUser/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
        {
            var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

         [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] MaintenanceExpenseRequest expense)
        {
            var expenseReq = _mapper.Map<MaintenanceExpense>(expense);
            var expenseRes = await _expenseService.UpdateExpense(expenseReq);
            if (expenseRes == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<MaintenanceExpenseResponse>(expense);
            return Ok(result);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetExpenseTypes()
        {
            var expense = await _expenseService.GetExpenseTypes<MaintenanceExpenseType>();
            if (expense == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<ExpenseTypeResponse>(expense);

            return Ok(result);
        }

        [HttpPost("Types")]
        public async Task<IActionResult> AddExpenseType([FromBody] MaintenanceExpenseType expenseType)
        {
            _expenseService.AddExpenseType(expenseType);
            return Ok();
        }
    }
}
