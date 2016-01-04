using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestProductConcretes
{
    public class StockProductRequestProductProxy : AnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>, IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>
    {
        public override StockProductRequestProductViewModel Convert(StockProductRequestProduct data)
        {
            return new StockProductRequestProductViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

            };
        }

        public override StockProductRequestProduct ReverseConvert(StockProductRequestProductViewModel data)
        {
            return new StockProductRequestProduct
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

            
            };
        }
    }
}