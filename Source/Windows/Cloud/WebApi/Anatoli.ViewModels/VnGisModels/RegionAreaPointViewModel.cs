using System;

namespace Anatoli.ViewModels.VnGisModels
{
    public class RegionAreaPointViewModel : BaseViewModel
    {
        public double Lng { set; get; }
        public double Lat { set; get; }
        public int Pr { set; get; }
        public Guid? CstId { set; get; }

    }
}
