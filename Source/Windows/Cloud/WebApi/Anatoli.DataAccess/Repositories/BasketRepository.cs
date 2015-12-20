using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class BasketRepository : AnatoliRepository<Basket>, IBasketRepository
    {
        #region Ctors
        public BasketRepository() : this(new AnatoliDbContext()) { }
        public BasketRepository(DbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
