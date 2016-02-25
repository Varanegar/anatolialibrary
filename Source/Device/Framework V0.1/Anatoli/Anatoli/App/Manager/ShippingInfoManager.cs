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

        //public static ShippingInfoModel GetDefault()
        //{
        //    SearchFilterParam f = new EqFilterParam("default_shipping", "1");
        //    SelectQuery dbQuery = new SelectQuery("shipping_info", f);
        //    return GetItem(dbQuery);
        //}
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
                await BaseDataAdapter<ShippingInfoModel>.UpdateItemAsync(command1);
                BasicParam cityP = new BasicParam("city", city);
                BasicParam provinceP = new BasicParam("province", province);
                BasicParam zoneP = new BasicParam("zone", zone);
                BasicParam districtP = new BasicParam("district", district);
                BasicParam addressP = new BasicParam("address", address);
                BasicParam nameP = new BasicParam("name", name);
                BasicParam telP = new BasicParam("tel", tel);
                BasicParam defaultShipping = new BasicParam("default_shipping", "1");
                var command2 = new InsertCommand("shipping_info", cityP, provinceP, zoneP, districtP, addressP, nameP, telP, defaultShipping);
                return await BaseDataAdapter<ShippingInfoModel>.UpdateItemAsync(command2) > 0 ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static async Task<List<DeliveryTimeModel>> GetAvailableDeliveryTimes(string storeId, DateTime now, string deliveryType)
        {
            List<DeliveryTimeModel> times = new List<DeliveryTimeModel>();
            //SelectQuery query = new SelectQuery("stores_calendar", new EqFilterParam("StoreId", storeId), new GreaterFilterParam("Date", now.ConvertToUnixTimestamp().ToString()));
            SelectQuery query;
            if (deliveryType.Equals(DeliveryTypeModel.DeliveryType.Equals(deliveryType)))
                query = new SelectQuery("stores_calendar", new EqFilterParam("StoreId", storeId), new EqFilterParam("CalendarTypeValueId", StoreCalendarViewModel.StoreActivedeliveryTime));
            else
                query = new SelectQuery("stores_calendar", new EqFilterParam("StoreId", storeId), new EqFilterParam("CalendarTypeValueId", StoreCalendarViewModel.StoreOpenTime));

            var result = await BaseDataAdapter<StoreCalendarViewModel>.GetListAsync(query);
            var time = new TimeSpan(DateTime.Now.Hour, 30, 0);
            if (time > result.First().FromTime)
            {
                for (TimeSpan i = time; i < result.First().ToTime; i += TimeSpan.FromMinutes(30))
                {
                    var t = new DeliveryTimeModel();
                    t.time = i.ToString();
                    t.timespan = i;
                    t.UniqueId = Guid.NewGuid().ToString().ToUpper();
                    times.Add(t);
                }
            }
            return times;
        }

        public enum ShippingDateOptions
        {
            Today = 1,
            Tommorow = 2
        }
        public enum ShippingTimeOptions
        {
            _8To10 = 1,
            _10To12 = 2,
            _12To14 = 3,
            _14To16 = 4,
            _16To18 = 5,
            _18To20 = 6,
        }
    }

    public class DateOption
    {
        public ShippingInfoManager.ShippingDateOptions date;
        public string itemName;
        public DateOption(string name, ShippingInfoManager.ShippingDateOptions date)
        {
            this.itemName = name;
            this.date = date;
        }
        public override string ToString()
        {
            return itemName;
        }
    }

    public class TimeOption
    {
        public ShippingInfoManager.ShippingTimeOptions time;
        public string itemName;
        public TimeOption(string name, ShippingInfoManager.ShippingTimeOptions time)
        {
            this.itemName = name;
            this.time = time;
        }
        public override string ToString()
        {
            return itemName;
        }
    }
}
