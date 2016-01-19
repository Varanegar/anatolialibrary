using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model
{
    public class ItemImageManager : BaseManager<BaseDataAdapter<ItemImageViewModel>, ItemImageViewModel>
    {
        public static async Task SyncDataBase(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("products");
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Images + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await GetListAsync(null, q);
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (item.ImageType == ItemImageViewModel.ProductImageType)
                        {
                            UpdateCommand command = new UpdateCommand("products", new EqFilterParam("product_id", item.BaseDataId.ToUpper()), new BasicParam("image", item.ImageName.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        if (item.ImageType == ItemImageViewModel.ProductSiteGroupImageType)
                        {
                            UpdateCommand command = new UpdateCommand("categories", new EqFilterParam("cat_id", item.BaseDataId.ToUpper()), new BasicParam("cat_image", item.ImageName.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                    await SyncManager.SaveUpdateDateAsync("products");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
