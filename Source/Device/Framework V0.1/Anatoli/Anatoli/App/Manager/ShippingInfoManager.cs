using Anatoli.App.Model.AnatoliUser;
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
    public class ShippingInfoManager : BaseManager<BaseDataAdapter<ShippingInfoModel>, ShippingInfoModel>
    {

        public static ShippingInfoModel GetDefault()
        {
            SearchFilterParam f = new SearchFilterParam("default_shipping", "1");
            SelectQuery dbQuery = new SelectQuery("shipping_info", f);
            return GetItem(dbQuery);
        }
        public static async Task<bool> NewShippingAddress(string address, string name, string tel)
        {
            UpdateCommand command1 = new UpdateCommand("shipping_info", new BasicParam("default_shipping", "0"));
            try
            {
                await LocalUpdateAsync(command1);
                BasicParam cityP = new BasicParam("city", "تهران");
                BasicParam addressP = new BasicParam("address", address);
                BasicParam nameP = new BasicParam("name", name);
                BasicParam telP = new BasicParam("tel", tel);
                BasicParam defaultShipping = new BasicParam("default_shipping", "1");
                var command2 = new InsertCommand("shipping_info", cityP, addressP, nameP, telP, defaultShipping);
                return await LocalUpdateAsync(command2) > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
