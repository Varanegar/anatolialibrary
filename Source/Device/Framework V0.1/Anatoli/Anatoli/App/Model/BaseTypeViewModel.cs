using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model
{
    public class BaseTypeViewModel : BaseDataModel
    {
        public static string DeliveryType = "f5ffad55-6e39-40bd-a95d-12a34ba4d005".ToUpper();
        public static string PayType = "f17b8898-d39f-4955-9757-a6b31767f5c7".ToUpper();
        public string BaseTypeDesc { get; set; }
        public List<BaseValueViewModel> BaseValues { get; set; }
    }
}
