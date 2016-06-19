using Anatoli.DataAccess.Models.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Anatoli.Common.DataAccess.Models;
using Anatoli.Common.DataAccess.Repositories;

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
            return ((AnatoliDbContext)DbContext).ApplicationModuleResources.Include(r => r.ApplicationModule.Application).ToList();
        }
    }
}