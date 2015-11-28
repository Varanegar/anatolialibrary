using System;
using System.Linq;
using System.Data.Entity;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class ProductRepository : AnatoliRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context)
            : base(context)
        {
        }

        //new custom methods could be added in here
    }
}