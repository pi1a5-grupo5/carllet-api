﻿using Application.ViewModels.Earning;
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

        //[HttpGet("AndExpensesLastDays/{Id:guid}")]
        //public async Task<IActionResult> GetEarningsAndExpensesOffLastSevenDays(Guid Id)
        //{
        //    int days = 7;
        //    var earningsByDay = await _earningService.GetEarningsByUserByDays(Id, days);
        //    var expensesByDay = await _expenseService.GetExpensesByUserByDay(Id, days);

        //    var allDates = 

        //    var summaryByDay = allDates.ToDictionary(
        //        date => date,
        //        date => (
        //            Earnings: earningsByDay.TryGetValue(date, out double earningValue) ? earningValue : 0,
        //            Expenses: expensesByDay.TryGetValue(date, out decimal expenseValue) ? expenseValue : 0
        //        )
        //    );

        //    if (summaryByDay.Count == 0)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(summaryByDay);
        //}

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

    }
}
