using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.PersonnelAcitvityModel
{
    public class OrderActivityEventViewModel : BaseActivityEventViewModel
    {
        public string CustomerName { set; get; }
        public string CustomerCode { set; get; }
        public string StoreName { set; get; }

        public string Phone { set; get; }
        public string Address { set; get; }

        public int WatingTime { set; get; }
        public int StartTime { set; get; }
        public int EndTime { set; get; }
        public int OrderQty { set; get; }
        public long OrderAmunt { set; get; }
        
    }
}
