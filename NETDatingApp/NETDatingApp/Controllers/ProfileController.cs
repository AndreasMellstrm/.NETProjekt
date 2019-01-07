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
        public ApplicationDbContext ctx { get; set; }

        public ProfileController() {
            ctx = new ApplicationDbContext();
        }

        public PersonProfile GetCurrentProfile() {
            var userId = User.Identity.GetUserId();
            var profiles = (from p in ctx.PersonProfiles
                            join user in ctx.Users on p.ProfileID equals user.ProfileID
                            where user.Id == userId
                            select p).ToList();
            return profiles[0];

        }

        public ActionResult MyProfile() {
            return View(new MyProfileViewModel {
                Profile = GetCurrentProfile()
            });
        }
        public ActionResult ChangeProfileInfo() {
            return View(new ChangeProfileInfoViewModel {
                Profile = GetCurrentProfile()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfileInfo(ChangeProfileInfoViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var profile = GetCurrentProfile();
            profile.FirstName = model.FirstName;
            profile.LastName = model.LastName;
            profile.Age = model.Age;
            profile.Gender = model.Gender;
            await ctx.SaveChangesAsync();
            return RedirectToAction("MyProfile");
        }


    }
}