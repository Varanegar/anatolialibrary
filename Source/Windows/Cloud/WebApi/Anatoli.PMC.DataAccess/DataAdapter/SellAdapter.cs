
using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.ViewModels.Base;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class SellAdapter : BaseAdapter
    {
        private static SellAdapter instance = null;
        public static SellAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SellAdapter();
                }
                return instance;
            }
        }
        SellAdapter() { }
        public List<PurchaseOrderViewModel> GetPurchaseOrderByCustomerId(string customerId, string statusId, bool getAllOrderTypes)
        {
            List<PurchaseOrderViewModel> result = new List<PurchaseOrderViewModel>();
            var stores = StoreConfigHeler.Instance.AllStoreConfigs;
            stores.ForEach(item =>
            {
                if(item.CenterId != 1)
                result.AddRange(GetPurchaseOrderByCustomerId(customerId, statusId, item.UniqueId, getAllOrderTypes, item.ConnectionString));
            });
            return result;
        }
        public List<PurchaseOrderViewModel> GetPurchaseOrderByCustomerId(string customerId, string statusId, string centerId, bool getAllOrderTypes, string connectionString)
        {
            log.Debug(" customerId " + customerId + " & centerId " + centerId);
            List<PurchaseOrderViewModel> result = new List<PurchaseOrderViewModel>();

            using (var context = new DataContext(centerId, connectionString, Transaction.No))
            {
                try
                {
                    IEnumerable<PurchaseOrderViewModel> data = null;
                    if (getAllOrderTypes)
                    {
                        if (statusId != null)
                            data = context.All<PurchaseOrderViewModel>(DBQuery.Instance.GetSellInfoAllTypes() + " and C.CustomerSiteUserId='" + customerId + "' and SS.UniqueId = '" + statusId + "'");
                        else
                            data = context.All<PurchaseOrderViewModel>(DBQuery.Instance.GetSellInfoAllTypes() + " and C.CustomerSiteUserId='" + customerId + "'");
                    }
                    else
                    {
                        if (statusId != null)
                            data = context.All<PurchaseOrderViewModel>(DBQuery.Instance.GetSellInfo() + " and C.CustomerSiteUserId='" + customerId + "' and SS.UniqueId = '" + statusId + "'");
                        else
                            data = context.All<PurchaseOrderViewModel>(DBQuery.Instance.GetSellInfo() + " and C.CustomerSiteUserId='" + customerId + "'");
                    }
                    result = data.ToList();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    throw ex;
                }
                finally
                {
                    context.Dispose();
                }
            }
            return result;
        }
        public List<PurchaseOrderLineItemViewModel> GetPurchaseOrderLineItemsByPOId(string pOId, string centerId, bool getAllOrderTypes)
        {
            List<PurchaseOrderLineItemViewModel> result = new List<PurchaseOrderLineItemViewModel>();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;

            using (var context = new DataContext(Transaction.No))
            {
                try
                {
                    if (getAllOrderTypes)
                    {
                        var data = context.All<PurchaseOrderLineItemViewModel>(DBQuery.Instance.GetSellDetailInfoAllTypes() + " where S.UniqueId='" + pOId + "'");
                        result = data.ToList();
                    }
                    else
                    {
                        var data = context.All<PurchaseOrderLineItemViewModel>(DBQuery.Instance.GetSellDetailInfo() + " where S.UniqueId='" + pOId + "'");
                        result = data.ToList();
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    throw ex;
                }
                finally
                {
                    context.Dispose();
                }
            }
            return result;
        }
        public List<PurchaseOrderStatusHistoryViewModel> GetPurchaseOrderStatusByPOId(string pOId, string centerId)
        {
            List<PurchaseOrderStatusHistoryViewModel> result = new List<PurchaseOrderStatusHistoryViewModel>();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;

            using (var context = new DataContext(Transaction.No))
            {
                try
                {
                    var data = context.All<PurchaseOrderStatusHistoryViewModel>(DBQuery.Instance.GetSellActionInfo() + " where S.UniqueId='" + pOId + "'");
                    result = data.ToList();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    throw ex;
                }
                finally
                {
                    context.Dispose();
                }
            }
            return result;
        }
        public long SavePurchaseOrder(PMCSellViewModel orderInfo, PMCCustomerViewModel customer)
        {
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(orderInfo.CenterId).ConnectionString;

            using (var context = new DataContext(Transaction.Begin))
            {
                try
                {
                    if (CustomerAdapter.Instance.IsCustomerValid(customer.CustomerSiteUserId))
                        orderInfo.CustomerId = CustomerAdapter.Instance.GetCustomerId(customer.CustomerSiteUserId);
                    else
                    {
                        DataObject<PMCCustomerViewModel> customerDataObject = new DataObject<PMCCustomerViewModel>("Customer", "InvalidId");
                        customer.CustomerId = GeneralCommands.GetId(context, "Customer");
                        customer.CustomerCode = CustomerAdapter.Instance.GetNewCustomerCode(customer.CustomerSiteUserId);
                        customerDataObject.Insert(customer, context);
                        orderInfo.CustomerId = customer.CustomerId;
                        var genScript = context.GetValue<string>(@"EXEC usp_GenData 'Customer','customerid=" + customer.CustomerId + "' ,1,1");

                        StoreConfigHeler.Instance.AllStoreConfigs.ForEach(item =>
                        {
                            if (item.CenterId != 1)
                            {
                                using (var contextTemp = new DataContext(item.CenterId.ToString(), item.ConnectionString, Transaction.Begin))
                                {
                                    try
                                    {
                                        contextTemp.Execute(@"update CenterSetting set LogIsActive = 0");
                                        contextTemp.Execute(genScript);
                                        contextTemp.Execute(@"update CenterSetting set LogIsActive = 1");
                                        contextTemp.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        contextTemp.Rollback();
                                        log.Error(ex.Message, ex);
                                        throw ex;
                                    }
                                }
                            }
                        });
                        context.Commit();
                    }
                }catch(Exception ex)
                {
                    context.Rollback();

                    log.Error(ex.Message, ex);
                    throw ex;
                }
                finally
                {
                    context.Dispose();
                }
            }

            using (var context = new DataContext(orderInfo.CenterId.ToString(), connectionString, Transaction.Begin))
            {
                try
                {
                    DataObject<PMCSellViewModel> sellDataObject = new DataObject<PMCSellViewModel>("Sell", "InvalidId");
                    orderInfo.SellId = GeneralCommands.GetId(context, "Sell");
                    orderInfo.FiscalYearId = GeneralCommands.GetFiscalYearId(context);
                    orderInfo.CashSessionId = GeneralCommands.GetCashSessionId(context);
                    orderInfo.RequestNo = GeneralCommands.GetRequestNo(context, orderInfo.FiscalYearId);
                    orderInfo.RequestDateTime = PersianDate.Now.ToString() + " " + DateTime.Now.ToString("HH:mm");
                    orderInfo.SellNotInPersonTypeId = GetActionSourceId(orderInfo.SellNotInPersonTypeGuid.ToString());
                    orderInfo.DeliveryTypeId = GetDeliveryTypeId(orderInfo.DeliveryTypeGuid.ToString());
                    sellDataObject.Insert(orderInfo, context);
                    DataObject<PMCSellDetailViewModel> lineItemDataObject = new DataObject<PMCSellDetailViewModel>("SellDetail", "InvalidId");
                    orderInfo.SellDetail.ForEach(item =>
                    {   
                        item.SellId = orderInfo.SellId;
                        item.SellDetailId = GeneralCommands.GetId(context, "SellDetail");
                        lineItemDataObject.Insert(item, context);
                    });
                    context.Commit();
                }
                catch (Exception ex)
                {
                    context.Rollback();

                    log.Error(ex.Message, ex);
                    throw ex;
                }
                finally
                {
                    context.Dispose();
                }
            }

            return orderInfo.RequestNo;
        }


        private int GetDeliveryTypeId(string DeliveryTypeGuid)
        {
            int result = 0;
            switch (DeliveryTypeGuid)
            {
                case "CE4AEE25-F8A7-404F-8DBA-80340F7339CC":
                    result = 1;
                    break;
                case "BE2919AB-5564-447A-BE49-65A81E6AF712":
                    result = 2;
                    break;
                default:
                    result = 1;
                    break;
            }
            return result;
        }
        private int GetActionSourceId(string ActionSourceGuid)
        {
            int result = 0;
            switch (ActionSourceGuid)
            {
                case "65DEC223-059E-48BA-8281-E4FAAFF6E32D":
                    result = 1;
                    break;
                case "0410F5BD-0C01-4E32-A4D9-D2F4DCC46003":
                    result = 2;
                    break;
                case "6CF27F09-E162-4802-A451-9BC3304A8130":
                    result = 3;
                    break;
                default:
                    result = 3;
                    break;
            }
            return result;
        }
    }
}

