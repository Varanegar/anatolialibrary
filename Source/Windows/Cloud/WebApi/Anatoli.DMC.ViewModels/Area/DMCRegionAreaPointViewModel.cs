using System;

namespace Anatoli.DMC.ViewModels.Area
{
    public class DMCRegionAreaPointViewModel
    {
        public Guid UniqueId { set; get; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public int Priority { set; get; }
        public Guid? CustomerUniqueId { set; get; }

    }
}
