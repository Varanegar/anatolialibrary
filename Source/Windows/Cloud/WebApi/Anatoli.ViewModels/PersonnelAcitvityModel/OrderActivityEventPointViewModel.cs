using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anatoli.ViewModels.PersonnelAcitvityModel
{
    public class OrderActivityEventPointViewModel : PersonnelDailyActivityEventViewModel
    {
        public OrderActivityEventViewModel eventData { set; get; }
    }

    public class OrderActivityEventViewModel : BaseActivityEventViewModel
    {
        public string CustomerName { set; get; }
        public string CustomerCode { set; get; }
        public string StoreName { set; get; }

        public string Phone { set; get; }
        public string Address { set; get; }

        public string WatingTime { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public int OrderQty { set; get; }
        public long OrderAmunt { set; get; }
        
    }
}
