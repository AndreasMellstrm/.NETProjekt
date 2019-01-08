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
            try { 
            ApplicationDbContext db = new ApplicationDbContext();
            UserPostModel up = new UserPostModel();
            up.Id = id;
            up.User = user;
            up.Comment = comment;
            db.UserPosts.Add(up);
            int res = db.SaveChanges();
            if(res > 0) {
                List<UserPostModel> Lup = db.UserPosts.ToList();
                return View(Lup);
            }

            return View();
        }
    }
}