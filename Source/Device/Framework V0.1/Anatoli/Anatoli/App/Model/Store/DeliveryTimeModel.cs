using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Store
{
    public class DeliveryTimeModel : BaseDataModel
    {
        public TimeSpan timespan;
        public string time { get; set; }
        public override string ToString()
        {
            return time;
        }
    }
}
