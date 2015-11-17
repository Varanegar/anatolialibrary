using Anatoli.Framework.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class StoreDataModel : BaseDataModel
    {
        public string store_name { get; set; }
        public string store_address { get; set; }
        public string store_zone { get; set; }
        public string store_city { get; set; }
        public string store_province { get; set; }
        public int store_id { get; set; }
        public int selected { get; set; }
        public string store_tel { get; set; }
    }
}
