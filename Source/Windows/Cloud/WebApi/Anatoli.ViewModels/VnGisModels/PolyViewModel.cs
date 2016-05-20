using System;
using System.Collections.Generic;

namespace Anatoli.ViewModels.VnGisModels
{
    public class PolyViewModel
    {
        public Guid? masterId { set; get; }
        public List<PointViewModel> points { set; get; }
        public string color { set; get; }
        public string desc { set; get; }
        public string lable { set; get; }
        public bool isLeaf { set; get; }
        public string jData { set; get; }


    }
}
