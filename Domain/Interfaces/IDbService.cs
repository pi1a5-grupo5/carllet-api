using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDbService
    {
        Task<T> GetAsync<T>(string command, object parameters);
        Task<List<T>> GetAll<T>(string command, object parameters);
        Task<int> EditData(string command, object parameters);
    }
}
