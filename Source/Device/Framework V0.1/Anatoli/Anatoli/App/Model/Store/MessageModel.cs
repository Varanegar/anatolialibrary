using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class MessageModel : BaseDataModel
    {
        public string store_id { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public int new_flag { get; set; }
        public string store_name { get; set; }
    }
}
