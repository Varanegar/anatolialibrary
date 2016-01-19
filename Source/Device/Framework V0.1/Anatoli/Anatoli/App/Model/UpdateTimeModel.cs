using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model
{
    public class UpdateTimeModel : BaseDataModel
    {
        public string table_name { get; set; }
        public string update_time { get; set; }
    }
}
