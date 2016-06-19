using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class ProductTagValueRepository : AnatoliRepository<ProductTagValue>, IProductTagValueRepository
    {
        #region Ctors
        public ProductTagValueRepository() : this(new AnatoliDbContext()) { }
        public ProductTagValueRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}