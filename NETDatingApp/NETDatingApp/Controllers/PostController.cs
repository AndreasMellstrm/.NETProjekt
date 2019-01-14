using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using NETDatingApp.Models;

namespace NETDatingApp.Controllers
{
    public class PostController : Controller
    {
        public ApplicationDbContext ctx { get; set; }

        public PostController()
        {
            ctx = new ApplicationDbContext();
        }

        public ActionResult ShowPosts(string recieverID)
        {
            int RecieverID = int.Parse(recieverID);
            var profiles = (from p in ctx.PersonProfiles
                            where p.ProfileID == RecieverID
                            select p).ToList();
            var profile = profiles[0];

            return PartialView(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendPost(SendPostViewModel model)
        {
            var Post = new Post
            {
                Message = model.Message,
                RecieverID = model.RecieverID,
                SenderID = model.SenderID
            };
            ctx.Posts.Add(Post);
            ctx.SaveChanges();

            return View();
        }

        
    }
}