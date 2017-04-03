namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class document : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        Description = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        DeadlineDate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        CourseId = c.Int(),
                        ModuleId = c.Int(),
                        ActivityId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Activities", t => t.ActivityId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Modules", t => t.ModuleId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.CourseId)
                .Index(t => t.ModuleId)
                .Index(t => t.ActivityId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Documents", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Documents", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Documents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Documents", new[] { "ActivityId" });
            DropIndex("dbo.Documents", new[] { "ModuleId" });
            DropIndex("dbo.Documents", new[] { "CourseId" });
            DropTable("dbo.Documents");
        }
    }
}
