using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestConcretes
{
    public class StockProductRequestProxy : AnatoliProxy<StockProductRequest, StockProductRequestViewModel>, IAnatoliProxy<StockProductRequest, StockProductRequestViewModel>
    {
        public override StockProductRequestViewModel Convert(StockProductRequest data)
        {
            return new StockProductRequestViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

            };
        }

        public override StockProductRequest ReverseConvert(StockProductRequestViewModel data)
        {
            return new StockProductRequest
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

            
            };
        }
    }
}