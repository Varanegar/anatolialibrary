using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;

namespace Anatoli.DataAccess.Repositories.Account
{
    public class PrincipalPermissionRepository : AnatoliRepository<PrincipalPermission>, IPrincipalPermissionRepository
    {
        #region Ctors
        public PrincipalPermissionRepository() : this(new AnatoliDbContext()) { }
        public PrincipalPermissionRepository(AnatoliDbContext context)
            : base(context)
        {
        }

        #endregion

        //notice: new custom methods could be added in here
    }   
}