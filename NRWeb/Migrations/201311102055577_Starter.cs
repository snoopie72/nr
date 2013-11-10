namespace NRWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Starter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kontigents",
                c => new
                    {
                        KontigentId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Sum = c.Single(nullable: false),
                        DatoBetalt = c.DateTime(nullable: false),
                        Medlem_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.KontigentId)
                .ForeignKey("dbo.Users", t => t.Medlem_Name)
                .Index(t => t.Medlem_Name);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Adresse = c.String(),
                        Fodselsdato = c.DateTime(nullable: false),
                        Postnr = c.String(),
                        Poststed = c.String(),
                        RegistringsDato = c.DateTime(nullable: false),
                        Kjonn = c.Int(nullable: false),
                        Epost = c.String(),
                        Passord = c.String(),
                        Motto = c.String(),
                        Bilde = c.Binary(),
                        Telefon = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.RaceTimes",
                c => new
                    {
                        RaceTimeId = c.Int(nullable: false, identity: true),
                        Time = c.Time(nullable: false, precision: 7),
                        Bilde = c.Binary(),
                        Kommentar = c.String(),
                        RaceInstance_RaceInstanceId = c.Int(),
                        User_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RaceTimeId)
                .ForeignKey("dbo.RaceInstances", t => t.RaceInstance_RaceInstanceId)
                .ForeignKey("dbo.Users", t => t.User_Name)
                .Index(t => t.RaceInstance_RaceInstanceId)
                .Index(t => t.User_Name);
            
            CreateTable(
                "dbo.RaceInstances",
                c => new
                    {
                        RaceInstanceId = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        Beskrivelse = c.String(),
                        Dato = c.DateTime(nullable: false),
                        Race_RaceId = c.Int(),
                    })
                .PrimaryKey(t => t.RaceInstanceId)
                .ForeignKey("dbo.Races", t => t.Race_RaceId)
                .Index(t => t.Race_RaceId);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        RaceId = c.Int(nullable: false, identity: true),
                        Navn = c.String(),
                        Beskrivelse = c.String(),
                        Type = c.Int(nullable: false),
                        Avstand = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kontigents", "Medlem_Name", "dbo.Users");
            DropForeignKey("dbo.RaceTimes", "User_Name", "dbo.Users");
            DropForeignKey("dbo.RaceTimes", "RaceInstance_RaceInstanceId", "dbo.RaceInstances");
            DropForeignKey("dbo.RaceInstances", "Race_RaceId", "dbo.Races");
            DropIndex("dbo.Kontigents", new[] { "Medlem_Name" });
            DropIndex("dbo.RaceTimes", new[] { "User_Name" });
            DropIndex("dbo.RaceTimes", new[] { "RaceInstance_RaceInstanceId" });
            DropIndex("dbo.RaceInstances", new[] { "Race_RaceId" });
            DropTable("dbo.Races");
            DropTable("dbo.RaceInstances");
            DropTable("dbo.RaceTimes");
            DropTable("dbo.Users");
            DropTable("dbo.Kontigents");
        }
    }
}
