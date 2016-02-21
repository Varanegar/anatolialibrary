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
            var data = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(data.ToList()); ;
        }

        public async Task<List<PurchaseOrderViewModel>> GetAllByCustomerIdOnLine(string custoemrId)
        {
            var returnData = new List<PurchaseOrderViewModel>();

            await Task.Factory.StartNew(() =>
            {

                var storeList = Repository.DbContext.Stores.Select(m => m.Id);
                Parallel.ForEach(storeList, (currentStore) =>
                    {
                        returnData.AddRange(GetOnlineData(WebApiURIHelper.GetPoByCustomerIdLocalURI, "id=" + custoemrId + "&centerId=" + currentStore));
                    });
            });
            return returnData;
        }

        public async Task<List<PurchaseOrderViewModel>> PublishAsync(List<PurchaseOrderViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (PurchaseOrder item in dataList)
                {

                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentData = Repository.GetQuery().Where(p => p.CustomerId == item.CustomerId).FirstOrDefault();
                    if (currentData != null)
                    {
                        currentData.ActionSourceId = item.ActionSourceId;
                        currentData.AppOrderNo = item.AppOrderNo;
                        currentData.BackOfficeId = item.BackOfficeId;
                        currentData.CancelDesc = item.CancelDesc;
                        currentData.CancelReasonId = item.CancelReasonId;
                        currentData.ChargeAmount = item.ChargeAmount;
                        currentData.ChargeFinalAmount = item.ChargeFinalAmount;
                        currentData.Comment = item.Comment;
                        currentData.CustomerId = item.CustomerId;
                        currentData.CustomerShipAddressId = item.CustomerShipAddressId;
                        currentData.DeliveryDate = item.DeliveryDate;
                        currentData.DeliveryFromTime = item.DeliveryFromTime;
                        currentData.DeliveryPDate = item.DeliveryPDate;
                        currentData.DeliveryToTime = item.DeliveryToTime;
                        currentData.DeliveryTypeId = item.DeliveryTypeId;
                        currentData.DeviceIMEI = item.DeviceIMEI;
                        currentData.Discount2FinalAmount = item.Discount2FinalAmount;
                        currentData.DiscountAmount = item.DiscountAmount;
                        currentData.DiscountAmount2 = item.DiscountAmount2;
                        currentData.DiscountCodeId = item.DiscountCodeId;
                        currentData.DiscountFinalAmount = item.DiscountFinalAmount;
                        currentData.FinalNetAmount = item.FinalNetAmount;
                        currentData.IsCancelled = item.IsCancelled;
                        currentData.IsRemoved = item.IsRemoved;
                        currentData.NetAmount = item.NetAmount;
                        currentData.Number_ID = item.Number_ID;
                        currentData.OrderDate = item.OrderDate;
                        currentData.OrderPDate = item.OrderPDate;
                        currentData.OrderTime = item.OrderTime;
                        currentData.OtherAdd = item.OtherAdd;
                        currentData.OtherFinalAdd = item.OtherFinalAdd;
                        currentData.OtherFinalSub = item.OtherFinalSub;
                        currentData.OtherSub = item.OtherSub;
                        currentData.PaymentTypeId = item.PaymentTypeId;
                        currentData.PurchaseOrderStatusId = item.PurchaseOrderStatusId;
                        currentData.ShippingCost = item.ShippingCost;
                        currentData.ShippingFinalCost = item.ShippingFinalCost;
                        currentData.StoreId = item.StoreId;
                        currentData.TaxAmount = item.TaxAmount;
                        currentData.TaxFinalAmount = item.TaxFinalAmount;
                        currentData.TotalAmount = item.TotalAmount;
                        currentData.TotalFinalAmount = item.TotalFinalAmount;
                        currentData.LastUpdate = DateTime.Now;
                        currentData = await SetLineItemData(currentData, item.PurchaseOrderLineItems.ToList(), Repository.DbContext);
                        await Repository.UpdateAsync(currentData);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.PurchaseOrderLineItems != null)
                        {
                            item.PurchaseOrderLineItems.ToList().ForEach(itemDetail =>
                            {
                                itemDetail.PrivateLabelOwner = item.PrivateLabelOwner;
                                itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                            });
                        }
                        await Repository.AddAsync(item);
                    }
                }
                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            return dataViewModels;

        }

        public async Task<PurchaseOrder> SetLineItemData(PurchaseOrder data, List<PurchaseOrderLineItem> dataList, AnatoliDbContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                //data.PurchaseOrderLineItems.Clear();
                foreach (PurchaseOrderLineItem item in dataList)
                {
                    item.PrivateLabelOwner = data.PrivateLabelOwner;
                    item.CreatedDate = item.LastUpdate = data.CreatedDate;
                    //LineItemRepository.Add(item);
                }
            });
            return data;
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
                order = PostOnlineData(WebApiURIHelper.SaveOrderLocalURI, data, true);
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
