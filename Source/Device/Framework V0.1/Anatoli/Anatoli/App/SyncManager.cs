using Anatoli.App.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;
namespace Anatoli.App
{
    public class SyncManager
    {
        public static async Task SyncDatabase()
        {
            await StoreUpdateManager.SyncDataBase();
            await ProductGroupManager.SyncDataBase();
            await ProductUpdateManager.SyncDataBase();
            await ProductPriceManager.SyncDataBase();
            SaveDBVersionAsync();
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
        public static async void SaveDBVersionAsync()
        {
            await Task.Run(() =>
            {
                AnatoliClient.GetInstance().FileIO.WriteAllText("1", AnatoliClient.GetInstance().FileIO.GetDataLoction(), "dbVersion");
            });
        }
    }
}
