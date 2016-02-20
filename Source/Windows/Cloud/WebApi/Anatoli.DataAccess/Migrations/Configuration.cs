namespace Anatoli.DataAccess.Migrations
{
    using Anatoli.DataAccess.Models;
    using Anatoli.DataAccess.Models.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Anatoli.DataAccess.AnatoliDbContext>
    {
        private readonly bool _pendingMigrations;
        public Configuration()
        {
            var migrator = new DbMigrator(this);
            var _pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(Anatoli.DataAccess.AnatoliDbContext context)
        {
            //if (!_pendingMigrations) return;

            

        }
    }
}
