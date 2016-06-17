﻿using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class PurchaseOrderLineItemRepository : AnatoliRepository<PurchaseOrderLineItem>, IPurchaseOrderLineItemRepository
    {
        #region Ctors
        public PurchaseOrderLineItemRepository() : this(new AnatoliDbContext()) { }
        public PurchaseOrderLineItemRepository(AnatoliDbContext context)
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
        public override async Task DeleteAsync(PurchaseOrderLineItem entity)
        {
            await DeleteAsync(entity.Id);
        }
    }
}