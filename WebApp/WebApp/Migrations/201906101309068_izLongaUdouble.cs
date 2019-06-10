namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izLongaUdouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stations", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Stations", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stations", "Longitude", c => c.Long(nullable: false));
            AlterColumn("dbo.Stations", "Latitude", c => c.Long(nullable: false));
        }
    }
}
