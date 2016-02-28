using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model;
using Anatoli.Framework.DataAdapter;
namespace Anatoli.App.Manager
{
    public class SyncManager
    {
        public static string StoresTbl = "stores";
        public static string BaseTypesTbl = "basetypes";
        public static string GroupsTbl = "categories";
        public static string CityRegionTbl = "cityregion";
        public static string ImagesTbl = "images";
        public static string PriceTbl = "products_price";
        public static string ProductTbl = "products";
        public static string BasketTbl = "baskets";
        public static string OnHand = "onhand";
        public static string StoreCalendarTbl = "stores_calendar";
        public static string UpdateCompleted = "CompleteUpdate";
        public static async Task<bool> AddLogAsync(string tableName)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        InsertCommand command = new InsertCommand("updates", new BasicParam("table_name", tableName),
                            new BasicParam("update_time", ConvertToUnixTimestamp(DateTime.Now).ToString()));
                        var query = connection.CreateCommand(command.GetCommand());
                        int t = query.ExecuteNonQuery();
                        if (t > 0) return true; else return false;
                    }
                });
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static async Task<bool> RemoveLogAsync(string tableName)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        DeleteCommand command = new DeleteCommand("updates", new EqFilterParam("table_name", tableName));
                        var query = connection.CreateCommand(command.GetCommand());
                        int t = query.ExecuteNonQuery();
                        if (t > 0) return true; else return false;
                    }
                });
            }
            catch (Exception)
            {

                return false;
            }
        }
        public static async Task<DateTime> GetLogAsync(string tableName)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        var query = connection.CreateCommand(String.Format("SELECT * FROM updates WHERE table_name = '{0}' order by update_time DESC LIMIT 0,1", tableName));
                        var time = query.ExecuteQuery<UpdateTimeModel>();
                        var d = double.Parse(time.First().update_time);
                        return ConvertFromUnixTimestamp(d);
                    }
                });
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }


        public static async Task ClearDatabase()
        {
            try
            {
                await DataAdapter.UpdateItemAsync(new DeleteCommand("delivery_types"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("pay_types"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("cityregion"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("products_price"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("products"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("stores"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("categories"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("updates"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("store_onhand"));
                await DataAdapter.UpdateItemAsync(new DeleteCommand("stores_calendar"));
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
