using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class EarningService : IEarningService
    {

        private readonly CarlletDbContext _dbContext;
        public EarningService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Earning> DeleteEarning(Guid Id)
        {
            var earning = _dbContext.Earnings.Where(u => u.Id == Id).FirstOrDefault();

            if (earning == null)
            {
                return null;
            }

            var deletedEarning = _dbContext.Earnings.Remove(earning);
            _dbContext.SaveChanges();

            return earning; 
        }

        public async Task<Earning> GetEarningById(Guid id)
        {
            var earning = _dbContext.Earnings.FirstOrDefault(e => e.Id == id);
            return earning;
        }

        public async Task<List<Earning>> GetEarningByUser(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            var earnings = _dbContext.Earnings.Where(u => u.OwnerId == driver
            && u.InsertionDateTime >= StartSearch
            && u.InsertionDateTime <= EndSearch).ToList();

            if (earnings == null || earnings.Count == 0)
            {
                return null;
            }

            return earnings;
        }

        public async Task<List<Earning>> GetEarningByUser(Guid driver)
        {
            var earnings = _dbContext.Earnings.Where(u => u.OwnerId == driver).ToList();

            if (earnings == null || earnings.Count == 0)
            {
                return null;
            }

            return earnings;
        }

        public async Task<Dictionary<DateTime, decimal>> GetEarningsByUserByDays(Guid driver, int days)
        {
            var date = DateTime.Now.AddDays(-days);
            var earnings = _dbContext.Earnings
                .Where(u => u.OwnerId == driver)
                .ToList();

            var groupedEarnings = earnings
                .GroupBy(e => e.InsertionDateTime.Date)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.EarningValue));

            return groupedEarnings;
        }

        public async Task<Earning> RegisterEarning(Earning earning)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == earning.OwnerId);

            earning.Owner = user;

            var setEarning = _dbContext.Earnings.Add(earning);
            _dbContext.SaveChanges();

            if (setEarning == null)
            {
                return null;
            }

            return earning;
        }

        public async Task<Earning> UpdateEarning(Earning earning)
        {
            var earningToUpdate = _dbContext.Earnings.Find(earning.Id);

            if (earningToUpdate == null)
            {
                return null;
            }

            _dbContext.Entry(earningToUpdate).CurrentValues.SetValues(earning);
            await _dbContext.SaveChangesAsync();
            return earningToUpdate;
        }


    }
}
