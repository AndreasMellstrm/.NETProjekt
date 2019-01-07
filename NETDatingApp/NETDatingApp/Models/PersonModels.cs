using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
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
        
        [InverseProperty("ProfileA")]
        public virtual ICollection<Friends> ProfileAFriends { get; set; }

        [InverseProperty("ProfileB")]
        public virtual ICollection<Friends> ProfileBFriends { get; set; }


        public PersonProfile() {

        }
        public PersonProfile(string FirstName, string LastName, string Gender, int Age, string Bio = "Biografi saknas", string ProfileImg = "") {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.Age = Age;
            this.Bio = Bio;
            this.ProfileImg = ProfileImg;
            ApplicationUsers = new HashSet<ApplicationUser>();
            ProfileAFriends = new HashSet<Friends>();
            ProfileBFriends = new HashSet<Friends>();

        }

    }

    public class Friends {

        [Key, Column(Order = 0)]
        public int ProfileAId { get; set; }

        [Key, Column(Order = 1)]
        public int ProfileBId { get; set; }

        [ForeignKey("ProfileAId")]
        [InverseProperty("ProfileAFriends")]
        public virtual PersonProfile ProfileA { get; set; }

        [ForeignKey("UserBId")]
        [InverseProperty("ProfileBFriends")]
        public virtual PersonProfile ProfileB { get; set; }

        public bool IsFriends { get; set; }

    }
    

}
