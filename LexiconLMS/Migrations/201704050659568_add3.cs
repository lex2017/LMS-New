namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "FileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "FileId");
        }
    }
}
