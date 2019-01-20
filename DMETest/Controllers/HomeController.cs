using Core;
using Data;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMETest.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userRepository = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetUserList(int pageNum = 1, string sortBy = "id", string isDesc = "false", string fName = null)
        {
            int skipC = pageNum*10 - 10;
            fName = fName == "null" ? null : fName; 
            IEnumerable<Core.User> userList = _userRepository.GetUsers(skipC, 10, sortBy, Convert.ToBoolean(isDesc));
            if (fName != null)
            {
                userList = _userRepository.FindUserByName(fName, skipC, 10, sortBy, Convert.ToBoolean(isDesc));
            }
            return PartialView(userList);
        }

        public string authLink(User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return "<a href='huy'>" + user.lastName + user.firstName + "</a>";
            }
            else
            {
                return "<a>" + user.lastName + user.firstName + "</a>";
            }
        }

    }
}