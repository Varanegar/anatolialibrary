using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anatoli.ViewModels.PersonnelAcitvityModel
{
    public class LackOfOrderActivityEventPointViewModel : PersonnelDailyActivityEventViewModel
    {
        public  LackeOfOrderActivityEventViewModel eventData { set; get; }
    }

    public class LackeOfOrderActivityEventViewModel : BaseActivityEventViewModel
    {
        public string CustomerName { set; get; }
        public string CustomerCode { set; get; }
        public string StoreName { set; get; }

        public string Phone { set; get; }
        public string Address { set; get; }

        public string Time { set; get; }
        public string Description { set; get; }
    }
}
