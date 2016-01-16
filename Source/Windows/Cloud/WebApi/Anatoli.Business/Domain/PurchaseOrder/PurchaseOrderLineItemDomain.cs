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
    public class PurchaseOrderLineItemDomain : BusinessDomain<PurchaseOrderLineItemViewModel>, IBusinessDomain<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>
    {
        #region Properties
        public IAnatoliProxy<Customer, CustomerViewModel> CustomerProxy { get; set; }
        public IAnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel> Proxy { get; set; }
        public IRepository<Customer> CustomerRepository { get; set; }
        public IRepository<PurchaseOrderLineItem> Repository { get; set; }

        #endregion

        #region Ctors
        PurchaseOrderLineItemDomain() { }
        public PurchaseOrderLineItemDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public PurchaseOrderLineItemDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PurchaseOrderLineItemRepository(dbc), new CustomerRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>.Create(), AnatoliProxy<Customer, CustomerViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public PurchaseOrderLineItemDomain(IPurchaseOrderLineItemRepository PurchaseOrderLineItemRepository, ICustomerRepository customerRepository, IPrincipalRepository principalRepository, IAnatoliProxy<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel> proxy, IAnatoliProxy<Customer, CustomerViewModel> customerProxy)
        {
            Proxy = proxy;
            CustomerProxy = customerProxy;
            Repository = PurchaseOrderLineItemRepository;
            CustomerRepository = customerRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<PurchaseOrderLineItemViewModel>> GetAll()
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task<List<PurchaseOrderLineItemViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var data = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(data.ToList()); ;
        }

        public async Task<List<PurchaseOrderLineItemViewModel>> GetAllByPOIdOnLine(string orderId)
        {
            Guid orderGuid = Guid.Parse(orderId);
            List<PurchaseOrderLineItemViewModel> returnData = new List<PurchaseOrderLineItemViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var result = Repository.DbContext.PurchaseOrders.Where(f => f.Id == orderGuid).Select(m => m.StoreId).First();

                if (result != null)
                    returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoLineItemsByPoIdLocalURI, "poId=" + orderId + "&centerId=" + result));
            });
            return returnData;
        }


        public async Task<List<PurchaseOrderLineItemViewModel>> PublishAsync(List<PurchaseOrderLineItemViewModel> dataViewModels)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            return dataViewModels;

        }

        public async Task<List<PurchaseOrderLineItemViewModel>> Delete(List<PurchaseOrderLineItemViewModel> dataViewModels)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
