using Core;
using Data;
using DMETest.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DMETest.Controllers
{
    public class AuthController : Controller
    {
        private IUserRepository userRepository = new UserRepository();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            User authUser = null;
            authUser = userRepository.CheckUser(model.email, model.password);
            if(authUser!= null)
            {
                FormsAuthentication.SetAuthCookie(authUser.email, true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}