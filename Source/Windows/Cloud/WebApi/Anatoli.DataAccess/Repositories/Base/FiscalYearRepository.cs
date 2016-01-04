using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class FiscalYearRepository : AnatoliRepository<FiscalYear>, IFiscalYearRepository
    {
        #region Ctors
        public FiscalYearRepository() : this(new AnatoliDbContext()) { }
        public FiscalYearRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
