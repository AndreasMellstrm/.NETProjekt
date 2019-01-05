using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace NETDatingApp.Models {
    public class PersonProfile {

        [Key]
        public int ProfileID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; }
        public string ProfileImg { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public PersonProfile(string FirstName, string LastName, string Gender, int Age, string Bio = "Biografi saknas", string ProfileImg = "") {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.Age = Age;
            this.Bio = Bio;
            this.ProfileImg = ProfileImg;
            ApplicationUsers = new HashSet<ApplicationUser>();
        }
    }
}
