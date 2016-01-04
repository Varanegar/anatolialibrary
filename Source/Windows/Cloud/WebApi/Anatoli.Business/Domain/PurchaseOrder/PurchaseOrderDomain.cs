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

namespace Anatoli.Business.Domain
{
    public class PurchaseOrderDomain : BusinessDomain<PurchaseOrderViewModel>, IBusinessDomain<PurchaseOrder, PurchaseOrderViewModel>
    {
        #region Properties
        public IAnatoliProxy<PurchaseOrder, PurchaseOrderViewModel> Proxy { get; set; }
        public IRepository<PurchaseOrder> Repository { get; set; }

        #endregion

        #region Ctors
        PurchaseOrderDomain() { }
        public PurchaseOrderDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public PurchaseOrderDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new PurchaseOrderRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<PurchaseOrder, PurchaseOrderViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public PurchaseOrderDomain(IPurchaseOrderRepository PurchaseOrderRepository, IPrincipalRepository principalRepository, IAnatoliProxy<PurchaseOrder, PurchaseOrderViewModel> proxy)
        {
            Proxy = proxy;
            Repository = PurchaseOrderRepository;
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

        public async Task PublishAsync(List<PurchaseOrderViewModel> purchaseOrderViewModels)
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

        public async Task Delete(List<PurchaseOrderViewModel> purchaseOrderViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var itemImages = Proxy.ReverseConvert(purchaseOrderViewModels);

                itemImages.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public async Task PublishOrderOnline(PurchaseOrderViewModel order)
        {
            var returnData = new List<PurchaseOrderViewModel>();
            string data = JsonConvert.SerializeObject(order);

            await Task.Factory.StartNew(() =>
            {
                PostOnlineData(WebApiURIHelper.SaveOrderLocalURI, data);
            });
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
