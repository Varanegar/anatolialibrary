using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.AnatoliUser
{
    public class ShippingInfoModel : BaseViewModel
    {
        public string shipping_id { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string default_shipping { get; set; }
    }
}
