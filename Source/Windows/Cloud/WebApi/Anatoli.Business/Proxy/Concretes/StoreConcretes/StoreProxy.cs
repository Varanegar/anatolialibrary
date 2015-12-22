using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Proxy.Concretes
{
    public class StoreProxy : AnatoliProxy<Store, StoreViewModel>, IAnatoliProxy<Store, StoreViewModel>
    {

        public override StoreViewModel Convert(Store data)
        {
            return new StoreViewModel();
        }

        public override Store ReverseConvert(StoreViewModel data)
        {
            return new Store();
        }
    }
}
