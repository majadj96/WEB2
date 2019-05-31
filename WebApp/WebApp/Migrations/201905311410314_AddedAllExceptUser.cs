namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAllExceptUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        IDDay = c.Int(nullable: false, identity: true),
                        KindOfDay = c.String(),
                    })
                .PrimaryKey(t => t.IDDay);
            
            CreateTable(
                "dbo.Departures",
                c => new
                    {
                        IDDeparture = c.Int(nullable: false, identity: true),
                        IDDay = c.Int(nullable: false),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.IDDeparture)
                .ForeignKey("dbo.Days", t => t.IDDay, cascadeDelete: true)
                .Index(t => t.IDDay);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        Number = c.String(nullable: false, maxLength: 128),
                        IDtypeOfLine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Number);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        Latitude = c.Long(nullable: false),
                        Longitude = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        IDticket = c.Int(nullable: false, identity: true),
                        BoughtTime = c.DateTime(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        IDtypeOfTicket = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.IDticket)
                .ForeignKey("dbo.TypeOfTickets", t => t.IDtypeOfTicket, cascadeDelete: true)
                .Index(t => t.IDtypeOfTicket);
            
            CreateTable(
                "dbo.TypeOfTickets",
                c => new
                    {
                        IDtypeOfTicket = c.Int(nullable: false, identity: true),
                        typeOfTicket = c.String(),
                    })
                .PrimaryKey(t => t.IDtypeOfTicket);
            
            CreateTable(
                "dbo.TypeOfUsers",
                c => new
                    {
                        IDtypeOfUser = c.Int(nullable: false, identity: true),
                        typeOfUser = c.String(),
                        Percentage = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.IDtypeOfUser);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        IDvehicle = c.Int(nullable: false, identity: true),
                        RegistrationPlates = c.String(),
                        Number = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IDvehicle)
                .ForeignKey("dbo.Lines", t => t.Number)
                .Index(t => t.Number);
            
            CreateTable(
                "dbo.LineDepartures",
                c => new
                    {
                        Line_Number = c.String(nullable: false, maxLength: 128),
                        Departure_IDDeparture = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Line_Number, t.Departure_IDDeparture })
                .ForeignKey("dbo.Lines", t => t.Line_Number, cascadeDelete: true)
                .ForeignKey("dbo.Departures", t => t.Departure_IDDeparture, cascadeDelete: true)
                .Index(t => t.Line_Number)
                .Index(t => t.Departure_IDDeparture);
            
            CreateTable(
                "dbo.StationLines",
                c => new
                    {
                        Station_Name = c.String(nullable: false, maxLength: 128),
                        Line_Number = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Station_Name, t.Line_Number })
                .ForeignKey("dbo.Stations", t => t.Station_Name, cascadeDelete: true)
                .ForeignKey("dbo.Lines", t => t.Line_Number, cascadeDelete: true)
                .Index(t => t.Station_Name)
                .Index(t => t.Line_Number);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Number", "dbo.Lines");
            DropForeignKey("dbo.Tickets", "IDtypeOfTicket", "dbo.TypeOfTickets");
            DropForeignKey("dbo.StationLines", "Line_Number", "dbo.Lines");
            DropForeignKey("dbo.StationLines", "Station_Name", "dbo.Stations");
            DropForeignKey("dbo.LineDepartures", "Departure_IDDeparture", "dbo.Departures");
            DropForeignKey("dbo.LineDepartures", "Line_Number", "dbo.Lines");
            DropForeignKey("dbo.Departures", "IDDay", "dbo.Days");
            DropIndex("dbo.StationLines", new[] { "Line_Number" });
            DropIndex("dbo.StationLines", new[] { "Station_Name" });
            DropIndex("dbo.LineDepartures", new[] { "Departure_IDDeparture" });
            DropIndex("dbo.LineDepartures", new[] { "Line_Number" });
            DropIndex("dbo.Vehicles", new[] { "Number" });
            DropIndex("dbo.Tickets", new[] { "IDtypeOfTicket" });
            DropIndex("dbo.Departures", new[] { "IDDay" });
            DropTable("dbo.StationLines");
            DropTable("dbo.LineDepartures");
            DropTable("dbo.Vehicles");
            DropTable("dbo.TypeOfUsers");
            DropTable("dbo.TypeOfTickets");
            DropTable("dbo.Tickets");
            DropTable("dbo.Stations");
            DropTable("dbo.Lines");
            DropTable("dbo.Departures");
            DropTable("dbo.Days");
        }
    }
}
