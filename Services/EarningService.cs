using Domain.Entities;
using Domain.Entities.Budget;
using Domain.Interfaces;
using Infra.Data;

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
            throw new NotImplementedException();
        }

        public async Task<Earning> GetEarningById(Guid id)
        {
            var earning = _dbContext.Earning.FirstOrDefault(e => e.Id == id);
            return earning;
        }

        public async Task<List<Earning>> GetEarningByUser(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            var earnings = _dbContext.Earning.Where(u => u.OwnerId == driver 
            && u.InsertionDateTime <= StartSearch 
            && u.InsertionDateTime >= EndSearch).ToList();

            if (earnings == null || earnings.Count == 0)
            {
                return null;
            }

            return earnings;
        }

        public async Task<List<Earning>> GetEarningByUser(Guid driver)
        {
            var earnings = _dbContext.Earning.Where(u => u.OwnerId == driver).ToList();

            if (earnings == null || earnings.Count == 0)
            {
                return null;
            }

            return earnings;
        }

        public async Task<Earning> RegisterEarning(Earning earning)
        {
            User user = _dbContext.User.FirstOrDefault(u => u.Id == earning.OwnerId);

            earning.Owner = user;

            var setEarning = _dbContext.Earning.Add(earning);

            if (setEarning == null)
            {
                return null;
            }

            return earning;
        }

        public async Task<Earning> UpdateEarning(Earning earning)
        {
            var earningToUpdate = _dbContext.Earning.Find(earning.Id);

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
