using Anatoli.DataAccess.Models;
using System.Data.Entity;
using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return DbContext.ApplicationModules.Include(m => m.Application).ToList();
        }

        public IEnumerable<ApplicationModule> GetAllModulesOfApp(Guid appId)
        {
            return DbContext.ApplicationModules.Where(m => m.ApplicationId == appId).Include(m => m.Application).ToList();
        }
    }
}
