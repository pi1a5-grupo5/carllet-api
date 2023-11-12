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
    public class FuelExpenseController : HomeController
    {
        private readonly IExpenseService<FuelExpense> _expenseService;
        private readonly IMapper _mapper;

        public FuelExpenseController(IExpenseService<FuelExpense> expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFuelExpense([FromBody] FuelExpenseRequest request)
        {
            var expense = _mapper.Map<FuelExpense>(request);
            var createdExpense = await _expenseService.RegisterExpense(expense);
            if (createdExpense == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<FuelExpenseResponse>(createdExpense);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFuelExpense([FromBody] Guid expenseId)
        {
            var expense = await _expenseService.DeleteExpense(expenseId);
            if (expense == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<FuelExpenseResponse>(expense);

            return Ok(result);
        }

        [HttpGet("{ExpenseId:Guid}")]
        public async Task<IActionResult> GetExpenseById(Guid ExpenseId)
        {
            var expense = await _expenseService.GetExpense(ExpenseId);
            if (expense == null)
            {
                return BadRequest();

            }
            var result = _mapper.Map<FuelExpenseResponse>(expense);

            return Ok(result);
        }

        [HttpGet("ByUser/{UserVehicleId:Guid}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId)
        {
            var expense = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            if (expense == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<FuelExpenseResponse>(expense);

            return Ok(result);
        }

        [HttpGet("ByUser/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var expense = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId, StartSearch, EndSearch);
            if (expense == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<FuelExpenseResponse>(expense);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFuelExpense([FromBody] FuelExpenseRequest expense)
        {
            var expenseReq = _mapper.Map<FuelExpense>(expense);
            var expenseRes = await _expenseService.UpdateExpense(expenseReq);
            if (expenseRes == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<FuelExpenseResponse>(expense);
            return Ok(result);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetExpenseTypes()
        {
            var expense = await _expenseService.GetExpenseTypes<FuelExpenseType>();
            if (expense == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<ExpenseTypeResponse>(expense);
            return Ok(result);
        }

        [HttpPost("Types")]
        public async Task<IActionResult> AddExpenseType([FromBody] FuelExpenseType expenseType)
        {
            _expenseService.AddExpenseType(expenseType);
            return Ok();
        }
    }


}

