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
        private readonly IExpenseService<FuelExpense> _fuelExpenseService;
        private readonly IExpenseService<MaintenanceExpense> _maintenanceExpenseService;
        private readonly IExpenseService<OtherExpense> _otherExpenseService;


        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService<Expense> expenseService, IMapper mapper, 
            IExpenseService<FuelExpense> fuelExpenseService, IExpenseService<MaintenanceExpense> maintenanceExpenseService, 
            IExpenseService<OtherExpense> otherExpenseService)
        {
            _expenseService = expenseService;
            _fuelExpenseService = fuelExpenseService; 
            _maintenanceExpenseService= maintenanceExpenseService;
            _otherExpenseService = otherExpenseService;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExpense([FromBody] Guid expenseId)
        {
            var result = await _expenseService.DeleteExpense(expenseId);
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var result = await _expenseService.GetExpensesList();
            if (result == null)
            {
                return NoContent();

            }
            return Ok(result);
        }

        [HttpGet("{ExpenseId:Guid}")]
        public async Task<IActionResult> GetExpenseById(Guid ExpenseId)
        {
            var result = await _expenseService.GetExpense(ExpenseId);
            if (result == null)
            {
                return NoContent();

            }
            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserId)
        {
            var fuelExpenses = await _fuelExpenseService.GetExpenseByUserId(UserId);
            var maintenanceExpenses = await _maintenanceExpenseService.GetExpenseByUserId(UserId);
            var otherExpenses = await _otherExpenseService.GetExpenseByUserId(UserId);

            var maintenanceResult = new List<MaintenanceExpenseResponse>();
            if (maintenanceExpenses != null)
            {
                foreach (var expense in maintenanceExpenses)
                {
                    var expenseByUser = new MaintenanceExpenseResponse
                    {
                        ExpenseId = expense.ExpenseId,
                        UserVehicleId = expense.UserVehicleId,
                        ExpenseDate = expense.ExpenseDate,
                        Value = expense.Value,
                        MaintenanceTypeId = expense.MaintenanceExpenseTypeId,
                        MaintenanceTypeName = expense.MaintenanceExpenseType.MaintenanceName,
                        Details = expense.Details
                    };
                    maintenanceResult.Add(expenseByUser);
                }
            }

            var fuelResult = new List<FuelExpenseResponse>();
            if (fuelExpenses != null)
            {
                foreach (var expense in fuelExpenses)
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
                    fuelResult.Add(expenseByUser);
                }
            }
            var otherResult = new List<OtherExpenseResponse>();
            if (otherExpenses != null)
            {
                foreach (var expense in otherExpenses)
                {
                    var expenseByUser = new OtherExpenseResponse
                    {
                        ExpenseId = expense.ExpenseId,
                        UserVehicleId = expense.UserVehicleId,
                        ExpenseDate = expense.ExpenseDate,
                        Value = expense.Value,
                        OtherTypeId = expense.OtherExpenseTypeId,
                        OtherTypeName = expense.OtherExpenseType.OtherExpenseName

                    };
                    otherResult.Add(expenseByUser);
                }
            }


            var result = new AllExpenseResponse
            {
                UserId = UserId,
                MaintenanceExpenses = maintenanceResult,
                FuelExpenses = fuelResult,
                OtherExpenses = otherResult,
            };

            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserId, DateTime StartSearch, DateTime EndSearch)
        {
            var result = await _expenseService.GetExpenseByUserId(UserId, StartSearch, EndSearch);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

    [HttpGet("ByUserVehicle/{UserVehicleId:Guid}")]
        public async Task<IActionResult> GetExpenseByUserVehicle(Guid UserVehicleId)
        {
            var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("ByUserVehicle/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUserVehicle(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId, StartSearch, EndSearch);
            if (result == null)
            {
                return NoContent();
            }
                return Ok(result);
            }
        }
}
 