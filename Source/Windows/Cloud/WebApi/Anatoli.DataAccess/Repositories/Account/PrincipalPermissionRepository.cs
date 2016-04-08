using Anatoli.DataAccess.Models.Identity;
using Anatoli.DataAccess.Interfaces.Account;
using System.Linq;
using System;
using EntityFramework.Caching;
using EntityFramework.Extensions;
using System.Collections.Generic;


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