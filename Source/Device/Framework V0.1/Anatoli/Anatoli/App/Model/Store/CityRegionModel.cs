using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class CityRegionModel : BaseViewModel
    {
        public string group_name { get; set; }
        public string group_id { get; set; }
        public string parent_id { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public int level { get; set; }
        public override string ToString()
        {
            return group_name;
        }
    }
}
