using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NETDatingApp.Models;

namespace NETDatingApp.Controllers
{
    public class MainUserPostController : Controller
    {
        // GET: MainUserPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(int id, string user, string comment) {
            ApplicationDbContext db = new ApplicationDbContext();
            UserPost up = new UserPost();
            up.Id = id;
            up.User = user;
            up.Comment = comment;
            int res = db.SaveChanges();
            if(res > 0) {
                List<UserPost> Lup = db.UserPost.ToList();
                return View(Lup);
            }

            return View();
        }
    }
}