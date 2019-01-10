using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NETDatingApp.Models {
    public class SearchController : Controller {
        public ApplicationDbContext ctx { get; set; }

        public SearchController() {
            ctx = new ApplicationDbContext();
        }

        // GET: Search
        public ActionResult Index(String searchString) {
            var profiles = (from p in ctx.PersonProfiles
                            where (p.FirstName + " " + p.LastName).Contains(searchString)
                            select p).ToList();

            return View(new SearchViewModels {
                ProfileList = profiles
            });
        }

    }
}