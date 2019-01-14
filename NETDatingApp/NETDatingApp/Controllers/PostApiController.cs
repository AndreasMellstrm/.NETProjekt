using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NETDatingApp.Models;

namespace NETDatingApp.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostApiController : ApiController
    {
        [HttpGet]
        [Route("")]
        public Post[] ShowPosts(int id)
        {
            var ctx = new ApplicationDbContext();
            var Posts = (from P in ctx.Posts
                         where P.RecieverID == id
                         orderby P.ID descending
                         select P).ToArray();

            return Posts;

        }

        /*[HttpGet]
        [Route("")]
        public ActionResult SendPost(string RecieverID, string SenderID)
        {
            int Reciever = int.Parse(RecieverID);
            int Sender = int.Parse(SenderID);
            return View(new SendPostViewModel
            {
                RecieverID = Reciever,
                SenderID = Sender
            });
        }*/
    }
}
