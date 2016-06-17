using System.Data.Entity;
using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Anatoli.Common.DataAccess.Models;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories.Base
{
    public class ApplicationModuleRepository : BaseAnatoliRepository<ApplicationModule>
    {
        public ApplicationModuleRepository(AnatoliDbContext context) : base(context)
        {

        }

        public ApplicationModuleRepository(AnatoliDbContext context, OwnerInfo ownerInfo) : base(context, ownerInfo)
        {

        }

        public IEnumerable<ApplicationModule> GetAllModulesWithApp()
        {
            return ((AnatoliDbContext)DbContext).ApplicationModules.Include(m => m.Application).ToList();
        }

        public IEnumerable<ApplicationModule> GetAllModulesOfApp(Guid appId)
        {
            return ((AnatoliDbContext)DbContext).ApplicationModules.Where(m => m.ApplicationId == appId).Include(m => m.Application).ToList();
        }
    }
}
