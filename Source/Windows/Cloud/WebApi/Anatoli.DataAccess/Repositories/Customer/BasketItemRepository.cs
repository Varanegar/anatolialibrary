using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

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
