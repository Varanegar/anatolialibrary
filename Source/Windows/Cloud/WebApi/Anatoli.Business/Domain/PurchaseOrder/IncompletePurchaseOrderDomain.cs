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
    public class IncompletePurchaseOrderDomain : BusinessDomain<IncompletePurchaseOrderViewModel>, IBusinessDomain<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>
    {
        #region Properties
        public IAnatoliProxy<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel> Proxy { get; set; }
        public IRepository<IncompletePurchaseOrder> Repository { get; set; }
        public IRepository<IncompletePurchaseOrderLineItem> LineItemRepository { get; set; }

        #endregion

        #region Ctors
        IncompletePurchaseOrderDomain() { }
        public IncompletePurchaseOrderDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public IncompletePurchaseOrderDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new IncompletePurchaseOrderRepository(dbc), new IncompletePurchaseOrderLineItemRepository(dbc), 
            new PrincipalRepository(dbc), AnatoliProxy<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public IncompletePurchaseOrderDomain(IIncompletePurchaseOrderRepository IncompletePurchaseOrderRepository, IIncompletePurchaseOrderLineItemRepository IncompletePurchaseOrderLineItemRepository, 
            IPrincipalRepository principalRepository, IAnatoliProxy<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel> proxy)
        {
            Proxy = proxy;
            Repository = IncompletePurchaseOrderRepository;
            LineItemRepository = IncompletePurchaseOrderLineItemRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<IncompletePurchaseOrderViewModel>> GetAll()
        {
            var data = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(data.ToList()); ;
        }

        public async Task<List<IncompletePurchaseOrderViewModel>> GetAllByCustomerId(string customerId)
        {
            Guid customerGuid = Guid.Parse(customerId);
            var data = await Repository.FindAllAsync(p => p.CustomerId == customerGuid);
            data.ToList().ForEach(item =>
                {
                    Repository.DbContext.Entry(item).Collection(c => c.IncompletePurchaseOrderLineItems).Load();
                });

            return Proxy.Convert(data.ToList()); ;
        }

        public async Task<List<IncompletePurchaseOrderViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task<List<IncompletePurchaseOrderViewModel>> PublishAsync(List<IncompletePurchaseOrderViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (IncompletePurchaseOrder item in dataList)
                {
                    
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentData = Repository.GetQuery().Where(p => p.CustomerId == item.CustomerId).FirstOrDefault();
                    if (currentData != null)
                    {
                        currentData.CityRegionId = item.CityRegionId;
                        currentData.DeliveryDate = item.DeliveryDate;
                        currentData.DeliveryFromTime = item.DeliveryFromTime;
                        currentData.DeliveryToTime = item.DeliveryToTime;
                        currentData.DeliveryTypeId = item.DeliveryTypeId;
                        currentData.OrderShipAddress = item.OrderShipAddress;
                        currentData.Phone = item.Phone;
                        currentData.StoreId = item.StoreId;
                        currentData.Transferee = item.Transferee;
                        currentData.LastUpdate = DateTime.Now;
                        //currentData = await SetLineItemData(currentData, item.IncompletePurchaseOrderLineItems.ToList(), Repository.DbContext);
                        await Repository.UpdateAsync(currentData);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.IncompletePurchaseOrderLineItems != null)
                        {
                            item.IncompletePurchaseOrderLineItems.ToList().ForEach(itemDetail =>
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

        public async Task<List<IncompletePurchaseOrderViewModel>> Clear(List<IncompletePurchaseOrderViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var itemDetailData = LineItemRepository.GetQuery().Where(p => p.IncompletePurchaseOrderId == item.Id);
                    itemDetailData.ToList().ForEach(itemDetail =>
                        {
                            LineItemRepository.DbContext.IncompletePurchaseOrderLineItems.Remove(itemDetail);
                        });
                });
                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }

        public async Task<List<IncompletePurchaseOrderViewModel>> Delete(List<IncompletePurchaseOrderViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var itemImages = Proxy.ReverseConvert(dataViewModels);

                itemImages.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DbContext.IncompletePurchaseOrders.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }

        //public async Task<IncompletePurchaseOrder> SetLineItemData(IncompletePurchaseOrder data, List<IncompletePurchaseOrderLineItem> dataList, AnatoliDbContext context)
        //{
        //    await Task.Factory.StartNew(() =>
        //    {
        //        //data.IncompletePurchaseOrderLineItems.Clear();
        //        foreach (IncompletePurchaseOrderLineItem item in dataList)
        //        {
        //            item.PrivateLabelOwner = data.PrivateLabelOwner;
        //            item.CreatedDate = item.LastUpdate = data.CreatedDate;
        //            LineItemRepository.Add(item);
        //        }
        //    });
        //    return data;
        //}


        #endregion
    }
}
