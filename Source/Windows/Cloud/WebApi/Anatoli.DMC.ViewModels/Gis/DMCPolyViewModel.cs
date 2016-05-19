using System;
using System.Collections.Generic;

namespace Anatoli.DMC.ViewModels.Gis
{
    public class DMCPolyViewModel
    {
        public Guid? MasterId { set; get; }
        public List<DMCPointViewModel> Points { set; get; }
        public string Color { set; get; }
        public string Desc { set; get; }
        public string Lable { set; get; }
        public bool IsLeaf { set; get; }
        public string JData { set; get; }


    }
}
