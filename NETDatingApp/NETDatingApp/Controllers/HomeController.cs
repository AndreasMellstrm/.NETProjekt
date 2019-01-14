using NETDatingApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace NETDatingApp.Controllers {
    public class HomeController : Controller {

        //Returnerar startsidan samt hämtar 3 stycken personProfile och skickar vidare dem till vyn
        public ActionResult Index() {
            ApplicationDbContext db = new ApplicationDbContext();
            return View(db.PersonProfiles.ToList().Take(3));
        }


        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}