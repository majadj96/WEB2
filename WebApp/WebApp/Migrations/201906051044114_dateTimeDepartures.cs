namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateTimeDepartures : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departures", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departures", "Time", c => c.String());
        }
    }
}
