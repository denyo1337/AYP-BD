using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Interfaces
{
    public interface IUsersRepostiory : IRepository<User>
    {
        Task<bool> AddUser(User user);
    }
}
