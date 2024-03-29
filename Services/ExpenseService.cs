﻿using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Entities.Budget.Expenses;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class ExpenseService : IExpenseService<Expense>
    {
        private readonly CarlletDbContext _dbContext;

        public ExpenseService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Expense> DeleteExpense(Guid ExpenseId)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(e => e.ExpenseId == ExpenseId);

            if (expense == null)
            {
                return null;
            }

            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();
            return expense;
        }

        public async Task<Expense> GetExpense(Guid ExpenseId)
        {
            var expense = _dbContext.Expenses.Find(ExpenseId);
            return expense;
        }

        public async Task<List<Expense>> GetExpensesList()
        {
            var expenses = _dbContext.Expenses
                .ToList();
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserId(Guid driver)
        {
            var expenses = _dbContext.Expenses
            .Include(fe => ((FuelExpense)fe).FuelExpenseType)
            .Include(me => ((MaintenanceExpense)me).MaintenanceExpenseType)
            .Include(oe => ((OtherExpense)oe).OtherExpenseType)
            .Where(e => e.UserVehicle.UserId == driver).ToList();


            //var expenses = _dbContext.UserVehicles
            //.Where(uv => uv.UserId == driver)
            //.SelectMany(uv => uv.Expenses)
            //.Include(fe => (fe as FuelExpense).FuelExpenseType)
            //.Include(me => (me as MaintenanceExpense).MaintenanceExpenseType)
            //.Include(oe => (oe as OtherExpense).OtherExpenseType)
            //.ToList();

            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserId(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses =  _dbContext.Expenses
                .Where(e => e.UserVehicle.UserId == driver && e.ExpenseDate >= StartSearch && e.ExpenseDate <= EndSearch)
                .ToList();

            if (expenses == null || expenses.Count == 0)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserVehicleId(Guid userVehicleId)
        {
            var expenses = _dbContext.Expenses.Where(e => e.UserVehicleId == userVehicleId).ToList();
            if (expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public async Task<List<Expense>> GetExpenseByUserVehicleId(Guid UserVehicleId, DateTime StartSearch, DateTime EndSearch)
        {
            var expenses = _dbContext.Expenses.Where(u => u.UserVehicleId == UserVehicleId
                && u.ExpenseDate <= StartSearch
                && u.ExpenseDate >= EndSearch).ToList();

            if (expenses == null || expenses.Count == 0)
            {
                return null;
            }
            return expenses;
        }

        public async Task<Expense> RegisterExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();
            return expense;
        }

        public Task<Expense> UpdateExpense(Expense expense)
        {
            throw new NotImplementedException();
        }

        public void AddExpenseType<U>(U expense) where U : ExpenseType
        {
            throw new NotImplementedException();
        }

        public Task<List<U>> GetExpenseTypes<U>() where U : ExpenseType
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<DateTime, decimal>> GetExpensesByUserByDay(Guid userId, int days)
        {
            var date = DateTime.Now.AddDays(-days).Date;
            var expenses = _dbContext.Expenses
                .Where(e => e.UserVehicle.UserId == userId && e.ExpenseDate >= date)
                .ToList();

            var groupedExpenses = expenses
                .GroupBy(e => e.ExpenseDate.Date)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Value));

            return groupedExpenses;
        }
    }
}
