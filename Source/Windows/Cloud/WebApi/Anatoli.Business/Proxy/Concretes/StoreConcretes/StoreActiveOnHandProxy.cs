using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Proxy.Concretes
{
    public class StoreActiveOnhandProxy : AnatoliProxy<StoreActiveOnhand, StoreActiveOnhandViewModel>, IAnatoliProxy<StoreActiveOnhand, StoreActiveOnhandViewModel>
    {

        public override StoreActiveOnhandViewModel Convert(StoreActiveOnhand data)
        {
            return new StoreActiveOnhandViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,

                    ProductGuid = data.ProductId,
                    StoreGuid = data.StoreId,
                    Qty  = data.Qty,
                };
        }

        public override StoreActiveOnhand ReverseConvert(StoreActiveOnhandViewModel data)
        {
            return new StoreActiveOnhand()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,

                    ProductId = data.ProductGuid,
                    StoreId = data.StoreGuid,
                    Qty = data.Qty,
                };
        }
    }
}
