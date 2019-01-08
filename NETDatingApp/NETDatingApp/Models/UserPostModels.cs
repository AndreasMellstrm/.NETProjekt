using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models {
    public class UserPostModels {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
    }
}