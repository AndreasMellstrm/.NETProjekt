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

        public ActionResult Profile(int ProfileID) {
            var profiles = (from p in ctx.PersonProfiles
                            where p.ProfileID == ProfileID
                            select p).ToList();
            var profile = profiles[0];
            var currentProfile = GetCurrentProfile();
            var friendrequests = (from fr in ctx.FriendRelationships
                                  where (fr.ProfileAId == currentProfile.ProfileID
                                  && fr.ProfileBId == profile.ProfileID) 
                                  || (fr.ProfileAId == profile.ProfileID
                                  && fr.ProfileBId == currentProfile.ProfileID)
                                  select fr).ToList();
            if(friendrequests.Count != 0){
                var friendrequest = friendrequests[0];
                return View(new ProfileViewModel {
                    Profile = profile,
                    FriendRequest = friendrequest
                });
            }
            else {
                return View(new ProfileViewModel {
                    Profile = profile
                });
            }
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
            profile.Bio = model.Bio;
            profile.Gender = model.Gender;
            await ctx.SaveChangesAsync();
            return RedirectToAction("MyProfile");
        }

        public ActionResult ChangeProfileImg() {
            return View();
        }

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

        public ActionResult FriendsList() {
            var profile = GetCurrentProfile();
            return View(new FriendsListViewModel {
                Profile = profile,
                Friends = (from fr in ctx.FriendRelationships
                           where fr.ProfileAId == profile.ProfileID
                           || fr.ProfileBId == profile.ProfileID
                           && fr.IsFriends == true
                           select fr).ToList()
            });
        }

        public ActionResult _LoginPartial() {
            if (User.Identity.IsAuthenticated) {
                var currentProfile = GetCurrentProfile();
                return PartialView(new FriendRequestViewModel {
                    FriendRequests = (from fr in ctx.FriendRelationships
                                      where fr.IsFriends == false
                                      where fr.ProfileBId == currentProfile.ProfileID
                                      select fr).ToList()
                });
            }
            else {
                return PartialView();
            }
        }

        
        public async Task<ActionResult> SendFriendRequest(int ProfileID) {
            int RequestorID = GetCurrentProfile().ProfileID;
            var fr = new FriendRelationship {
                ProfileAId = RequestorID,
                ProfileBId = ProfileID,
                IsFriends = false
            };
            ctx.FriendRelationships.Add(fr);
            await ctx.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.AbsoluteUri); ;
        }

        public async Task<ActionResult> AcceptFriendRequest(int ProfileAId) {
            int ProfileBId = GetCurrentProfile().ProfileID;
            var friendRelationships = (from fr in ctx.FriendRelationships
                                      where fr.ProfileAId == ProfileAId
                                      where fr.ProfileBId == ProfileBId
                                      select fr).ToList();
            var friendRelationship = friendRelationships[0];
            friendRelationship.IsFriends = true;
            await ctx.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public async Task<ActionResult> DeclineFriendRequest(int ProfileAId) {
            int ProfileBId = GetCurrentProfile().ProfileID;
            var friendRelationships = (from fr in ctx.FriendRelationships
                                       where fr.ProfileAId == ProfileAId
                                       where fr.ProfileBId == ProfileBId
                                       select fr).ToList();
            var friendRelationship = friendRelationships[0];
            ctx.FriendRelationships.Remove(friendRelationship);
            await ctx.SaveChangesAsync();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }


    }
}