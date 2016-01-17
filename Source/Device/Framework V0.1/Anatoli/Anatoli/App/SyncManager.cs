using Anatoli.App.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model;
namespace Anatoli.App
{
    public class SyncManager
    {
        public static async Task SyncDatabase()
        {
            await CityRegionUpdateManager.SyncDataBase();
            await StoreUpdateManager.SyncDataBase();
            await ProductGroupManager.SyncDataBase();
            await ProductUpdateManager.SyncDataBase();
            await ProductPriceManager.SyncDataBase();
            await SaveDBVersionAsync();
        }
        public static async Task<bool> SaveUpdateDateAsync(string tableName)
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
        public static async Task<DateTime> GetLastUpdateDateAsync(string tableName)
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
        public static int LoadDBVersion()
        {
            try
            {
                string versionString = AnatoliClient.GetInstance().FileIO.ReadAllText(AnatoliClient.GetInstance().FileIO.GetDataLoction(), "dbVersion");
                return int.Parse(versionString);
            }
            catch (Exception)
            {
                return 0;
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
        public static async Task SaveDBVersionAsync()
        {
            await Task.Run(() =>
            {
                AnatoliClient.GetInstance().FileIO.WriteAllText("1", AnatoliClient.GetInstance().FileIO.GetDataLoction(), "dbVersion");
            });
        }
    }
}
