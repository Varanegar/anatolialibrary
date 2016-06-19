using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class ProductTagRepository : AnatoliRepository<ProductTag>, IProductTagRepository
    {
        #region Ctors
        public ProductTagRepository() : this(new AnatoliDbContext()) { }
        public ProductTagRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}