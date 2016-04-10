using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.DataAccess.Repositories.Account
{
    public class PermissionCatalogRepository : AnatoliRepository<PermissionCatalog>, IPermissionCatalogRepository
    {
        #region Ctors
        public PermissionCatalogRepository() : this(new AnatoliDbContext()) { }
        public PermissionCatalogRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        //notice: new custom methods could be added in here
    }
}
