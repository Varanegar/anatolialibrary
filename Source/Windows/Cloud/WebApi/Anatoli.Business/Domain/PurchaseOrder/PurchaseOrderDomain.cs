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
    public class PurchaseOrderDomain : BusinessDomain<PurchaseOrderViewModel>, IBusinessDomain<PurchaseOrder, PurchaseOrderViewModel>
    {
        #region Properties
        public IAnatoliProxy<Customer, CustomerViewModel> CustomerProxy { get; set; }
        public IAnatoliProxy<PurchaseOrder, PurchaseOrderViewModel> Proxy { get; set; }
        public IRepository<Customer> CustomerRepository { get; set; }
        public IRepository<PurchaseOrder> Repository { get; set; }

        #endregion

        #region Ctors
        PurchaseOrderDomain() { }
        public PurchaseOrderDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public PurchaseOrderDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PurchaseOrderRepository(dbc), new CustomerRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<PurchaseOrder, PurchaseOrderViewModel>.Create(), AnatoliProxy<Customer, CustomerViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public PurchaseOrderDomain(IPurchaseOrderRepository PurchaseOrderRepository, ICustomerRepository customerRepository, IPrincipalRepository principalRepository, IAnatoliProxy<PurchaseOrder, PurchaseOrderViewModel> proxy, IAnatoliProxy<Customer, CustomerViewModel> customerProxy)
        {
            Proxy = proxy;
            CustomerProxy = customerProxy;
            Repository = PurchaseOrderRepository;
            CustomerRepository = customerRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<PurchaseOrderViewModel>> GetAll()
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task<List<PurchaseOrderViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task<List<PurchaseOrderViewModel>> PublishAsync(List<PurchaseOrderViewModel> dataViewModels)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task<List<PurchaseOrderViewModel>> Delete(List<PurchaseOrderViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var itemImages = Proxy.ReverseConvert(dataViewModels);

                itemImages.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });

            return dataViewModels;
        }

        public async Task<PurchaseOrderViewModel> PublishOrderOnline(PurchaseOrderViewModel order)
        {
            var returnData = new List<PurchaseOrderViewModel>();
            order.Customer = CustomerProxy.Convert(CustomerRepository.GetById(order.UserId));
            string data = JsonConvert.SerializeObject(order);
            

            await Task.Factory.StartNew(() =>
            {
                order = PostOnlineData(WebApiURIHelper.SaveOrderLocalURI, data);
            });
            return order;
        }

        public async Task<PurchaseOrderViewModel> CalcPromoOnline(PurchaseOrderViewModel order)
        {
            var returnData = new PurchaseOrderViewModel();
            string data = JsonConvert.SerializeObject(order);

            await Task.Factory.StartNew(() =>
            {
                returnData = PostOnlineData(WebApiURIHelper.CalcPromoLocalURI, data, true);
            });
            return returnData;
        }

        #endregion
    }
}
