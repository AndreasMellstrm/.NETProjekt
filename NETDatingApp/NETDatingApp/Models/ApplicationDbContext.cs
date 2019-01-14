using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NETDatingApp.Models {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public DbSet<PersonProfile> PersonProfiles { get; set; }
        public DbSet<FriendRelationship> FriendRelationships { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        //Överlagring av metoden OnModelCreating för att skapa främmande nycklar i tabellerna FriendRelationships och Posts
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FriendRelationship>().HasRequired(c => c.FriendRequester).WithMany(u => u.SentRequest).HasForeignKey(c => c.RequesterID).WillCascadeOnDelete(false);
            modelBuilder.Entity<FriendRelationship>().HasRequired(c => c.FriendReciever).WithMany(u => u.RecievedRequest).HasForeignKey(c => c.RecieverID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasRequired(c => c.PostSender).WithMany(u => u.SentPosts).HasForeignKey(c => c.SenderID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasRequired(c => c.PostReciever).WithMany(u => u.RecievedPosts).HasForeignKey(c => c.RecieverID).WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();

        }
    }
}