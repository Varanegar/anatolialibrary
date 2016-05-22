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

        public Guid? PointType { set; get; }
        
        public Guid? SubType { set; get; }

        public Guid MasterId { set; get; }

        public Guid? ReferId { set; get; }
        
        public bool IsLeaf { set; get; }
    }
    /*
    public enum EPointType
    {
        Point = "",
        CustomerRout = '1B340061-0DFC-490B-9A72-91365916D911',
        CustomerOtherRout = '733E4674-57A3-4013-A6DB-6EF543356E1B',
        CustomerWithoutRout = 'A5BE354A-C0CB-4D47-B159-F156112408F8',

        Order = 0,
        LackOfOrder = 1,
        LackOfVisit = 2,
        StopWithoutCustomer = 3,
        StopWithoutActivity = 4,
        Customer = 5,
        OuteLine = 6,
        GpsOff = 7,

        Multi = 10

    }

     * */
}
