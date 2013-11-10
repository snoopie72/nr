using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRWeb.Model;

namespace NRWeb.Services
{
    public interface IUserService
    {
        ICollection<User> GetAllUsers();
        User GetUserByName(string name);
        
    }
}
