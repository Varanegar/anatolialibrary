using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.ViewModels.Base;
using Anatoli.Rastak.ViewModels.Order;
using Anatoli.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.DataAdapter
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
        public List<PurchaseOrderViewModel> GetPurchaseOrderByCustomerId(string customerId, string statusId, string centerId)
        {
            List<PurchaseOrderViewModel> result = new List<PurchaseOrderViewModel>();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;

            using (var context = new DataContext(Transaction.No))
            {
                try
                {
                    IEnumerable<PurchaseOrderViewModel> data = null;
                    if(statusId != null)
                        data = context.All<PurchaseOrderViewModel>(DBQuery.Instance.GetSellInfo() + " where customerId='" + customerId + "' and statusId = '" + statusId + "'");
                    else
                        data = context.All<PurchaseOrderViewModel>(DBQuery.Instance.GetSellInfo() + " where customerId='" + customerId + "'");

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
        public List<PurchaseOrderLineItemViewModel> GetPurchaseOrderLineItemsByPOId(string pOId, string centerId)
        {
            List<PurchaseOrderLineItemViewModel> result = new List<PurchaseOrderLineItemViewModel>();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;

            using (var context = new DataContext(Transaction.No))
            {
                try
                {
                    var data = context.All<PurchaseOrderLineItemViewModel>(DBQuery.Instance.GetSellDetailInfo() + " where sellId='" + pOId + "'");
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
        public List<PurchaseOrderStatusHistoryViewModel> GetPurchaseOrderStatusByPOId(string pOId, string centerId)
        {
            List<PurchaseOrderStatusHistoryViewModel> result = new List<PurchaseOrderStatusHistoryViewModel>();
            var connectionString = StoreConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;

            using (var context = new DataContext(Transaction.No))
            {
                try
                {
                    var data = context.All<PurchaseOrderStatusHistoryViewModel>(DBQuery.Instance.GetSellActionInfo() + " where sellId='" + pOId + "'");
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
        public long SavePurchaseOrder(RastakSellViewModel orderInfo, RastakCustomerViewModel customer)
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
                        DataObject<RastakCustomerViewModel> customerDataObject = new DataObject<RastakCustomerViewModel>("Customer", "InvalidId");
                        customer.CustomerId = GeneralCommands.GetId(context, "Customer");
                        customer.CustomerCode = CustomerAdapter.Instance.GetNewCustomerCode(customer.CustomerSiteUserId);
                        context.Execute(@"update CenterSetting set LogIsActive = 0");
                        customerDataObject.Insert(customer, context);
                        context.Execute(@"update CenterSetting set LogIsActive = 1");
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
                    DataObject<RastakSellViewModel> sellDataObject = new DataObject<RastakSellViewModel>("Sell", "InvalidId");
                    orderInfo.SellId = GeneralCommands.GetId(context, "Sell");
                    orderInfo.FiscalYearId = GeneralCommands.GetFiscalYearId(context);
                    orderInfo.CashSessionId = GeneralCommands.GetCashSessionId(context);
                    orderInfo.CashSessionStatusId = 0;
                    orderInfo.RequestNo = GeneralCommands.GetRequestNo(context, orderInfo.FiscalYearId);
                    orderInfo.RequestDateTime = PersianDate.Now.ToString();

                    sellDataObject.Insert(orderInfo, context);
                    DataObject<RastakSellDetailViewModel> lineItemDataObject = new DataObject<RastakSellDetailViewModel>("SellDetail", "InvalidId");
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
    }
}

