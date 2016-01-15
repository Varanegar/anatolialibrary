using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Proxy.Concretes
{
    public class StoreActivePriceListProxy : AnatoliProxy<StoreActivePriceList, StoreActivePriceListViewModel>, IAnatoliProxy<StoreActivePriceList, StoreActivePriceListViewModel>
    {

        public override StoreActivePriceListViewModel Convert(StoreActivePriceList data)
        {
            return new StoreActivePriceListViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,
                    PrivateOwnerId = data.PrivateLabelOwner_Id,
                    
                    Price = data.Price,
                    StoreGuid = data.StoreId,
                    ProductGuid = data.ProductId,
                };
        }

        public override StoreActivePriceList ReverseConvert(StoreActivePriceListViewModel data)
        {
            return new StoreActivePriceList()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,

                    Price = data.Price,
                    StoreId = data.StoreGuid,
                    ProductId = data.ProductGuid,
                };
        }
    }
}
