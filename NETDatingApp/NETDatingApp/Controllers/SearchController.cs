using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NETDatingApp.Models
{
    public class SearchController : Controller
    {
        public ApplicationDbContext ctx { get; set; }

        public SearchController()
        {
            ctx = new ApplicationDbContext();
        }

        // GET: Search
        public ActionResult Index(String searchString)
        {
            //return View(ctx.PersonProfiles.Where(s => (s.FirstName + ' ' + s.LastName).Contains(searchString) || searchString == null).ToList());
            var user = from p in ctx.PersonProfiles
                       select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                user = user.Where(s => (s.FirstName + ' ' + s.LastName).Contains(searchString));
            }

            return View(new SearchViewModels
            {
                ProfileList = user.ToList()
            });
        }

    }
}