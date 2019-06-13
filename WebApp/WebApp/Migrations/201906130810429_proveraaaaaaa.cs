namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proveraaaaaaa : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Prices", "RowVersion");
            DropColumn("dbo.Lines", "RowVersion");

        }

        public override void Down()
        {
            AddColumn("dbo.Prices", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Lines", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));

        }
    }
}
