using Anatoli.Rastak.Business.Proxy;
using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.DataAdapter;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.ViewModels.StoreModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Domain.Store
{
    public class RastakStoreOnHandDomain : RastakBusinessDomain<RastakStoreOnhandViewModel, StoreActiveOnhandViewModel>, IRastakBusinessDomain<RastakStoreOnhandViewModel, StoreActiveOnhandViewModel>
    {
        #region Ctors
        public RastakStoreOnHandDomain()
            : this(AnatoliProxy<RastakStoreOnhandViewModel, StoreActiveOnhandViewModel>.Create())
        { }
        public RastakStoreOnHandDomain(IAnatoliProxy<RastakStoreOnhandViewModel, StoreActiveOnhandViewModel> proxy)
        {
            Proxy = proxy;
        }
        #endregion

        #region Methods
        public List<StoreActiveOnhandViewModel> GetAll()
        {
            var storeActiveOnhands = StoreAdapter.Instance.GetAllStoreOnHands(DateTime.MinValue);
            return storeActiveOnhands;
        }

        public List<StoreActiveOnhandViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            var storeActiveOnhands = StoreAdapter.Instance.GetAllStoreOnHands(DateTime.MinValue);
            return storeActiveOnhands;
        }

        public List<StoreActiveOnhandViewModel> GetAllByStoreId(string storeId)
        {
            var storeActiveOnhands = StoreAdapter.Instance.GetAllStoreOnHandsByStoreId(DateTime.MinValue, storeId);
            return storeActiveOnhands;
        }

        public List<StoreActiveOnhandViewModel> GetAllChangedAfter(DateTime selectedDate, string storeId)
        {
            var storeActiveOnhands = StoreAdapter.Instance.GetAllStoreOnHandsByStoreId(DateTime.MinValue, storeId);
            return storeActiveOnhands;
        }
        #endregion
    }
}
