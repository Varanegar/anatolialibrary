using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.Order;
using Anatoli.Business.Helpers;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.Business.Domain
{
    public class PurchaseOrderStatusHistoryDomain : BusinessDomainV2<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel, PurchaseOrderStatusHistoryRepository, IPurchaseOrderStatusHistoryRepository>, IBusinessDomainV2<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public PurchaseOrderStatusHistoryDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public PurchaseOrderStatusHistoryDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<PurchaseOrderStatusHistoryViewModel>> GetAllByPOIdOnLine(Guid orderId)
        {
            List<PurchaseOrderStatusHistoryViewModel> returnData = new List<PurchaseOrderStatusHistoryViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var result = MainRepository.DbContext.PurchaseOrders.Where(f => f.Id == orderId && f.DataOwnerId == DataOwnerKey ).Select(m => m.StoreId).First();
        
                if(result != null)
                    returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoStatusHistoryByPoIdLocalURI, "poId=" + orderId + "&centerId=" + result));
            });
            return returnData;
        }

        public override async Task PublishAsync(List<PurchaseOrderStatusHistory> dataViewModels)
        {
            Exception ex = new NotImplementedException();
            Logger.Error("PublishAsync", ex);
            throw ex;

        }

        public override async Task DeleteAsync(List<PurchaseOrderStatusHistory> dataViewModels)
        {
            Exception ex = new NotImplementedException();
            Logger.Error("DeleteAsync", ex);
            throw ex;

        }


        #endregion
    }
}
