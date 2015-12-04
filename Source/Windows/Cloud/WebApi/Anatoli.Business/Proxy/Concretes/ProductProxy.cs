using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Aantoli.Common.Entity.Gateway.Product;

namespace Anatoli.Business.Proxy.Concretes
{
    public class ProductProxy : AnatoliProxy<Product, ProductEntity>, IAnatoliProxy<Product, ProductEntity>
    {
        public override ProductEntity Convert(Product data)
        {
            throw new NotImplementedException();
        }

        public override Product ReverseConvert(ProductEntity data)
        {
            throw new NotImplementedException();
        }
    }
}
