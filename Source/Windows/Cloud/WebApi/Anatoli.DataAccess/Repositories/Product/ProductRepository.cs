﻿using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class ProductRepository : Common.DataAccess.Repositories.AnatoliRepository<Product>, IProductRepository
    {
        #region Ctors
        public ProductRepository() : this(new AnatoliDbContext()) { }
        public ProductRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion

        //notice: new custom methods could be added in here
        public override async Task DeleteAsync(Guid id)
        {
            var model = GetByIdAsync(id);

            if (model == null || model.Result == null)
                return;

            model.Result.IsRemoved = true;

            await UpdateAsync(model.Result);

            await SaveChangesAsync();
        }
        public override async Task DeleteAsync(Product entity)
        {
            await DeleteAsync(entity.Id);
        }
    }
}