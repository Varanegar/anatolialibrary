using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class BasketRepository : AnatoliRepository<Basket>, IBasketRepository
    {
        #region Ctors
        public BasketRepository() : this(new AnatoliDbContext()) { }
        public BasketRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
