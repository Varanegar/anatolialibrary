namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserResetPass2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ResetSMSCode", c => c.String());
            AddColumn("dbo.Users", "ResetSMSPass", c => c.String());
            AddColumn("dbo.Users", "ResetSMSRequestTime", c => c.DateTime(nullable: true));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "ResetSMSRequestTime");
            DropColumn("dbo.Users", "ResetSMSPass");
            DropColumn("dbo.Users", "ResetSMSCode");
        }
        
    }
}
