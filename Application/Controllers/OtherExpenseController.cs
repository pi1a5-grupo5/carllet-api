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
   public class OtherExpenseController : HomeController
   {
       private readonly IExpenseService<OtherExpense> _expenseService;
       private readonly IMapper _mapper;

       public OtherExpenseController(IExpenseService<OtherExpense> expenseService, IMapper mapper)
       {
           _expenseService = expenseService;
           _mapper = mapper;
       }

       [HttpPost]
       public async Task<IActionResult> RegisterExpense([FromBody] OtherExpenseRequest request)
       {
           var expenseReq = _mapper.Map<OtherExpense>(request);
           var expense = await _expenseService.RegisterExpense(expenseReq);
            var result = new OtherExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                OtherTypeId = expense.OtherExpenseTypeId,
                OtherTypeName = expense.OtherExpenseType.OtherExpenseName
               
            };
            return Ok(result);
       }

       [HttpDelete("{expenseId:Guid}")]
       public async Task<IActionResult> DeleteExpense(Guid expenseId)
       {
           var expense = await _expenseService.DeleteExpense(expenseId);
            var result = new OtherExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                OtherTypeId = expense.OtherExpenseTypeId,
                OtherTypeName = expense.OtherExpenseType.OtherExpenseName

            };
            return Ok(result);
       }

       [HttpGet("/{expenseId:Guid}")]
       public async Task<IActionResult> GetExpenseById(Guid expenseId)
       {
           var expense = await _expenseService.GetExpense(expenseId);
            var result = new OtherExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                OtherTypeId = expense.OtherExpenseTypeId,
                OtherTypeName = expense.OtherExpenseType.OtherExpenseName

            };
            return Ok(result);
        }

       [HttpGet("ByUser/{UserId:Guid}")]
       public async Task<IActionResult> GetExpenseByUser(Guid UserId)
       {
           var expenses = await _expenseService.GetExpenseByUserId(UserId);
            var result = new List<OtherExpenseResponse>();

            foreach (var expense in expenses)
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
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUser(Guid UserId, DateOnly StartSearch, DateOnly EndSearch)
        {
            var expenses = await _expenseService.GetExpenseByUserId(UserId);
            var result = new List<OtherExpenseResponse>();

            foreach (var expense in expenses)
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
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpGet("ByUserVehicle/{UserVehicleId:Guid}")]
        public async Task<IActionResult> GetExpenseByUserVehicle(Guid UserVehicleId)
        {
            var expenses = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            var result = new List<OtherExpenseResponse>();

            foreach (var expense in expenses)
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
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpGet("ByUserVehicle/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetExpenseByUserVehicle(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
        {
            var expenses = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
            var result = new List<OtherExpenseResponse>();

            foreach (var expense in expenses)
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
                result.Add(expenseByUser);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] OtherExpenseRequest request)
        {
            var expenseReq = _mapper.Map<OtherExpense>(request);
            var expense = await _expenseService.UpdateExpense(expenseReq);
            var result = new OtherExpenseResponse
            {
                ExpenseId = expense.ExpenseId,
                UserVehicleId = expense.UserVehicleId,
                ExpenseDate = expense.ExpenseDate,
                Value = expense.Value,
                OtherTypeId = expense.OtherExpenseTypeId,
                OtherTypeName = expense.OtherExpenseType.OtherExpenseName

            };
            return Ok(result);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetExpenseTypes()
        {
            var expense = await _expenseService.GetExpenseTypes<OtherExpenseType>();
            if (expense == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<List<ExpenseTypeResponse>>(expense);

            return Ok(result);
        }

        [HttpPost("Types")]
        public async Task<IActionResult> AddExpenseType([FromBody] OtherExpenseType expenseType)
        {
            _expenseService.AddExpenseType(expenseType);
            return Ok();
        }
       }
}
 