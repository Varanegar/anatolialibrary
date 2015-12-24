using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.CustomerConcretes
{
    public class CustomerProxy : AnatoliProxy<Customer, CustomerViewModel>, IAnatoliProxy<Customer, CustomerViewModel>
    {
        public IAnatoliProxy<Basket, BasketViewModel> BasketProxy { get; set; }

        #region Ctors
        public CustomerProxy() :
            this(AnatoliProxy<Basket, BasketViewModel>.Create()
            )
        { }

        public CustomerProxy(IAnatoliProxy<Basket, BasketViewModel> basketProxy
            )
        {
            BasketProxy = basketProxy;
        }
        #endregion

        public override CustomerViewModel Convert(Customer data)
        {
            return new CustomerViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateLabelKey = data.PrivateLabelOwner.Id,

                Baskets = (data.CustomerBaskets == null) ? null : BasketProxy.Convert(data.CustomerBaskets.ToList()),
            };
        }

        public override Customer ReverseConvert(CustomerViewModel data)
        {
            return new Customer
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },

                CustomerBaskets = (data.Baskets == null) ? null : data.Baskets != null ? BasketProxy.ReverseConvert(data.Baskets.ToList()) : null,
            };
        }
    }
}