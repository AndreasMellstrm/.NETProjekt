using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NETDatingApp.Models;

namespace NETDatingApp.Controllers
{
    public class ProfileController : Controller
    {

        
        public ActionResult MyProfile() {
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var query = from p in ctx.PersonProfiles
                        where 
            return View(new MyProfileViewModel {
                UserId = userId,
                Profile = 
            });
        }
        public ActionResult ChangePersonalInformation(ChangeProfileInfoViewModel model) {
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            return View( new ChangeProfileInfoViewModel 
            {
            });
        }


       
       
    }
}