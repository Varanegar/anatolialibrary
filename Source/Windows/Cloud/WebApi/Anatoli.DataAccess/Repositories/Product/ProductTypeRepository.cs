using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class ProductTypeRepository : AnatoliRepository<ProductType>, IProductTypeRepository
    {
        #region Ctors
        public ProductTypeRepository() : this(new AnatoliDbContext()) { }
        public ProductTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}