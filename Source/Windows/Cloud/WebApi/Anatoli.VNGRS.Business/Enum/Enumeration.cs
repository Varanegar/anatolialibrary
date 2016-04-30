using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Common.Enum
{
    class Enumeration
    {
    }

    public enum ELogLevel
    {
        DEBUG,
        INFO,
        ERROR
    }

    public enum ETransactionType {ORDER,  LACK_OF_ORDER, LACK_OF_VISIT, STOP_WITHOUT_CUSTOME, MULTI}
    public enum ESubType { IN_LINE = 0, OUTE_LINE = 1, NEW = 2, DISTANCE = 3 }

    public enum PointType
    {
        Point = 0,
        CustomerRout = 1,
        CustomerOtherRout = 2,
        CustomerWithoutRout = 0,

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
