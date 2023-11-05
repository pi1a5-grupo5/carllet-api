//using Application.ViewModels.Expense;
//using AutoMapper;
//using Domain.Entities.Budget;
//using Domain.Entities.Budget.Expenses;
//using Domain.Entities.VehicleNS;
//using Domain.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Services;
//using System;

//namespace Application.Controllers
//{
//    [Route("api/[controller]")]
//    [Route("Fuel")]
//    [ApiController]
//    public class ExpenseController : HomeController
//    {
//        private readonly IExpenseService _expenseService;
//        private readonly IMapper _mapper;

//        public ExpenseController(IExpenseService expenseService, IMapper mapper)
//        {
//            _expenseService = expenseService;
//            _mapper = mapper;
//        }

//        [HttpPost("Fuel")]
//        public async Task<IActionResult> RegisterFuelExpense([FromBody] FuelExpenseDTO expense)
//        {
//            var expenseReq = _mapper.Map<Expense>(expense);
//            var result = await _expenseService.RegisterExpense(expenseReq);
//            if (result == null)
//            {
//                return BadRequest();
//            }

//            return Ok(result);
//        }

//        [HttpDelete("Fuel")]
//        public async Task<IActionResult> DeleteFuelExpense([FromBody] Guid expenseId)
//        {
//            var result = await _expenseService.DeleteExpense(expenseId);
//            if (result == null)
//            {
//                return BadRequest();
//            }

//            return Ok(result);
//        }

//        [HttpGet("Fuel/{ExpenseId:Guid}")]
//        public async Task<IActionResult> GetExpenseById(Guid ExpenseId)
//        {
//            var result = await _expenseService.GetExpense(ExpenseId);
//            if (result == null)
//            {
//                return BadRequest();

//            }
//            return Ok(result);
//        }

//        [HttpGet("FuelByUser/{UserVehicleId:Guid}")]
//        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId)
//        {
//            var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
//            if (result == null)
//            {
//                return BadRequest();
//            }
//            return Ok(result);
//        }

//        [HttpGet("FuelByUser/{UserVehicleId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
//        public async Task<IActionResult> GetExpenseByUser(Guid UserVehicleId, DateOnly StartSearch, DateOnly EndSearch)
//        {
//            var result = await _expenseService.GetExpenseByUserVehicleId(UserVehicleId);
//            if (result == null)
//            {
//                return BadRequest();
//            }
//                return Ok(result);
//            }
//        }
//}
 