﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;

namespace Anatoli.DataAccess.Repositories
{
    public class PurchaseOrderStatusHistoryRepository : AnatoliRepository<PurchaseOrderStatusHistory>, IPurchaseOrderStatusHistoryRepository
    {
        #region Ctors
        public PurchaseOrderStatusHistoryRepository() : this(new AnatoliDbContext()) { }
        public PurchaseOrderStatusHistoryRepository(AnatoliDbContext context)
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
        public override async Task DeleteAsync(PurchaseOrderStatusHistory entity)
        {
            await DeleteAsync(entity.Id);
        }
    }
}