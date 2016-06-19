using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.Order;
using Anatoli.Business.Helpers;
using Newtonsoft.Json;
using Anatoli.ViewModels;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

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
        public async Task<List<PurchaseOrderLineItemViewModel>> GetAllByPOIdOnLine(PurchaseOrderRequestModel dataRequest)
        {
            List<PurchaseOrderLineItemViewModel> returnData = new List<PurchaseOrderLineItemViewModel>();
            await Task.Factory.StartNew(() =>
            {
                dataRequest.centerId = "all";
                string data = JsonConvert.SerializeObject(dataRequest);
                returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoLineItemsByPoIdLocalURI, data));
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
