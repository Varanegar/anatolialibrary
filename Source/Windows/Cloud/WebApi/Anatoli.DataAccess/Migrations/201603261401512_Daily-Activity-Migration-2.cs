namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyActivityMigration2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Visitors", newName: "CompanyPersonnels");
            RenameTable(name: "dbo.VisitorRoutes", newName: "CompanyPersonnelRoutes");
            RenameColumn(table: "dbo.PersonnelDailyActivityDayAreas", name: "VisitorId", newName: "CompanyPersonnelId");
            RenameColumn(table: "dbo.PersonnelDailyActivityEvents", name: "VisitorId", newName: "CompanyPersonnelId");
            RenameColumn(table: "dbo.PersonnelDailyActivityPoints", name: "VisitorId", newName: "CompanyPersonnelId");
            RenameIndex(table: "dbo.PersonnelDailyActivityDayAreas", name: "IX_VisitorId", newName: "IX_CompanyPersonnelId");
            RenameIndex(table: "dbo.PersonnelDailyActivityEvents", name: "IX_VisitorId", newName: "IX_CompanyPersonnelId");
            RenameIndex(table: "dbo.PersonnelDailyActivityPoints", name: "IX_VisitorId", newName: "IX_CompanyPersonnelId");
            AddColumn("dbo.PersonnelDailyActivityEvents", "ActivityDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PersonnelDailyActivityEvents", "ActivityPDate", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonnelDailyActivityEvents", "ActivityPDate");
            DropColumn("dbo.PersonnelDailyActivityEvents", "ActivityDate");
            RenameIndex(table: "dbo.PersonnelDailyActivityPoints", name: "IX_CompanyPersonnelId", newName: "IX_VisitorId");
            RenameIndex(table: "dbo.PersonnelDailyActivityEvents", name: "IX_CompanyPersonnelId", newName: "IX_VisitorId");
            RenameIndex(table: "dbo.PersonnelDailyActivityDayAreas", name: "IX_CompanyPersonnelId", newName: "IX_VisitorId");
            RenameColumn(table: "dbo.PersonnelDailyActivityPoints", name: "CompanyPersonnelId", newName: "VisitorId");
            RenameColumn(table: "dbo.PersonnelDailyActivityEvents", name: "CompanyPersonnelId", newName: "VisitorId");
            RenameColumn(table: "dbo.PersonnelDailyActivityDayAreas", name: "CompanyPersonnelId", newName: "VisitorId");
            RenameTable(name: "dbo.CompanyPersonnelRoutes", newName: "VisitorRoutes");
            RenameTable(name: "dbo.CompanyPersonnels", newName: "Visitors");
        }
    }
}
