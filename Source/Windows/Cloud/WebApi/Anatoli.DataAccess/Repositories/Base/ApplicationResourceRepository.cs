using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Repositories.Base
{
    public class ApplicationResourceRepository : BaseAnatoliRepository<ApplicationModuleResource>
    {
        public ApplicationResourceRepository(AnatoliDbContext context) : base(context)
        {

        }

        public ApplicationResourceRepository(AnatoliDbContext context, OwnerInfo ownerInfo) : base(context, ownerInfo)
        {

        }

        public IEnumerable<ApplicationModuleResource> GetAllResourcesWithApp()
        {
            return DbContext.ApplicationModuleResources.Include(r => r.ApplicationModule.Application).ToList();
        }
    }
}