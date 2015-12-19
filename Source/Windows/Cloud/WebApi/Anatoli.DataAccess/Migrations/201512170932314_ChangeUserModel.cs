namespace Anatoli.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Mobile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Mobile", c => c.String());
        }
    }
}
