using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

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