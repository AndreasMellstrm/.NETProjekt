using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Inlägg")]
        public string Message { get; set; }

        public int SenderID { get; set; }
        public int RecieverID { get; set; }

        [ForeignKey("SenderID")]
        [InverseProperty("SentPosts")]
        public virtual PersonProfile SenderProfile { get; set; }

        [ForeignKey("RecieverID")]
        [InverseProperty("RecievedPosts")]
        public virtual PersonProfile RecieverProfile { get; set; }
    }
}
        
        