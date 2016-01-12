using Anatoli.App.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
