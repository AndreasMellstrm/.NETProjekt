using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models {
    public class UserPostDBContext : DbContext {
        public DbSet<UserPostModels> UserPost { get; set; }

        public UserPostDBContext() : base("UserPostDb") { }
    }
}