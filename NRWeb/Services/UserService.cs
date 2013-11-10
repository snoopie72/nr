using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NRWeb.Context;
using NRWeb.Model;

namespace NRWeb.Services
{
    public class UserService:IUserService
    {
        private readonly NRDataContext _dataContext;

        public UserService(NRDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<User> GetAllUsers()
        {
            return _dataContext.Users.ToList();
        }

        public User GetUserByName(string name)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Name.Equals(name));
        }

        
    }
}