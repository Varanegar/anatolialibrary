using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

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