namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timestampforlines : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "RowVersion");
        }
    }
}
