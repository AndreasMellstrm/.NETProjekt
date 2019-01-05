using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models
{
    public class MyProfileViewModel
    {
        public string UserId { get; set; }
        
        public PersonProfile Profile { get; set; }
    }

    public class ProfileViewModel {

    }

    public class ChangeProfileInfoViewModel {
        public string UserId { get; set; }

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

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
    }
}