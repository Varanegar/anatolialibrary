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
        public static void TestSavePersonelActivitieOrder(HttpClient client, string servserURI)
        {
            var eventdata = new OrderActivityEventViewModel()
            {
                Address = "تبريز -خيابان دارايي 2 اول ميدان صاحب الامر ",
                CustomerCode = "4110001",
                CustomerName = "عليرضا تاملي",
                OrderAmunt = 300000000,
                OrderQty = 59

            };

            var eventpoint = new OrderActivityEventPointViewModel()
            {
                UniqueId = Guid.NewGuid(),

                CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"),//"بهزاد احمديان هزه بران"
                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),//"عليرضا تاملي",                
                PersonnelDailyActivityVisitTypeId = Guid.Parse("AF24B66E-069C-4EB6-926D-852664237251"), //"فروش گرم"
                PersonnelDailyActivityEventTypeId = Guid.Parse("E780728B-9BAC-4E86-AFE0-9886AD101128"),//"سفارش",
                Latitude = 38.084812,
                Longitude = 46.295516,
                ActivityDate = DateTime.Now,
                ActivityPDate= "1395/02/29",
                eventData = eventdata
            };

            var req = new PersonelTrackingRequestModel
            {
                orderEvent = new List<OrderActivityEventPointViewModel> {eventpoint}
            };

            Call(client, servserURI + "api/dsd/tracking/svprsact", req);

        }
        public static void TestSavePersonelActivitieLackOfOrder(HttpClient client, string servserURI)
        {
            var eventdata = new LackeOfOrderActivityEventViewModel()
            {
                Address = "تبريز -خيابان دارايي 2 اول ميدان صاحب الامر ",
                CustomerCode = "4110001",
                CustomerName = "عليرضا تاملي",
                Time = "9:40",
                Description = "عدم رضایت از محصولات"

            };

            var eventpoint = new LackOfOrderActivityEventPointViewModel()
            {
                UniqueId = Guid.NewGuid(),
                CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"), //"بهزاد احمديان هزه بران",
                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),//"عليرضا تاملي",
                PersonnelDailyActivityVisitTypeId = Guid.Parse("AF24B66E-069C-4EB6-926D-852664237251"),//"فروش گرم",
                PersonnelDailyActivityEventTypeId = Guid.Parse("E8ED4D92-CBC0-40F1-A732-53CFF4C91AC5"),// "عدم سفارش",
                Latitude = 38.084812,
                Longitude = 46.295516,
                ActivityDate = DateTime.Now,
                ActivityPDate = "1395/02/29",
                eventData = eventdata
            };

            var req = new PersonelTrackingRequestModel
            {
                lackOfOrderEvent = new List<LackOfOrderActivityEventPointViewModel> { eventpoint }
            };

            Call(client, servserURI + "api/dsd/tracking/svprsact", req);

        }
        public static void TestSavePersonelActivitieLackOfVisit(HttpClient client, string servserURI)
        {
            var eventdata = new LackeOfVisitActivityEventViewModel()
            {
                Address = "تبريز -خيابان دارايي 2 اول ميدان صاحب الامر ",
                CustomerCode = "4110001",
                CustomerName = "عليرضا تاملي",
                Time = "9:40",
                Description = "بسته بودن مغازه"

            };

            var eventpoint = new LackOfVisitActivityEventPointViewModel()
            {
                UniqueId = Guid.NewGuid(),

                CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"),// "بهزاد احمديان هزه بران",

                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),// "عليرضا تاملي",

                PersonnelDailyActivityVisitTypeId = Guid.Parse("AF24B66E-069C-4EB6-926D-852664237251"),// "فروش گرم",
                PersonnelDailyActivityEventTypeId = Guid.Parse("E68F7931-2189-4000-B173-D02719720923"),// "عدم ویزیت",
                Latitude = 38.084912,
                Longitude = 46.290506,
                ActivityDate = DateTime.Now,
                ActivityPDate = "1395/03/22",
                eventData = eventdata
            };

            var req = new PersonelTrackingRequestModel
            {
                lackOfVisitEvent = new List<LackOfVisitActivityEventPointViewModel> { eventpoint }
            };
          
            Call(client, servserURI + "api/dsd/tracking/svprsact", req);

        }
        public static void TestSavePersonelActivitiePoint(HttpClient client, string servserURI)
        {

            var eventpoint = new PersonnelDailyActivityPointViewModel()
            {
                UniqueId = Guid.NewGuid(),

                //CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"),
                CompanyPersonnelId = Guid.Parse("c99ae71a-a82d-4879-a859-82079ecb69c7"), // ایمانی
                //Latitude = 38.075812,
                //Longitude = 46.285022,
                Latitude = 38.041951,
                Longitude = 46.104700,
                ActivityDate = DateTime.Now,
                ActivityPDate = "1395/02/29",
            };

            var req = new PersonelTrackingRequestModel
            {
                pointEvent = new List<PersonnelDailyActivityPointViewModel> { eventpoint }
            };

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
