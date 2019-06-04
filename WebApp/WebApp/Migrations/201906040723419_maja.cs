namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maja : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PricePriceLists", newName: "PriceListPrices");
            DropPrimaryKey("dbo.PriceListPrices");
            AddPrimaryKey("dbo.PriceListPrices", new[] { "PriceList_IDPriceList", "Price_IDPrice" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PriceListPrices");
            AddPrimaryKey("dbo.PriceListPrices", new[] { "Price_IDPrice", "PriceList_IDPriceList" });
            RenameTable(name: "dbo.PriceListPrices", newName: "PricePriceLists");
        }
    }
}
