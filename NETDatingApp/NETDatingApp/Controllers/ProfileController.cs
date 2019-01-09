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

        [HttpPost]
        public async Task<ActionResult> SendFriendRequest(int ReceiverID) {
            int RequestorID = GetCurrentProfile().ProfileID;
            var fr = new FriendRelationship {
                ProfileAId = RequestorID,
                ProfileBId = ReceiverID,
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