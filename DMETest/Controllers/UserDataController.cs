using Core;
using Data;
using DMETest.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMETest.Controllers
{
    public class UserDataController : Controller
    {
        private IUserRepository _userRepository = new UserRepository();

        public ActionResult Index(Guid uid)
        {
            User userData =_userRepository.GetUser(uid);
            UserModel uData = new UserModel
            {
                firstName = userData.firstName,
                lastName = userData.lastName,
                phone = userData.phone,
                email = userData.email,
                mediumPhoto = userData.thumbnail,
                largePhoto = userData.largeSizePhoto
            };
            return View(uData);
        }
    }
}