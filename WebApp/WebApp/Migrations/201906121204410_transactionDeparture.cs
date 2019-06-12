namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionDeparture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departures", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departures", "RowVersion");
        }
    }
}
