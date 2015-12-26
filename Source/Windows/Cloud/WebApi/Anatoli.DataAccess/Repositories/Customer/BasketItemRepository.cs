using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class BasketItemRepository : AnatoliRepository<BasketItem>, IBasketItemRepository
    {
        #region Ctors
        public BasketItemRepository() : this(new AnatoliDbContext()) { }
        public BasketItemRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
