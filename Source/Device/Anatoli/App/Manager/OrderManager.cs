using Anatoli.App.Model.Store;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class OrderManager : BaseManager<OrderModel>
    {
        public static async Task<bool> SaveOrderAsync(PurchaseOrderViewModel order)
        {
            try
            {
                InsertCommand insertCommand = new InsertCommand("orders",
                    new BasicParam("UniqueId", order.UniqueId.ToUpper()),
                     new BasicParam("order_price", order.FinalNetAmount.ToString()),
                                    new BasicParam("order_id", order.AppOrderNo.ToString()),
                                    new BasicParam("store_id", order.StoreGuid.ToString().ToUpper()),
                                    new BasicParam("order_status", order.PurchaseOrderStatusValueId.ToString().ToUpper()),
                                new BasicParam("order_date", order.OrderPDate));
                var result = await DataAdapter.UpdateItemAsync(insertCommand);
                if (result > 0)
                {
                    List<List<BasicParam>> parametres = new List<List<BasicParam>>();
                    foreach (var item in order.LineItems)
                    {
                        var p = new List<BasicParam>();
                        p.Add(new BasicParam("order_id", order.AppOrderNo.ToString()));
                        p.Add(new BasicParam("product_id", item.UniqueId));
                        p.Add(new BasicParam("product_count", item.FinalQty.ToString()));
                        p.Add(new BasicParam("product_price", item.FinalNetAmount.ToString()));
                        parametres.Add(p);
                    }
                    InsertAllCommand command2 = new InsertAllCommand("order_items", parametres);
                    var r = await DataAdapter.UpdateItemAsync(command2);
                    if (r > 0)
                    {
                        await ShoppingCardManager.ClearAsync();
                        return true;
                    }
                }
                return false;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<OrderModel> GetOrderAsync(string orderId)
        {
            try
            {
                SelectQuery query = new SelectQuery("orders_view", new EqFilterParam("order_id", orderId));
                return await BaseDataAdapter<OrderModel>.GetItemAsync(query);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<List<PurchaseOrderViewModel>> DownloadOrdersAsync(string customerId)
        {
            var data = new RequestModel.CustomerRequestModel();
            data.customerId = customerId;
            var list = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<List<PurchaseOrderViewModel>>(TokenType.AppToken, Configuration.WebService.Purchase.OrdersList,data);
            return list;
        }

        //public static async Task<List<PurchaseOrderStatusHistoryViewModel>> GetOrderHistoryAsync(string orderId)
        //{
        //    var list = await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<List<PurchaseOrderStatusHistoryViewModel>>(TokenType.AppToken, Configuration.WebService.Purchase.OrderHistory + "&poId=" + orderId);
        //    return list;
        //}
        public static async Task SyncOrdersAsync(string customerId)
        {
            try
            {
                var orders = await OrderManager.DownloadOrdersAsync(customerId);
                if (orders != null)
                {
                    SelectQuery query = new SelectQuery("orders");
                    var localOrders = await BaseDataAdapter<OrderModel>.GetListAsync(query);
                    Dictionary<long, OrderModel> ordersDict = new Dictionary<long, OrderModel>();
                    foreach (var item in localOrders)
                    {
                        if (!ordersDict.ContainsKey(item.order_id))
                        {
                            ordersDict.Add(item.order_id, item);
                        }
                    }
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        connection.BeginTransaction();
                        foreach (var item in orders)
                        {
                            if (ordersDict.ContainsKey(item.AppOrderNo))
                            {
                                UpdateCommand uComand = new UpdateCommand("orders", new EqFilterParam("order_id", item.AppOrderNo.ToString()),
                                    new BasicParam("order_status", item.PurchaseOrderStatusValueId.ToString().ToUpper()));
                                var dbquery = connection.CreateCommand(uComand.GetCommand());
                                int t = dbquery.ExecuteNonQuery();
                            }
                            else
                            {
                                InsertCommand insertCommand = new InsertCommand("orders",
                                    new BasicParam("UniqueId", item.UniqueId.ToUpper()),
                                    new BasicParam("order_price", item.FinalNetAmount.ToString()),
                                    new BasicParam("order_id", item.AppOrderNo.ToString()),
                                    new BasicParam("store_id", item.StoreGuid.ToString().ToUpper()),
                                    new BasicParam("order_status", item.PurchaseOrderStatusValueId.ToString().ToUpper()),
                                new BasicParam("order_date", item.OrderPDate));
                                var dbquery = connection.CreateCommand(insertCommand.GetCommand());
                                int t = dbquery.ExecuteNonQuery();
                            }
                        }
                        connection.Commit();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static StringQuery GetOrderQueryString()
        {
            StringQuery query = new StringQuery("SELECT * FROM orders_view");
            return query;
        }
    }
}
