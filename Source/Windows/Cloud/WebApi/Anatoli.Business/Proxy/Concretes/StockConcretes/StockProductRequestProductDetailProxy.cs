using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestProductDetailConcretes
{
    public class StockProductRequestProductDetailProxy : AnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>, IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>
    {
        public override StockProductRequestProductDetailViewModel Convert(StockProductRequestProductDetail data)
        {
            return new StockProductRequestProductDetailViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

                StockProductRequestProductId = data.StockProductRequestProductId,
                RequestQty = data.RequestQty,
                StockProductRequestRuleId = data.StockProductRequestRuleId,
                StockProductRequestRuleName = data.StockProductRequestRule.StockProductRequestRuleName
            };
        }

        public override StockProductRequestProductDetail ReverseConvert(StockProductRequestProductDetailViewModel data)
        {
            return new StockProductRequestProductDetail
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner_Id = data.PrivateOwnerId,
                CreatedDate = DateTime.Now,
                LastUpdate = DateTime.Now,

                StockProductRequestProductId = data.StockProductRequestProductId,
                RequestQty = data.RequestQty,
                StockProductRequestRuleId = data.StockProductRequestRuleId,

            };
        }
    }
}