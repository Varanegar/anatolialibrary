using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;

namespace Anatoli.Business.Proxy.Concretes
{
    public class PurchaseOrderProxy : AnatoliProxy<PurchaseOrder, PurchaseOrderViewModel>, IAnatoliProxy<PurchaseOrder, PurchaseOrderViewModel>
    {

        public override PurchaseOrderViewModel Convert(PurchaseOrder data)
        {
            return new PurchaseOrderViewModel()
                {
                    ID = data.Number_ID,
                    UniqueId = data.Id,

                };
        }

        public override PurchaseOrder ReverseConvert(PurchaseOrderViewModel data)
        {
            return new PurchaseOrder()
                {
                    Number_ID = data.ID,
                    Id = data.UniqueId,
                };
        }
    }
}
