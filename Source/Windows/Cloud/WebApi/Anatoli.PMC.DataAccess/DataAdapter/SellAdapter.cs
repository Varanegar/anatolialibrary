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
        public  void SavePurchaseOrder(PMCSellViewModel orderInfo, PMCCustomerViewModel customer)
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
        }
    }
}

