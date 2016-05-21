using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;

namespace Anatoli.DataAccess.Repositories.Account
{
    public class PermissionRepository : BaseAnatoliRepository<Permission>, IPermissionRepository
    {
        #region Ctors
        public PermissionRepository() : this(new AnatoliDbContext()) { }
        public PermissionRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        //notice: new custom methods could be added in here
    }
}