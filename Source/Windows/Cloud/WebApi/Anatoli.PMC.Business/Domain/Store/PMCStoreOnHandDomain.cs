using Anatoli.PMC.Business.Proxy;
using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.ViewModels.StoreModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Domain.Store
{
    public class PMCStoreOnHandDomain : IPMCBusinessDomain<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Properties
        public IAnatoliProxy<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel> Proxy { get; set; }
        #endregion

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
            var storeActiveOnhands = StoreAdapter.GetAllStoreOnHands(DateTime.MinValue);
            return storeActiveOnhands;
        }

        public List<StoreActiveOnhandViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            var storeActiveOnhands = StoreAdapter.GetAllStoreOnHands(DateTime.MinValue);
            return storeActiveOnhands;
        }

        public List<StoreActiveOnhandViewModel> GetAllByStoreId(string storeId)
        {
            var storeActiveOnhands = StoreAdapter.GetAllStoreOnHandsByStoreId(DateTime.MinValue, storeId);
            return storeActiveOnhands;
        }

        public List<StoreActiveOnhandViewModel> GetAllChangedAfter(DateTime selectedDate, string storeId)
        {
            var storeActiveOnhands = StoreAdapter.GetAllStoreOnHandsByStoreId(DateTime.MinValue, storeId);
            return storeActiveOnhands;
        }

        public Task PublishAsync(List<StoreActiveOnhandViewModel> BaseViewModels)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<StoreActiveOnhandViewModel> BaseViewModels)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
