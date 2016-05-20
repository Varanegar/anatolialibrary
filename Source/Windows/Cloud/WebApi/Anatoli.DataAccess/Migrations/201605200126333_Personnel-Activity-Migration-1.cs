namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonnelActivityMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonnelDailyActivityEvents", "JData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonnelDailyActivityEvents", "JData");
        }
    }
}
