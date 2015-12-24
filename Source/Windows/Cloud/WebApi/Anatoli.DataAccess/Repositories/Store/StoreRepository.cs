using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class StoreRepository : AnatoliRepository<Store>, IStoreRepository
    {
          #region Ctors
        public StoreRepository() : this(new AnatoliDbContext()) { }
        public StoreRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
