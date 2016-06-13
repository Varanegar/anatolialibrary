using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Repositories.Base
{
    public class ApplicationRepository : BaseAnatoliRepository<Application>
    {
        public ApplicationRepository(AnatoliDbContext context) : base(context)
        {
        }

        public ApplicationRepository(AnatoliDbContext context, OwnerInfo ownerInfo) : base(context, ownerInfo)
        {
        }

        public IEnumerable<Application> GetAppsWithModulesAndResources()
        {
            return DbContext.Applications.Include(app => app.ApplicationModules.Select(m => m.ApplicationModuleResources)).AsNoTracking().ToList();
        }
    }
}
