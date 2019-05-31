namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeOfLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeOfLines",
                c => new
                    {
                        IDtypeOfLine = c.Int(nullable: false, identity: true),
                        typeOfLine = c.String(),
                    })
                .PrimaryKey(t => t.IDtypeOfLine);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TypeOfLines");
        }
    }
}
