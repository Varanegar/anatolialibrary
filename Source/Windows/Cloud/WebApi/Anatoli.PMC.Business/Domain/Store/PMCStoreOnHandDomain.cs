using Anatoli.PMC.Business.Proxy;
using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.ViewModels.StoreModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Domain.Store
{
    public class PMCStoreOnHandDomain : PMCBusinessDomain<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel>, IPMCBusinessDomain<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel>
    {
        #region Ctors
        public PMCStoreOnHandDomain()
            : this(AnatoliProxy<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel>.Create())
        { }
        public PMCStoreOnHandDomain(IAnatoliProxy<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel> proxy)
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
