namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asistent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "Version");
        }
    }
}
