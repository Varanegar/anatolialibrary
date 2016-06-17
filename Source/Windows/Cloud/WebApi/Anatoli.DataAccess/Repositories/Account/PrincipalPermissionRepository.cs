using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories.Account
{
    public class PrincipalPermissionRepository : BaseAnatoliRepository<PrincipalPermission>, IPrincipalPermissionRepository
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