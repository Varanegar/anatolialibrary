namespace Anatoli.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
    
    public partial class InitialDataInsert2 : DbMigration
    {
        public override void Up()
        {
            AnatoliDbContext context = new AnatoliDbContext();

            context.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
