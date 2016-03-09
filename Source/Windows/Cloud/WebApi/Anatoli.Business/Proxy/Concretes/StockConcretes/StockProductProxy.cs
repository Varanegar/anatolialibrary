using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductConcretes
{
    public class StockProductProxy : AnatoliProxy<StockProduct, StockProductViewModel>, IAnatoliProxy<StockProduct, StockProductViewModel>
    {
        public override StockProductViewModel Convert(StockProduct data)
        {
            var result = new StockProductViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

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
            };
            result.ReorderCalcTypeInfo = (result.ReorderCalcTypeId == null) ? new ReorderCalcTypeViewModel() : new ReorderCalcTypeViewModel { ReorderTypeName = data.ReorderCalcType.ReorderTypeName, UniqueId = data.ReorderCalcType.Id };

            return result;
        }

        public override StockProduct ReverseConvert(StockProductViewModel data)
        {
            return new StockProduct
            {
                MinQty = data.MinQty,
                ReorderLevel = data.ReorderLevel,
                MaxQty = data.MaxQty,
                IsEnable = data.IsEnable,
                StockId = data.StockGuid,
                FiscalYearId = data.FiscalYearId,
                ProductId = data.ProductGuid,
                ReorderCalcTypeId = data.ReorderCalcTypeId,
                StockProductRequestSupplyTypeId = data.StockProductRequestSupplyTypeId,

                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner_Id = data.PrivateOwnerId,

                ReorderCalcType = data.ReorderCalcTypeInfo == null ? new ReorderCalcType() : new ReorderCalcType { Id = data.ReorderCalcTypeInfo.UniqueId }
            };
        }
    }
}