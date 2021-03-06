﻿using System;

namespace Anatoli.DMC.ViewModels.Gis
{
    public class DMCPointViewModel
    {
        public Guid UniqueId { set; get; }
        public string JData { set; get; }
        public string Desc { set; get; }
        
        public string Lable { set; get; }

        public double Longitude { set; get; }
        
        public double Latitude { set; get; }

        //public DateTime Timestpm { set; get;}

        public Guid? PointType { set; get; }
        
        public Guid? SubType { set; get; }

        public Guid MasterId { set; get; }

        public Guid? ReferId { set; get; }
        
    }
}
