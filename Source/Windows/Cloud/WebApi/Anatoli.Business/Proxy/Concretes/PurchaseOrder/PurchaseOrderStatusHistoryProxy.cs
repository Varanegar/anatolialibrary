using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Proxy.Concretes
{
    public class PurchaseOrderStatusHistoryProxy : AnatoliProxy<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>, IAnatoliProxy<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>
    {

        public override PurchaseOrderStatusHistoryViewModel Convert(PurchaseOrderStatusHistory data)
        {
            return new PurchaseOrderStatusHistoryViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,



                };
        }

        public override PurchaseOrderStatusHistory ReverseConvert(PurchaseOrderStatusHistoryViewModel data)
        {
            return new PurchaseOrderStatusHistory()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,
                };
        }
    }
}
