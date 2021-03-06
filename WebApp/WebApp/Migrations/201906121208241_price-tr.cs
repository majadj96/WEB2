namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pricetr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prices", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prices", "RowVersion");
        }
    }
}
