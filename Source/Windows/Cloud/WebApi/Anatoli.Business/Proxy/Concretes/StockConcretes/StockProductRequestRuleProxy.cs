using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestRuleConcretes
{
    public class StockProductRequestRuleProxy : AnatoliProxy<StockProductRequestRule, StockProductRequestRuleViewModel>, IAnatoliProxy<StockProductRequestRule, StockProductRequestRuleViewModel>
    {
        public override StockProductRequestRuleViewModel Convert(StockProductRequestRule data)
        {
            return new StockProductRequestRuleViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

                FromDate = data.FromDate,
                FromPDate = data.FromPDate,
                MainProductGroupId = data.MainProductGroupId,
                ProductId = data.ProductId,
                ProductTypeId = data.ProductTypeId,
                SupplierId = data.SupplierId,
                ReorderCalcTypeId = data.ReorderCalcTypeId,
                RuleTypeId = data.StockProductRequestRuleTypeId,
                ToDate = data.ToDate,
                ToPDate = data.ToPDate,

            };
        }

        public override StockProductRequestRule ReverseConvert(StockProductRequestRuleViewModel data)
        {
            return new StockProductRequestRule
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                FromDate = data.FromDate,
                FromPDate = data.FromPDate,
                MainProductGroupId = data.MainProductGroupId,
                ProductId = data.ProductId,
                ProductTypeId = data.ProductTypeId,
                SupplierId = data.SupplierId,
                ReorderCalcTypeId = data.ReorderCalcTypeId,
                StockProductRequestRuleTypeId = data.RuleTypeId,
                ToDate = data.ToDate,
                ToPDate = data.ToPDate,
            
            };
        }
    }
}