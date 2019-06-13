namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proveraaaa : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lines", "maja");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lines", "maja", c => c.Binary());
        }
    }
}
