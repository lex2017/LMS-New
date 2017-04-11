namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        ActivityTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeID)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId)
                .Index(t => t.ActivityTypeID);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        ActivityTypeID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.ActivityTypeID);
            
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
                        UserId = c.String(),
                        CourseId = c.Int(),
                        ModuleId = c.Int(),
                        ActivityId = c.Int(),
                        FileId = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleID)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CourseId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Documents", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Documents", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "ActivityTypeID", "dbo.ActivityTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CourseId" });
            DropIndex("dbo.Modules", new[] { "CourseId" });
            DropIndex("dbo.Documents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Documents", new[] { "ActivityId" });
            DropIndex("dbo.Documents", new[] { "ModuleId" });
            DropIndex("dbo.Documents", new[] { "CourseId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeID" });
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Modules");
            DropTable("dbo.Courses");
            DropTable("dbo.Documents");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Activities");
        }
    }
}
