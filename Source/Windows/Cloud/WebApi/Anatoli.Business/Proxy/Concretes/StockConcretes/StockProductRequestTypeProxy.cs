using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestTypeConcretes
{
    public class StockProductRequestTypeProxy : AnatoliProxy<StockProductRequestType, StockProductRequestTypeViewModel>, IAnatoliProxy<StockProductRequestType, StockProductRequestTypeViewModel>
    {
        public override StockProductRequestTypeViewModel Convert(StockProductRequestType data)
        {
            return new StockProductRequestTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

                StockPorductRequestTypeName = data.StockPorductRequestTypeName,

            };
        }

        public override StockProductRequestType ReverseConvert(StockProductRequestTypeViewModel data)
        {
            return new StockProductRequestType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                StockPorductRequestTypeName = data.StockPorductRequestTypeName,
            };
        }
    }
}