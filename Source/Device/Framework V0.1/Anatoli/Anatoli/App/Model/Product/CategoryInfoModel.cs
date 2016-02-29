using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Product
{
    public class CategoryInfoModel : BaseViewModel
    {
        public string cat_id { get; set; }
        public string cat_parent { get; set; }
        public int cat_depth { get; set; }
        public string cat_name { get; set; }
        public string cat_image { get; set; }
    }
}
