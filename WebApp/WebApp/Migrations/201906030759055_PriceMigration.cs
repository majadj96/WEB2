namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceLists",
                c => new
                    {
                        IDPriceList = c.Int(nullable: false, identity: true),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDPriceList);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        IDPrice = c.Int(nullable: false, identity: true),
                        IDtypeOfTicket = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IDPrice);
            
            CreateTable(
                "dbo.PricePriceLists",
                c => new
                    {
                        Price_IDPrice = c.Int(nullable: false),
                        PriceList_IDPriceList = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Price_IDPrice, t.PriceList_IDPriceList })
                .ForeignKey("dbo.Prices", t => t.Price_IDPrice, cascadeDelete: true)
                .ForeignKey("dbo.PriceLists", t => t.PriceList_IDPriceList, cascadeDelete: true)
                .Index(t => t.Price_IDPrice)
                .Index(t => t.PriceList_IDPriceList);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PricePriceLists", "PriceList_IDPriceList", "dbo.PriceLists");
            DropForeignKey("dbo.PricePriceLists", "Price_IDPrice", "dbo.Prices");
            DropIndex("dbo.PricePriceLists", new[] { "PriceList_IDPriceList" });
            DropIndex("dbo.PricePriceLists", new[] { "Price_IDPrice" });
            DropTable("dbo.PricePriceLists");
            DropTable("dbo.Prices");
            DropTable("dbo.PriceLists");
        }
    }
}
