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
    public class ProductPriceManager : BaseManager<BaseDataAdapter<ProductPriceModel>, ProductPriceModel>
    {
        public static async Task SyncDataBase()
        {
            var list = await GetListAsync(null, new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.PricesView));

        }
    }
}
