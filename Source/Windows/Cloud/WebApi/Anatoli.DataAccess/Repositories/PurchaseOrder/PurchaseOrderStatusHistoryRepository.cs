using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

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