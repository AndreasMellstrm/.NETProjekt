namespace NETDatingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPostModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserPost");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPost",
                c => new
                    {
                        Comment = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Comment);
            
        }
    }
}
