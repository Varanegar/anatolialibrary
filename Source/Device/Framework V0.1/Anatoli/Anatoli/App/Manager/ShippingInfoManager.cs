using Anatoli.App.Model.AnatoliUser;
using Anatoli.App.Model.Store;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.App.Model;

namespace Anatoli.App.Manager
{
    public class ShippingInfoManager : BaseManager<ShippingInfoModel>
    {
        public static async Task<ShippingInfoModel> GetDefaultAsync()
        {
            SearchFilterParam f = new EqFilterParam("default_shipping", "1");
            SelectQuery dbQuery = new SelectQuery("shipping_info", f);
            return await BaseDataAdapter<ShippingInfoModel>.GetItemAsync(dbQuery);
        }
        public static async Task<bool> NewShippingAddress(string address, string province, string city, string zone, string district, string name, string tel)
        {
            UpdateCommand command1 = new UpdateCommand("shipping_info", new BasicParam("default_shipping", "0"));
            try
            {
                await DataAdapter.UpdateItemAsync(command1);
                BasicParam cityP = new BasicParam("city", city);
                BasicParam provinceP = new BasicParam("province", province);
                BasicParam zoneP = new BasicParam("zone", zone);
                BasicParam districtP = new BasicParam("district", district);
                BasicParam addressP = new BasicParam("address", address);
                BasicParam nameP = new BasicParam("name", name);
                BasicParam telP = new BasicParam("tel", tel);
                BasicParam defaultShipping = new BasicParam("default_shipping", "1");
                var command2 = new InsertCommand("shipping_info", cityP, provinceP, zoneP, districtP, addressP, nameP, telP, defaultShipping);
                return await DataAdapter.UpdateItemAsync(command2) > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
