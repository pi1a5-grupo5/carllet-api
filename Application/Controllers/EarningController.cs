using Application.ViewModels.Earning;
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
    public class EarningController : HomeController
    {
        private readonly IEarningService _earningService;
        private readonly IExpenseService<Expense> _expenseService;
        private readonly IMapper _mapper;

        public EarningController(IEarningService earningService, IExpenseService<Expense> expenseService, IMapper mapper)
        {
            _earningService = earningService;
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEarningById(Guid Id)
        {
            var earnings = await _earningService.GetEarningById(Id);

            if (earnings == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<EarningResponse>(earnings);
            return Ok(result);
        }

        [HttpGet("AndExpensesLastDays/{Id:guid}")]
        public async Task<IActionResult> GetEarningsAndExpensesOffLastSevenDays(Guid Id)
        {
            int days = 7;
            var earningsByDay = await _earningService.GetEarningsByUserByDays(Id, days);
            var expensesByDay = await _expenseService.GetExpensesByUserByDay(Id, days);
            var earningsAndExpenses = new Dictionary<DateTime, BudgetResume>();
            decimal earningValue, expenseValue;
            Console.WriteLine(earningsByDay.ToString());

            for (DateTime date = DateTime.Now.Date.AddDays(-days); date <= DateTime.Now; date = date.AddDays(1))
            {
                earningsByDay.TryGetValue(date, out earningValue);
                expensesByDay.TryGetValue(date, out expenseValue);

                earningsAndExpenses.Add(date, new BudgetResume
                {
                    TotalEarnings = earningValue,
                    TotalExpenses = expenseValue,
                });
            }

            return Ok(earningsAndExpenses);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EarningRequest earningReq)
        {
            var earning = _mapper.Map<Earning>(earningReq);
            var earningRes = await _earningService.RegisterEarning(earning);

            if (earningRes == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<EarningResponse>(earningRes);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid earningId)
        {
            var earning = await _earningService.DeleteEarning(earningId);
            var result = _mapper.Map<EarningResponse>(earning);
            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}")]
        public async Task<IActionResult> GetEarningsByUser(Guid UserId)
        {
            var earnings = await _earningService.GetEarningByUser(UserId);
            if(earnings == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<List<EarningResponse>>(earnings);
            return Ok(result);
        }

        [HttpGet("ByUser/{UserId:Guid}/{StartSearch:Datetime?}/{EndSearch:Datetime?}")]
        public async Task<IActionResult> GetEarningsByUser(Guid UserId, DateTime StartSearch, DateTime EndSearch)
        {
            var earnings = await _earningService.GetEarningByUser(UserId, StartSearch, EndSearch);
            if (earnings == null)
            {
                return NoContent();
            }
            var result = _mapper.Map<List<EarningResponse>>(earnings);
            return Ok(result);
        }

    }
}
