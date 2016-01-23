using Anatoli.App.Model;
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
    public class BaseTypeManager : BaseManager<BaseDataAdapter<BaseTypeViewModel>, BaseTypeViewModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("basetypes");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.BaseDatas + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (item.UniqueId == BaseTypeViewModel.DeliveryType)
                        {
                            var c = await LocalUpdateAsync(new DeleteCommand("delivery_types"));
                            foreach (var value in item.BaseValues)
                            {
                                InsertCommand command = new InsertCommand("delivery_types", new BasicParam("name", value.BaseValueName),
                                 new BasicParam("id", value.UniqueId.ToString()));
                                var query = connection.CreateCommand(command.GetCommand());
                                int t = query.ExecuteNonQuery();
                            }
                        }
                        else if (item.UniqueId == BaseTypeViewModel.PayType)
                        {
                            var c = await LocalUpdateAsync(new DeleteCommand("pay_types"));
                            foreach (var value in item.BaseValues)
                            {
                                InsertCommand command = new InsertCommand("pay_types", new BasicParam("name", value.BaseValueName),
                                 new BasicParam("id", value.UniqueId.ToString()));
                                var query = connection.CreateCommand(command.GetCommand());
                                int t = query.ExecuteNonQuery();
                            }
                        }
                    }

                    connection.Commit();
                }
                await SyncManager.SaveUpdateDateAsync("basetypes");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
