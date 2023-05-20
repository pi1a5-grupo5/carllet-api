using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> GetUser(int id);
        Task<List<User>> GetUserList();
        Task<User> DeleteUser(int id);
    }
}
