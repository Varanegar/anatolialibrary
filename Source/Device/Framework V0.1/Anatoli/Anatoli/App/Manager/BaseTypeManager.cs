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
    public class BaseTypeManager : BaseManager<BaseTypeViewModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLogAsync(SyncManager.BaseTypesTbl);
                RemoteQuery q;
                if (lastUpdateTime == DateTime.MinValue)
                    q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.BaseDatas);
                else
                    q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.BaseDatas + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await BaseDataAdapter<BaseTypeViewModel>.GetListAsync(q);
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    await BaseDataAdapter<BaseTypeViewModel>.UpdateItemAsync(new DeleteCommand("delivery_types"));
                    await BaseDataAdapter<BaseTypeViewModel>.UpdateItemAsync(new DeleteCommand("pay_types"));
                    foreach (var item in list)
                    {
                        if (item.UniqueId.ToUpper() == BaseTypeViewModel.DeliveryType)
                        {
                            foreach (var value in item.BaseValues)
                            {
                                InsertCommand command = new InsertCommand("delivery_types", new BasicParam("name", value.BaseValueName),
                                 new BasicParam("id", value.UniqueId.ToString().ToUpper()));
                                var query = connection.CreateCommand(command.GetCommand());
                                int t = query.ExecuteNonQuery();
                            }
                        }
                        else if (item.UniqueId.ToUpper() == BaseTypeViewModel.PayType)
                        {
                            foreach (var value in item.BaseValues)
                            {
                                InsertCommand command = new InsertCommand("pay_types", new BasicParam("name", value.BaseValueName),
                                 new BasicParam("id", value.UniqueId.ToString().ToUpper()));
                                var query = connection.CreateCommand(command.GetCommand());
                                int t = query.ExecuteNonQuery();
                            }
                        }
                    }

                    connection.Commit();
                }
                await SyncManager.AddLogAsync(SyncManager.BaseTypesTbl);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<List<DeliveryTypeModel>> GetDeliveryTypesAsync()
        {
            return await BaseDataAdapter<DeliveryTypeModel>.GetListAsync(new StringQuery("SELECT * FROM delivery_types"));
        }

    }
}
