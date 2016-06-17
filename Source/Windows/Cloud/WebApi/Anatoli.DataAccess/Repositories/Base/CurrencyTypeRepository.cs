using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class CurrencyTypeRepository : AnatoliRepository<CurrencyType>, ICurrencyTypeRepository
    {
        #region Ctors
        public CurrencyTypeRepository() : this(new AnatoliDbContext()) { }
        public CurrencyTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
