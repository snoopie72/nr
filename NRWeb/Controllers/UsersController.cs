using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NRWeb.Services;

namespace NRWeb.Controllers
{
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //
        // GET: /Users/
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        
    }
}
