using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;

namespace Anatoli.DataAccess.Repositories.Account
{
    public class PrincipalPermissionCatalogRepository : BaseAnatoliRepository<PrincipalPermissionCatalog>, IPrincipalPermissionCatalogRepository
    {
        #region Ctors
        public PrincipalPermissionCatalogRepository() : this(new AnatoliDbContext()) { }
        public PrincipalPermissionCatalogRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        //notice: new custom methods could be added in here
    }
}
