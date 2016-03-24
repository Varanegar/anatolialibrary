using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StockModels;
using System.Data.Entity;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class StockProductDomain : BusinessDomainV2<StockProduct, StockProductViewModel, StockProductRepository, IStockProductRepository>, IBusinessDomainV2<StockProduct, StockProductViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StockProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }

        #endregion

        #region Methods
        public async Task<List<StockProductViewModel>> GetAllByStockId(Guid stockId)
        {
            try
            {
                DBContext.Configuration.AutoDetectChangesEnabled = false;

                var model = await Task<List<StockProductViewModel>>.Factory.StartNew(() =>
                {
                    return MainRepository.GetQuery()
                    .Where(p => p.StockId == stockId)
                    .Select(data => new StockProductViewModel
                    {
                        ID = data.Number_ID,
                        UniqueId = data.Id,
                        MinQty = data.MinQty,
                        ReorderLevel = data.ReorderLevel,
                        MaxQty = data.MaxQty,
                        IsEnable = data.IsEnable,
                        StockGuid = data.StockId,
                        FiscalYearId = data.FiscalYearId,
                        ProductGuid = data.ProductId,
                        ProductCode = data.Product.ProductCode,
                        ProductName = data.Product.ProductName,
                        QtyPerPack = data.Product.QtyPerPack,
                        StockProductRequestSupplyTypeId = data.StockProductRequestSupplyTypeId,
                        ReorderCalcTypeId = data.ReorderCalcTypeId,
                        ReorderCalcTypeInfo = data.ReorderCalcType != null ? new ReorderCalcTypeViewModel
                        {
                            ReorderTypeName = data.ReorderCalcType.ReorderTypeName,
                            UniqueId = data.ReorderCalcType.Id
                        } : null
                    })
                    .AsNoTracking()
                    .ToList();
                });

                model.Where(p => p.ReorderCalcTypeInfo == null).ToList().ForEach(itm => itm.ReorderCalcTypeInfo = new ReorderCalcTypeViewModel());

                return model;
            }
            catch (Exception ex)
            {
                Logger.Error("GetAllByStockId ", ex);
                throw ex;
            }
        }

        protected override void AddDataToRepository(StockProduct data, StockProduct item)
        {
            if (data != null)
            {
                data.MinQty = item.MinQty;
                data.ReorderLevel = item.ReorderLevel;
                data.MaxQty = item.MaxQty;
                data.IsEnable = item.IsEnable;
                data.StockId = item.StockId;
                data.FiscalYearId = item.FiscalYearId;
                data.ProductId = item.ProductId;

                data.LastUpdate = DateTime.Now;

                if (item.ReorderCalcType != null && item.ReorderCalcType.Id != Guid.Empty)
                    data.ReorderCalcTypeId = item.ReorderCalcType.Id;

                MainRepository.Update(data);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                if (item.ReorderCalcTypeId == null)
                    item.ReorderCalcType = null;

                MainRepository.Add(item);
            }
        }
        #endregion
    }
}
