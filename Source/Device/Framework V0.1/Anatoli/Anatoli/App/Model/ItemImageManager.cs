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
        public static async Task SyncDataBase()
        {
            try
            {
                var list = await GetListAsync(null, new RemoteQuery(TokenType.AppToken, Configuration.WebService.Images));
                if (list.Count == 0)
                {
                    throw new Exception("Could not download images");
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (item.ImageType == ItemImageViewModel.ProductImageType)
                        {
                            UpdateCommand command = new UpdateCommand("products", new EqFilterParam("product_id", item.BaseDataId), new BasicParam("image", item.ImageName));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        if (item.ImageType == ItemImageViewModel.ProductSiteGroupImageType)
                        {
                            UpdateCommand command = new UpdateCommand("categories", new EqFilterParam("cat_id", item.BaseDataId), new BasicParam("cat_image", item.ImageName));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
