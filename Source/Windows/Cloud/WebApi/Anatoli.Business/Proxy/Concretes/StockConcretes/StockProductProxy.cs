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
            return new StockProductViewModel
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
                ReorderCalcTypeId = data.ReorderCalcTypeId,
            };
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

                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

            
            };
        }
    }
}