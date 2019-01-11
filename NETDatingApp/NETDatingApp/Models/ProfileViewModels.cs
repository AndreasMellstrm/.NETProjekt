using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models {
    public class MyProfileViewModel {
        public PersonProfile Profile { get; set; }
    }

    public class ProfileViewModel {
        public PersonProfile Profile { get; set; }
        public FriendRelationship FriendRequest { get; set; }
    }

    public class ChangeProfileInfoViewModel {
        public PersonProfile Profile { get; set; }

        [Required]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Range(18, 130)]
        [Display(Name = "Ålder")]
        public int Age { get; set; }

        [Display(Name ="Bio")]
        public string Bio { get; set; }


        [Required]
        [Display(Name = "Kön")]
        public string Gender { get; set; }
    }

    public class FriendsListViewModel {
        public List<FriendRelationship> Friends { get; set; }
        public PersonProfile Profile { get; set; }

    }

    public class FriendRequestViewModel {
        public List<FriendRelationship> FriendRequests { get; set; }
    }
}