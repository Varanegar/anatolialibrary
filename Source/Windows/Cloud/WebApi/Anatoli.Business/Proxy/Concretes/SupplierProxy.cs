using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.ViewModels.Product;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Proxy.Concretes
{
    public class SupplierProxy : AnatoliProxy<Supplier, SupplierViewModel>, IAnatoliProxy<Supplier, SupplierViewModel>
    {
        public override SupplierViewModel Convert(Supplier data)
        {
            return new SupplierViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                Name = data.SupplierName,
                PrivateLabelKey = data.PrivateLabelOwner.Id
            };
        }

        public override Supplier ReverseConvert(SupplierViewModel data)
        {
            return new Supplier
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                SupplierName = data.Name,
                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey }
            };
        }
    }
}
