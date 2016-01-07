using Anatoli.App.Model.Product;
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
    public class ProductGroupManager : BaseManager<BaseDataAdapter<ProductGroupModel>, ProductGroupModel>
    {
        public static async Task<List<ProductGroupModel>> GetAllGroupsAsync()
        {
            return await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<List<ProductGroupModel>>(TokenType.AppToken, Configuration.WebService.Products.ProductGroups);
        }
        

    }
}
