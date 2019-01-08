using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models {

    [Table("UserPost")]
    public class UserPostModel {
        [Key]
        public int Id { get; set; } = 1;
        public string User { get; set; }
        public string Comment { get; set; }
        //public string Img { get; set; }

    }
}