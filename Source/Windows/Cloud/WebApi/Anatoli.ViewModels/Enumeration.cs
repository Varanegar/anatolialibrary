namespace Anatoli.ViewModels
{

    public enum ETransactionType {ORDER,  LACK_OF_ORDER, LACK_OF_VISIT, STOP_WITHOUT_CUSTOME, MULTI}
    public enum ESubType { IN_LINE = 0, OUTE_LINE = 1, NEW = 2, DISTANCE = 3 }

    public enum EPointType
    {
        Point = 0,
        CustomerRoute = 1,
        CustomerOtherRoute = 2,
        CustomerWithoutRoute = 0,

        Order = 0,  
        LackOfOrder = 1, 
        LackOfVisit = 2, 
        StopWithoutCustomer = 3,
        StopWithoutActivity = 4,
        Customer = 5,
        OuteLine= 6,
        GpsOff = 7,
        
        Multi = 10

    }


}
