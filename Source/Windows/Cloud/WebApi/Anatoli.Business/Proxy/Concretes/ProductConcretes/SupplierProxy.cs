using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class SupplierProxy : AnatoliProxy<Supplier, SupplierViewModel>, IAnatoliProxy<Supplier, SupplierViewModel>
    {
        public override SupplierViewModel Convert(Supplier data)
        {
            return new SupplierViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                IsRemoved = data.IsRemoved,
                SupplierName = data.SupplierName,
                PrivateOwnerId = data.PrivateLabelOwner_Id
            };
        }

        public override Supplier ReverseConvert(SupplierViewModel data)
        {
            return new Supplier
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,
                SupplierName = data.SupplierName,   
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId }
            };
        }
    }
}
