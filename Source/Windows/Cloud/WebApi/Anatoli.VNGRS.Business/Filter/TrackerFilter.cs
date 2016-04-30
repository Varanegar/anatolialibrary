using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Filter
{
    public class TrackerFilter
    {

        public bool Visitor { set; get; }
        public bool Machin { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public string FromTime { set; get; }
        public string ToTime { set; get; }
        public int Id { set; get; }

    }
}
