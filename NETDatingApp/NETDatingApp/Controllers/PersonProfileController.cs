using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NETDatingApp.Models;

namespace NETDatingApp.Controllers
{
    public class PersonProfileController : Controller
    {
        // GET: PersonProfile
        public ActionResult Index()
        {
            var ctx = new ApplicationDbContext();
            //var viewModel = new PersonProfile();
            return View();
        }
    }
}