namespace NETDatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hej : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonProfiles",
                c => new
                    {
                        ProfileID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        Age = c.Int(nullable: false),
                        Bio = c.String(),
                        ProfileImg = c.String(),
                    })
                .PrimaryKey(t => t.ProfileID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonProfiles");
        }
    }
}
