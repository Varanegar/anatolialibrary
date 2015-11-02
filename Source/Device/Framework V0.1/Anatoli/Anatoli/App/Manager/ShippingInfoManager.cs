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
        public static async Task<ShippingInfoModel> GetDefaultAsync()
        {
            SearchFilterParam f = new SearchFilterParam("default_shipping", "1");
            SelectQuery dbQuery = new SelectQuery("shipping_info", f);
            return await GetItemAsync(dbQuery);
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

        public static TimeOption[] GetAvailableDeliveryTimes(DateTime now, ShippingInfoManager.ShippingDateOptions dateOption)
        {
            TimeOption[] times = null;
            if (dateOption == ShippingInfoManager.ShippingDateOptions.Today)
            {
                if (now.Hour < 16)
                {
                    times = new TimeOption[] { new TimeOption("18 To 20", ShippingTimeOptions._18To20) };
                }
                if (now.Hour < 14)
                {
                    times = new TimeOption[] {
                        new TimeOption("16 To 18", ShippingTimeOptions._16To18),
                        new TimeOption("18 To 20",ShippingTimeOptions._18To20)};
                }
                if (now.Hour < 12)
                {
                    times = new TimeOption[] { 
                        new TimeOption("14 To 16", ShippingTimeOptions._14To16),
                        new TimeOption("16 To 18", ShippingTimeOptions._16To18),
                        new TimeOption("18 To 20",ShippingTimeOptions._18To20)};
                }
                if (now.Hour < 10)
                {
                    times = new TimeOption[] { 
                        new TimeOption("12 To 14", ShippingTimeOptions._12To14),
                        new TimeOption("14 To 16", ShippingTimeOptions._14To16),
                        new TimeOption("16 To 18", ShippingTimeOptions._16To18),
                        new TimeOption("18 To 20",ShippingTimeOptions._18To20)};
                }
                if (now.Hour < 8)
                {
                    times = new TimeOption[] { new TimeOption("10 To 12", ShippingTimeOptions._10To12),
                        new TimeOption("12 To 14", ShippingTimeOptions._12To14),
                        new TimeOption("14 To 16", ShippingTimeOptions._14To16),
                        new TimeOption("16 To 18", ShippingTimeOptions._16To18),
                        new TimeOption("18 To 20",ShippingTimeOptions._18To20)};
                }
            }
            else if (dateOption == ShippingInfoManager.ShippingDateOptions.Tommorow)
            {
                times = new TimeOption[] { 
                        new TimeOption("8 To 10", ShippingTimeOptions._8To10),
                        new TimeOption("10 To 12", ShippingTimeOptions._10To12),
                        new TimeOption("12 To 14", ShippingTimeOptions._12To14),
                        new TimeOption("14 To 16", ShippingTimeOptions._14To16),
                        new TimeOption("16 To 18", ShippingTimeOptions._16To18),
                        new TimeOption("18 To 20",ShippingTimeOptions._18To20)};
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
