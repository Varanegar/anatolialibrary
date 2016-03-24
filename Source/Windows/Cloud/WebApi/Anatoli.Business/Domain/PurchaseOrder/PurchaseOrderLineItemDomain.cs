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
    public class PurchaseOrderLineItemDomain : BusinessDomainV2<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel, PurchaseOrderLineItemRepository, IPurchaseOrderLineItemRepository>, IBusinessDomainV2<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>
    {
        #region Properties
        public IRepository<Customer> CustomerRepository { get; set; }
        #endregion

        #region Ctors
        public PurchaseOrderLineItemDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public PurchaseOrderLineItemDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            CustomerRepository = new CustomerRepository(dbc);
        }
        #endregion

        #region Methods
        public async Task<List<PurchaseOrderLineItemViewModel>> GetAllByPOIdOnLine(Guid orderId)
        {
            List<PurchaseOrderLineItemViewModel> returnData = new List<PurchaseOrderLineItemViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var result = MainRepository.DbContext.PurchaseOrders.Where(f => f.Id == orderId && f.DataOwnerId == DataOwnerKey).Select(m => m.StoreId).First();

                if (result != null)
                    returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoLineItemsByPoIdLocalURI, "poId=" + orderId + "&centerId=" + result));
            });
            return returnData;
        }


        public override async Task PublishAsync(List<PurchaseOrderLineItem> dataViewModels)
        {
            Exception ex = new NotImplementedException();
            Logger.Error("PublishAsync", ex);
            throw ex;

        }

        public override async Task DeleteAsync(List<PurchaseOrderLineItem> dataViewModels)
        {
            Exception ex = new NotImplementedException();
            Logger.Error("DeleteAsync", ex);
            throw ex;
        }

        #endregion
    }
}
