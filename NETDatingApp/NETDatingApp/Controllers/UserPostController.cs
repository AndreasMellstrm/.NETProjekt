using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NETDatingApp.Models;


namespace NETDatingApp.Controllers
{
    public class UserPostController : Controller
    {
        // GET: UserPost
        public ActionResult Index()
        {

            var ctx = new UserPostDBContext();
            var viewModel = new UserPostIndexViewModel {
                UserPost = ctx.UserPost.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddUserPost(UserPostModels Model) {
            var ctx = new UserPostDBContext();
            ctx.UserPost.Add(Model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}