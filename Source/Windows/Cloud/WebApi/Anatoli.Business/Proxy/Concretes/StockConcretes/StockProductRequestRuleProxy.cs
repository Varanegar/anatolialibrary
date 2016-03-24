using Anatoli.DataAccess.Models;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.StockModels;
using System;

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
                ApplicationOwnerId = data.ApplicationOwnerId,

                FromDate = data.FromDate,
                FromPDate = data.FromPDate,

                MainProductGroupId = data.MainProductGroupId,
                MainProductGroupName = data.MainProductGroup != null ? data.MainProductGroup.GroupName : string.Empty,

                ProductId = data.ProductId,
                ProductName = data.Product != null ? data.Product.ProductName : "",
                ProductTypeId = data.ProductTypeId,
                SupplierId = data.SupplierId,
                SupplierName = data.Supplier != null ? data.Supplier.SupplierName : string.Empty,
                ReorderCalcTypeId = data.ReorderCalcTypeId,
                RuleTypeId = data.StockProductRequestRuleTypeId,
                ToDate = data.ToDate,
                ToPDate = data.ToPDate,

                Qty = data.Qty,
                RuleCalcTypeId = data.StockProductRequestRuleCalcTypeId,

                StockProductRequestRuleName = data.StockProductRequestRuleName
            };
        }

        public override StockProductRequestRule ReverseConvert(StockProductRequestRuleViewModel data)
        {
            return new StockProductRequestRule
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,

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

                Qty = data.Qty,
                StockProductRequestRuleCalcTypeId = data.RuleCalcTypeId,

                StockProductRequestRuleName = data.StockProductRequestRuleName
            };
        }
    }

    public class StockProductRequestRuleTypeProxy : AnatoliProxy<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel>,
                                                    IAnatoliProxy<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel>
    {
        public override StockProductRequestRuleTypeViewModel Convert(StockProductRequestRuleType data)
        {
            return new StockProductRequestRuleTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                StockProductRequestRuleTypeName = data.StockProductRequestRuleTypeName
            };
        }

        public override StockProductRequestRuleType ReverseConvert(StockProductRequestRuleTypeViewModel data)
        {
            return new StockProductRequestRuleType
            {
                Id = data.UniqueId,
                Number_ID = data.ID,
                StockProductRequestRuleTypeName = data.StockProductRequestRuleTypeName
            };
        }
    }

    public class StockProductRequestRuleCalcTypeProxy : AnatoliProxy<StockProductRequestRuleCalcType, StockProductRequestRuleCalcTypeViewModel>,
                                                    IAnatoliProxy<StockProductRequestRuleCalcType, StockProductRequestRuleCalcTypeViewModel>
    {
        public override StockProductRequestRuleCalcTypeViewModel Convert(StockProductRequestRuleCalcType data)
        {
            return new StockProductRequestRuleCalcTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                StockProductRequestRuleCalcTypeName = data.StockProductRequestRuleCalcTypeName
            };
        }

        public override StockProductRequestRuleCalcType ReverseConvert(StockProductRequestRuleCalcTypeViewModel data)
        {
            return new StockProductRequestRuleCalcType
            {
                Id = data.UniqueId,
                Number_ID = data.ID,
                StockProductRequestRuleCalcTypeName = data.StockProductRequestRuleCalcTypeName
            };
        }
    }
}