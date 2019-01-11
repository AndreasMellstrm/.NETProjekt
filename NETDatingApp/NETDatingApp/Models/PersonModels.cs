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
        
        [InverseProperty("FriendRequester")]
        public virtual ICollection<FriendRelationship> SentRequest { get; set; }

        [InverseProperty("FriendReciever")]
        public virtual ICollection<FriendRelationship> RecievedRequest{ get; set; }

        [InverseProperty("PostSender")]
        public virtual ICollection<Post> SentPosts { get; set; }

         [InverseProperty("PostReciever")]
        public virtual ICollection<Post> RecievedPosts { get; set; }


        public PersonProfile() {

        }
        public PersonProfile(string FirstName, string LastName, string Gender, int Age, string Bio = "Biografi saknas", string ProfileImg = @"/Content/img/blankProfile.png") {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.Age = Age;
            this.Bio = Bio;
            this.ProfileImg = ProfileImg;
            ApplicationUsers = new HashSet<ApplicationUser>();
            SentRequest = new HashSet<FriendRelationship>();
            RecievedRequest = new HashSet<FriendRelationship>();

        }

    }

    public class FriendRelationship {

        [Key, Column(Order = 0)]
        public int RequesterID { get; set; }

        [Key, Column(Order = 1)]
        public int RecieverID { get; set; }

        [ForeignKey("RequesterID")]
        [InverseProperty("SentRequest")]
        public virtual PersonProfile FriendRequester { get; set; }

        [ForeignKey("RecieverID")]
        [InverseProperty("RecievedRequest")]
        public virtual PersonProfile FriendReciever { get; set; }

        public bool IsFriends { get; set; }

    }
    

}
