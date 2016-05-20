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

                CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"),
                CompanyPersonnelName = "بهزاد احمديان هزه بران",
                
                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),
                CustomerName = "عليرضا تاملي",
                
                PersonnelDailyActivityVisitTypeId = Guid.Parse("AF24B66E-069C-4EB6-926D-852664237251"),
                PersonnelDailyActivityVisitTypeName  = "سفارش",
                PersonnelDailyActivityEventTypeId = Guid.Parse("E780728B-9BAC-4E86-AFE0-9886AD101128"),
                PersonnelDailyActivityEventTypeName  = "سفارش",
                Latitude = 38.084812,
                Longitude = 46.295516,
                ShortDescription = "test",
                ActivityDate = DateTime.Now,
                ActivityPDate= "1395/02/29",
                LastUpdate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DataOwnerId = Guid.Parse(DataOwnerKey),
                ApplicationOwnerId = Guid.Parse(OwnerKey),
                DataOwnerCenterId = Guid.Parse(DataOwnerCenterKey),
                eventData = eventdata
            };

            var req = new PersonelTrackingRequestModel();
            req.orderEvent = eventpoint;

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

                CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"),
                CompanyPersonnelName = "بهزاد احمديان هزه بران",

                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),
                CustomerName = "عليرضا تاملي",

                PersonnelDailyActivityVisitTypeId = Guid.Parse("AF24B66E-069C-4EB6-926D-852664237251"),
                PersonnelDailyActivityVisitTypeName = "سفارش",
                PersonnelDailyActivityEventTypeId = Guid.Parse("E780728B-9BAC-4E86-AFE0-9886AD101128"),
                PersonnelDailyActivityEventTypeName = "سفارش",
                Latitude = 38.084812,
                Longitude = 46.295516,
                ShortDescription = "test",
                ActivityDate = DateTime.Now,
                ActivityPDate = "1395/02/29",
                LastUpdate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DataOwnerId = Guid.Parse(DataOwnerKey),
                ApplicationOwnerId = Guid.Parse(OwnerKey),
                DataOwnerCenterId = Guid.Parse(DataOwnerCenterKey),
                eventData = eventdata
            };

            var req = new PersonelTrackingRequestModel();
            req.lackOfOrderEvent = eventpoint;

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

                CompanyPersonnelId = Guid.Parse("ABDBCF7B-0540-46C8-9CA3-0BB427C9FE59"),
                CompanyPersonnelName = "بهزاد احمديان هزه بران",

                CustomerId = Guid.Parse("BC145F27-2714-4610-ACBF-04A0679894AE"),
                CustomerName = "عليرضا تاملي",

                PersonnelDailyActivityVisitTypeId = Guid.Parse("AF24B66E-069C-4EB6-926D-852664237251"),
                PersonnelDailyActivityVisitTypeName = "داخل مسیر",
                PersonnelDailyActivityEventTypeId = Guid.Parse("E68F7931-2189-4000-B173-D02719720923"),
                PersonnelDailyActivityEventTypeName = "عدم ویزیت",
                Latitude = 38.084812,
                Longitude = 46.295516,
                ShortDescription = "test",
                ActivityDate = DateTime.Now,
                ActivityPDate = "1395/02/31",
                LastUpdate = DateTime.Now,
                CreatedDate = DateTime.Now,
                DataOwnerId = Guid.Parse(DataOwnerKey),
                ApplicationOwnerId = Guid.Parse(OwnerKey),
                DataOwnerCenterId = Guid.Parse(DataOwnerCenterKey),
                eventData = eventdata
            };

            var req = new PersonelTrackingRequestModel();
            req.lackOfVisitEvent = eventpoint;

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
