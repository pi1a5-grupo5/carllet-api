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
           var expense = _mapper.Map<OtherExpense>(request);
           var createdExpense = await _expenseService.RegisterExpense(expense);
           if (createdExpense == null)
           {
               return BadRequest();
           }

            var result = _mapper.Map<OtherExpenseResponse>(createdExpense);
           return Ok(result);
       }

       [HttpDelete("Fuel")]
       public async Task<IActionResult> DeleteExpense([FromBody] Guid expenseId)
       {
           var result = await _expenseService.DeleteExpense(expenseId);
           if (result == null)
           {
               return BadRequest();
           }

           return Ok(result);
       }

       [HttpGet("Fuel/{ExpenseId:Guid}")]
       public async Task<IActionResult> GetExpenseById(Guid ExpenseId)
       {
           var result = await _expenseService.GetExpense(ExpenseId);
           if (result == null)
           {
               return BadRequest();

           }
           return Ok(result);
       }

       [HttpGet("FuelByUser/{UserVehicleId:Guid}")]
       public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId)
       {
           var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
           if (result == null)
           {
               return BadRequest();
           }
           return Ok(result);
       }

       [HttpGet("FuelByUser/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
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
        public async Task<IActionResult> UpdateExpense([FromBody] OtherExpenseRequest expense)
        {
            var expenseReq = _mapper.Map<OtherExpense>(expense);
            var expenseRes = await _expenseService.UpdateExpense(expenseReq);
            if (expenseRes == null)
            {
                return BadRequest();
            }
            var result = _mapper.Map<OtherExpenseResponse>(expense);
            return Ok(result);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetExpenseTypes()
        {
            var result = await _expenseService.GetExpenseTypes<OtherExpenseType>();
            if (result == null)
            {
                return BadRequest();
            }
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
 