using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class PrincipalStockRepository : AnatoliRepository<PrincipalStock>, IPrincipalStockRepository
    {
        #region Ctors
        public PrincipalStockRepository() : this(new AnatoliDbContext()) { }
        public PrincipalStockRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
