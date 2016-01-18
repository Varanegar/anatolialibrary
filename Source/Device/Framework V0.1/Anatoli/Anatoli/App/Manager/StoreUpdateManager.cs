﻿using Anatoli.App.Model.Store;
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
    public class StoreUpdateManager : BaseManager<BaseDataAdapter<StoreUpdateModel>, StoreUpdateModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("stores");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.StoresView);
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                int c = await LocalUpdateAsync(new DeleteCommand("stores"));
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        InsertCommand command = new InsertCommand("stores", new BasicParam("store_id", item.UniqueId.ToUpper()),
                            new BasicParam("store_name", item.storeName.Trim()),
                            new BasicParam("store_address", item.address));
                        var query = connection.CreateCommand(command.GetCommand());
                        int t = query.ExecuteNonQuery();
                    }
                    connection.Commit();
                }
                await SyncManager.SaveUpdateDateAsync("stores");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
