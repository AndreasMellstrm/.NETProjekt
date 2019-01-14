using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using NETDatingApp.Models;

namespace NETDatingApp.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostApiController : ApiController
    {

        public ApplicationDbContext ctx { get; set; }

        public PostApiController() {
            ctx = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("Get")]
        public Post[] ShowPosts(int id)
        {
            var Posts = (from P in ctx.Posts
                         where P.RecieverID == id
                         orderby P.ID descending
                         select P).ToArray();

            return Posts;

        }

        [HttpPost]
        [Route("Send")]
        public void SendPost(string message,int recieverID, int senderID) {

            var post = new Post {
                Message = message,
                RecieverID = recieverID,
                SenderID = senderID
            };
            ctx.Posts.Add(post);
            ctx.SaveChanges();            
        }
    }
}
