using Domain.Entities.Budget;

namespace Domain.Interfaces
{
    public interface IEarningService
    {
        Task<Earning> RegisterEarning(Earning earning);
        Task<List<Earning>> GetEarningByUser(Guid driver);
        Task<List<Earning>> GetEarningByUser(Guid driver, DateTime StartSearch, DateTime EndSearch);
        Task<Earning> DeleteEarning(Guid Id);
    }
}
