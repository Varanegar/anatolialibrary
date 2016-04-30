using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Common.ViewModel
{
    public class AreaPointView
    {
        public Guid Id { set; get; }
        public Guid AreaId { set; get; }
        public Guid? CstId { set; get; }
        public double Lng { set; get; }
        public double Lat { set; get; }
        public int Pr { set; get; }
    }
}
