using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var profiles = (from p in ctx.PersonProfiles
                        join user in ctx.Users on p.ProfileID equals user.ProfileID
                        where user.Id == userId
                        select p).ToList();
            return View(new MyProfileViewModel {
                UserId = userId,
                Profile = profiles[0]
            });
        }
        public ActionResult ChangePersonalInformation() {
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var profiles = (from p in ctx.PersonProfiles
                            join user in ctx.Users on p.ProfileID equals user.ProfileID
                            where user.Id == userId
                            select p).ToList();
            return View(new ChangeProfileInfoViewModel {
                UserId = userId,
                Profile = profiles[0]
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfileInfo(ChangeProfileInfoViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            model.Profile.FirstName = model.FirstName;
            model.Profile.LastName = model.LastName;
            model.Profile.Age = model.Age;
            model.Profile.Gender = model.Gender;

            return RedirectToAction("MyProfile");
        }


    }
}