using System;
using System.Collections.Generic;
using System.IO;
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

        /*Tar in ett ProfileID och väljer ut en profil baserat på ProfileID samt en lista av vänförfrågningar. Returnerar sen vyn med en ProfileViewmodel
        som har profilen och listan med vänförfrågningar om listan innehåller några. Annars returnerar den vyn med endast en profil lagrad i ProfileViewModelen*/
        public ActionResult Profile(int ProfileID) {
            if (ProfileID != GetCurrentProfile().ProfileID)
            {
                var profiles = (from p in ctx.PersonProfiles
                                where p.ProfileID == ProfileID
                                select p).ToList();
                var profile = profiles[0];
                var currentProfile = GetCurrentProfile();
                var friendrequests = (from fr in ctx.FriendRelationships
                                      where (fr.RequesterID == currentProfile.ProfileID
                                      && fr.RecieverID == profile.ProfileID)
                                      || (fr.RequesterID == profile.ProfileID
                                      && fr.RecieverID == currentProfile.ProfileID)
                                      select fr).ToList();
                if (friendrequests.Count != 0)
                {
                    var friendrequest = friendrequests[0];
                    return View(new ProfileViewModel
                    {
                        Profile = profile,
                        FriendRequest = friendrequest
                    });
                }
                else
                {
                    return View(new ProfileViewModel
                    {
                        Profile = profile
                    });
                }
            }
            else
            {
                return RedirectToAction("MyProfile", "Profile");
            }
        }

        public ActionResult ChangeProfileInfo() {
            return View(new ChangeProfileInfoViewModel {
                Profile = GetCurrentProfile()
            });
        }

        //Metod för att ändra profilinfo
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
            profile.Bio = model.Bio;
            profile.Gender = model.Gender;
            await ctx.SaveChangesAsync();
            return RedirectToAction("MyProfile");
        }

        public ActionResult ChangeProfileImg() {
            return View();
        }

        //Metod för att ladda upp en bild i projektet samt ändra profilbilden på den inloggade användaren.
        [HttpPost]
        public async Task<ActionResult> ChangeProfileImg(HttpPostedFileBase file) {
            if (file != null && file.ContentLength > 0)
                try {
                    string path = Path.Combine(Server.MapPath("~\\Content\\Img"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    var Profile = GetCurrentProfile();
                    Profile.ProfileImg = Path.Combine("\\Content\\Img", Path.GetFileName(file.FileName));
                    await ctx.SaveChangesAsync();
                    return Redirect("MyProfile");
                }
                catch (Exception ex) {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

        /*Returnerar en FriendsList vy med en Lista av FriendRelationship och den
          inloggade användarens profil inskickat igenom FriendsListViewModel.*/
        public ActionResult FriendsList() {
            var profile = GetCurrentProfile();
            return View(new FriendsListViewModel {
                Profile = profile,
                Friends = (from fr in ctx.FriendRelationships
                           where fr.RequesterID == profile.ProfileID
                           || fr.RecieverID == profile.ProfileID
                           && fr.IsFriends == true
                           select fr).ToList()
            });
        }

        /*Om en användare är inloggad returneras en PartialView med en FriendRequestViewModel
        men om ingen användare är inloggad returnas en PartialView utan någon model.*/
        public ActionResult _LoginPartial() {
            if (User.Identity.IsAuthenticated) {
                var currentProfile = GetCurrentProfile();
                return PartialView(new FriendRequestViewModel {
                    FriendRequests = (from fr in ctx.FriendRelationships
                                      where fr.IsFriends == false
                                      where fr.RecieverID == currentProfile.ProfileID
                                      select fr).ToList()
                });
            }
            else {
                return PartialView();
            }
        }

        //Metod för att skicka en vänförfrågan.
        public async Task<ActionResult> SendFriendRequest(int ProfileID) {
            int RequestorID = GetCurrentProfile().ProfileID;
            var fr = new FriendRelationship {
                RequesterID = RequestorID,
                RecieverID = ProfileID,
                IsFriends = false
            };
            ctx.FriendRelationships.Add(fr);
            await ctx.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.AbsoluteUri); ;
        }

        //Metod för att acceptera en vänförfrågan. 
        public async Task<ActionResult> AcceptFriendRequest(int RequesterID) {
            int RecieverID = GetCurrentProfile().ProfileID;
            var friendRelationships = (from fr in ctx.FriendRelationships
                                      where fr.RequesterID == RequesterID
                                      where fr.RecieverID == RecieverID
                                      select fr).ToList();
            var friendRelationship = friendRelationships[0];
            friendRelationship.IsFriends = true;
            await ctx.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        //Metod för att avböja en vänförfrågan
        public async Task<ActionResult> DeclineFriendRequest(int RequesterID) {
            int RecieverID = GetCurrentProfile().ProfileID;
            var friendRelationships = (from fr in ctx.FriendRelationships
                                       where fr.RequesterID == RequesterID
                                       where fr.RecieverID == RecieverID
                                       select fr).ToList();
            var friendRelationship = friendRelationships[0];
            ctx.FriendRelationships.Remove(friendRelationship);
            await ctx.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }


    }
}