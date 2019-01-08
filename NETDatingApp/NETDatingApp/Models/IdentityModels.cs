using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NETDatingApp.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {

        public int ProfileID { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public DbSet<UserPost> UserPost { get; set; }
        public DbSet<PersonProfile> PersonProfiles { get; set; }
        public DbSet<FriendRelationship> FriendRelationships { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
           base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FriendRelationship>().HasRequired(c => c.ProfileA).WithMany(u => u.ProfileAFriendRelationship).HasForeignKey(c => c.ProfileAId).WillCascadeOnDelete(false);
            modelBuilder.Entity<FriendRelationship>().HasRequired(c => c.ProfileB).WithMany(u => u.ProfileBFriendRelationship).HasForeignKey(c => c.ProfileBId).WillCascadeOnDelete(false);
        }
       
        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}
