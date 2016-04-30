using System;
using TrackingMap.Common.Enum;

namespace TrackingMap.Service.ViewModel
{
    public class PointView
    {
        public Guid Id { set; get; }
        public string JData { set; get; }
        public string Desc { set; get; }
        
        public string Lable { set; get; }

        public double Longitude { set; get; }
        
        public double Latitude { set; get; }

        //public DateTime Timestpm { set; get;}

        public PointType PointType { set; get; }
        
        public int SubType { set; get; }

        public Guid MasterId { set; get; }

        public Guid? ReferId { set; get; }
        
        public bool IsLeaf { set; get; }
    }
}
