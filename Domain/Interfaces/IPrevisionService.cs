using Domain.Entities.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPrevisionService
    {
        Task<Prevision> CreatePrevision(Prevision prevision);
        Task<Prevision> DeletePrevision(Prevision prevision);
        Task<List<Prevision>> GetAllPrevisionsByUser(Guid userId);
    }
}
