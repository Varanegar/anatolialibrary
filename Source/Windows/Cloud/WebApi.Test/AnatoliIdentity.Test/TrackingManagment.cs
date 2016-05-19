using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Anatoli.ViewModels.PersonnelAcitvityModel;
using Anatoli.ViewModels.RequestModel;
using System.Web.Script.Serialization;

namespace ClientApp
{
    public class TrackingManagment : BaseTestManagment
    {
        public static void TestSavePersonelActivitie(HttpClient client, string servserURI)
        {
            var ev = new OrderActivityEventViewModel()
            {
                Address = "تبريز -خيابان دارايي 2 اول ميدان صاحب الامر ",
                CustomerCode = "4110001",
                CustomerName = "عليرضا تاملي",
                OrderAmunt = 300000000,
                OrderQty = 59

            };

            var req = new PersonelTrackingRequestModel();
            var activity = new PersonnelDailyActivityEventViewModel()
            {
                UniqueId = Guid.Parse("e09fcb88-6105-e611-825f-5c93a2e09b3c"),
                CompanyPersonnelId= Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),
                CompanyPersonnelName = "عليرضا تاملي",
                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),
                CustomerName = "عليرضا تاملي",
                PersonnelDailyActivityVisitTypeId = Guid.Parse("B0000000-0000-0000-0000-000000000000"),
                PersonnelDailyActivityVisitTypeName  = "سفارش",
                PersonnelDailyActivityEventTypeId = Guid.Parse("B0000000-0000-0000-0000-000000000000"),
                PersonnelDailyActivityEventTypeName  = "سفارش",
                Latitude = 38.084812,
                Longitude = 46.295516,
                ShortDescription = "test",
                //ActivityDate = ,
                ActivityPDate= "1395/02/29",
                Event= ev
            };
            req.activity = activity;

            Call(client, servserURI + "api/dsd/tracking/svprsact", req);

        }


        public static void TestLoadPersonelsPath(HttpClient client, string servserURI)
        {
            var req = new PersonelTrackingRequestModel();
            var list = new List<Guid>();
            list.Add(Guid.Parse("AF1A3BBC-FFED-4E4A-977F-94B304C12140"));
            req.personelIds = list;

            Call(client, servserURI + "api/dsd/tracking/ldprspth", req);

        }

        public static void TestLoadPersonActivities(HttpClient client, string servserURI)
        {
            var req = new PersonelTrackingRequestModel();
            var list = new List<Guid>();
            list.Add(Guid.Parse("AF1A3BBC-FFED-4E4A-977F-94B304C12140"));
            req.personelIds = list;
            req.order = true;
            req.lackOrder = true;

            Call(client, servserURI + "api/dsd/tracking/ldprsacts", req);


        }

    }
}
