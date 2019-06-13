namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proveraa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "maja", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "maja");
        }
    }
}
