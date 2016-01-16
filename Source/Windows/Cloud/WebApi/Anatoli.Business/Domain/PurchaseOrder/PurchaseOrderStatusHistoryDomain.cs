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
    public class PurchaseOrderStatusHistoryDomain : BusinessDomain<PurchaseOrderStatusHistoryViewModel>, IBusinessDomain<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>
    {
        #region Properties
        public IAnatoliProxy<Customer, CustomerViewModel> CustomerProxy { get; set; }
        public IAnatoliProxy<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel> Proxy { get; set; }
        public IRepository<Customer> CustomerRepository { get; set; }
        public IRepository<PurchaseOrderStatusHistory> Repository { get; set; }

        #endregion

        #region Ctors
        PurchaseOrderStatusHistoryDomain() { }
        public PurchaseOrderStatusHistoryDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public PurchaseOrderStatusHistoryDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PurchaseOrderStatusHistoryRepository(dbc), new CustomerRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>.Create(), AnatoliProxy<Customer, CustomerViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public PurchaseOrderStatusHistoryDomain(IPurchaseOrderStatusHistoryRepository PurchaseOrderStatusHistoryRepository, ICustomerRepository customerRepository, IPrincipalRepository principalRepository, IAnatoliProxy<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel> proxy, IAnatoliProxy<Customer, CustomerViewModel> customerProxy)
        {
            Proxy = proxy;
            CustomerProxy = customerProxy;
            Repository = PurchaseOrderStatusHistoryRepository;
            CustomerRepository = customerRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<PurchaseOrderStatusHistoryViewModel>> GetAll()
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task<List<PurchaseOrderStatusHistoryViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var data = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(data.ToList()); ;
        }

        public async Task<List<PurchaseOrderStatusHistoryViewModel>> GetAllByPOIdOnLine(string orderId)
        {
            Guid orderGuid = Guid.Parse(orderId);
            List<PurchaseOrderStatusHistoryViewModel> returnData = new List<PurchaseOrderStatusHistoryViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var result = Repository.DbContext.PurchaseOrders.Where(f => f.Id == orderGuid).Select(m => m.StoreId).First();
        
                if(result != null)
                    returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoStatusHistoryByPoIdLocalURI, "poId=" + orderId + "&centerId=" + result));
            });
            return returnData;
        }

        public async Task<List<PurchaseOrderStatusHistoryViewModel>> PublishAsync(List<PurchaseOrderStatusHistoryViewModel> dataViewModels)
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

        public async Task<List<PurchaseOrderStatusHistoryViewModel>> Delete(List<PurchaseOrderStatusHistoryViewModel> dataViewModels)
        {
            throw new NotImplementedException();

        }


        #endregion
    }
}
