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
        public PersonProfile CurrentProfile { get; set; }
        public PersonProfile TargetedProfile { get; set; }
        public FriendRelationship FriendRequest { get; set; }
    }

    public class ChangeProfileInfoViewModel {
        public PersonProfile Profile { get; set; }

        [Required]
        [Display(Name = "Förnamn")]
        [RegularExpression("([a-öA-Ö0-9 .&'-]+)", ErrorMessage = "Vänligen använd endast giltiga tecken")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        [RegularExpression("([a-öA-Ö0-9 .&'-]+)", ErrorMessage = "Vänligen använd endast giltiga tecken")]
        public string LastName { get; set; }

        [Range(18, 130)]
        [Display(Name = "Ålder")]
        public int Age { get; set; }

        [Display(Name ="Bio")]
        [RegularExpression("([a-öA-Ö0-9 .&'-]+)", ErrorMessage = "Vänligen använd endast giltiga tecken")]
        public string Bio { get; set; }


        [Required]
        [Display(Name = "Kön")]
        [RegularExpression("([a-öA-Ö0-9 .&'-]+)", ErrorMessage = "Vänligen använd endast giltiga tecken")]
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