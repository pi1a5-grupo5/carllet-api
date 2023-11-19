using Application.ViewModels.Expense;
using Application.ViewModels.Vehicle;
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
            var expenseReq = _mapper.Map<FuelExpense>(request);
            var expense = await _expenseService.RegisterExpense(expenseReq);
            if (expense == null)
            {
                return BadRequest();
            }
            var result = new FuelExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId= expense.UserVehicleId,
                ExpenseDate= expense.ExpenseDate,
                Value = expense.Value,
                Liters= expense.Liters,
                FuelTypeId= expense.FuelExpenseTypeId,
                FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
            };
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
            var result = new FuelExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                Liters = expense.Liters,
                FuelTypeId = expense.FuelExpenseTypeId,
                FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
            };
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
            var result = new FuelExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                Liters = expense.Liters,
                FuelTypeId = expense.   FuelExpenseTypeId,
                FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
            };
            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserId)
        {
            var expenses = await _expenseService.GetExpenseByUserId(UserId);
            if (expenses == null || expenses.Count == 0)
            {
                return NotFound();
            }
            var result = new List<FuelExpenseResponse>();

            foreach(var expense in expenses)
            {
                var expenseByUser = new FuelExpenseResponse
                {
                    ExpenseId = expense.ExpenseId,
                    UserVehicleId = expense.UserVehicleId,
                    ExpenseDate = expense.ExpenseDate,
                    Value = expense.Value,
                    Liters = expense.Liters,
                    FuelTypeId = expense.FuelExpenseTypeId,
                    FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
                };
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserId, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses = await _expenseService.GetExpenseByUserId(UserId, StartSearch, EndSearch);
            if (expenses == null)
            {
                return BadRequest();
            }

            var result = new List<FuelExpenseResponse>();

            foreach (var expense in expenses)
            {
                var expenseByUser = new FuelExpenseResponse
                {
                    ExpenseId = expense.ExpenseId,
                    UserVehicleId = expense.UserVehicleId,
                    ExpenseDate = expense.ExpenseDate,
                    Value = expense.Value,
                    Liters = expense.Liters,
                    FuelTypeId = expense.FuelExpenseTypeId,
                    FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
                };
                result.Add(expenseByUser);
            }

            return Ok(result);

        }

        [HttpGet("ByUserVehicle/{UserVehicleId:Guid}")]
        public async Task<IActionResult> GetExpenseByUserVehicle(Guid UserVehicleId)
        {
            var expenses = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            if (expenses == null)
            {
                return BadRequest();
            }
            var result = new List<FuelExpenseResponse>();

            foreach (var expense in expenses)
            {
                var expenseByUser = new FuelExpenseResponse
                {
                    ExpenseId = expense.ExpenseId,
                    UserVehicleId = expense.UserVehicleId,
                    ExpenseDate = expense.ExpenseDate,
                    Value = expense.Value,
                    Liters = expense.Liters,
                    FuelTypeId = expense.FuelExpenseTypeId,
                    FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
                };
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpGet("ByUserVehicle/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUserVehicle(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId, StartSearch, EndSearch);
            if (expenses == null)
            {
                return BadRequest();
            }
            var result = new List<FuelExpenseResponse>();

            foreach (var expense in expenses)
            {
                var expenseByUser = new FuelExpenseResponse
                {
                    ExpenseId = expense.ExpenseId,
                    UserVehicleId = expense.UserVehicleId,
                    ExpenseDate = expense.ExpenseDate,
                    Value = expense.Value,
                    Liters = expense.Liters,
                    FuelTypeId = expense.FuelExpenseTypeId,
                    FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
                };
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFuelExpense([FromBody] FuelExpenseRequest request)
        {
            var expenseReq = _mapper.Map<FuelExpense>(request);
            var expense = await _expenseService.UpdateExpense(expenseReq);
            if (expense == null)
            {
                return BadRequest();
            }
            var result = new FuelExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                Liters = expense.Liters,
                FuelTypeId = expense.FuelExpenseTypeId,
                FuelTypeName = expense.FuelExpenseType.FuelExpenseName,
            };
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
            var result = _mapper.Map<List<ExpenseTypeResponse>>(expense);
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

