using System;

namespace Anatoli.ViewModels.VnGisModels
{
    public class PointViewModel
    {
        public Guid Id { set; get; }
        public string JData { set; get; }
        public string Desc { set; get; }
        
        public string Lable { set; get; }

        public double Longitude { set; get; }
        
        public double Latitude { set; get; }

        //public DateTime Timestpm { set; get;}

        public int PointType { set; get; }
        
        public int SubType { set; get; }

        public Guid MasterId { set; get; }

        public Guid? ReferId { set; get; }
        
        public bool IsLeaf { set; get; }
    }
}
