using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Anatoli.ViewModels;
using Anatoli.ViewModels.RequestModel;
using Anatoli.ViewModels.VnGisModels;
using Newtonsoft.Json;

namespace ClientApp
{
    public class VnGisManagement : BaseTestManagment
    {
      

        #region Area
        public static void TestLoadRegionAreas(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaParentId = null;
              
            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldarealst", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
           // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestLoadRegionAreasPoints(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("eb9fcb88-6105-e611-825f-5c93a2e09b3c");
              
            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldareapoint", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
           // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestHasAreasPoints(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("e09fcb88-6105-e611-825f-5c93a2e09b3c");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/hsareapoint", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestGetRegionAreaPath(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("3ee50fef-6805-e611-825f-5c93a2e09b3c");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/getareapath", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestGetRegionAreaSelectedCustomer(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("32F2A6E2-6805-E611-825F-5C93A2E09B3C");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldselectedcust", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestGetRegionAreaNotSelectedCustomer(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("32F2A6E2-6805-E611-825F-5C93A2E09B3C");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldntselectedcust", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }
        public static void SaveRegionAreaPoint(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("33F2A6E2-6805-E611-825F-5C93A2E09B3C");

            var points = new List<RegionAreaPointViewModel>
            {
                new RegionAreaPointViewModel()
                {
                    Lng = 46.944150,
                    Lat = 38.131865,
                    Pr = 1,
                    CstId = null
                },
                new RegionAreaPointViewModel()
                {
                    Lng = 46.948150,
                    Lat = 38.132865,
                    Pr = 2,
                    CstId = Guid.Parse("DD0BE467-42A6-43D9-BFE1-6067956EEC82")
                },
                new RegionAreaPointViewModel()
                {
                    Lng = 46.955150,
                    Lat = 38.135865,
                    Pr = 3,
                    CstId = null
                },

            };
            req.regionAreaPointDataList = points;

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/svareapnt", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestLoadRigenAreaParentPoints(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("32f2a6e2-6805-e611-825f-5c93a2e09b3c");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldareaprntpnt", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }
        public static void TestLoadAreaSibilingPoints(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("32f2a6e2-6805-e611-825f-5c93a2e09b3c");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldareasblpnt", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestLoadAreaChildPoints(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("1AC64FE1-6105-E611-825F-5C93A2E09B3C");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldareachldpnt", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestRemoveAreaPointsByAreaId(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("33F2A6E2-6805-E611-825F-5C93A2E09B3C");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/rmvareapntareaid", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestAddCustomerToRegionArea(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("33F2A6E2-6805-E611-825F-5C93A2E09B3C");
            req.customerId = Guid.Parse("D43DB060-13EF-4BCE-8CF0-CB2998FBEB0E");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/adcusttoarea", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }


        public static void TestRemoveCustomerFromRegionArea(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaId = Guid.Parse("33F2A6E2-6805-E611-825F-5C93A2E09B3C");
            req.customerId = Guid.Parse("D43DB060-13EF-4BCE-8CF0-CB2998FBEB0E");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/rmvcustfrmarea", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestChangeCustomerPosition(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            var list = new List<CustomerPointViewModel>();
            list.Add(new CustomerPointViewModel()
            {
                UniqueId = Guid.Parse("DD0BE467-42A6-43D9-BFE1-6067956EEC82"),
                Lng = 46.96815,
                Lat = 38.132469
            
            });
            list.Add(new CustomerPointViewModel()
            {
                UniqueId = Guid.Parse("0CF20312-15DD-47B5-AC70-7C4A9F5FBCF6"),
                Lng = 46.99815,
                Lat = 38.402069

            });
            req.customerPointDataList = list;

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/chgcustpos", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        public static void TestLoadRegionAreaByLevel(HttpClient client, string servserURI)
        {
            var req = new RegionAreaRequestModel();

            req.regionAreaLevel = 0;

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/route/ldareabylv", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }
        #endregion

        #region customer
        public static void TestLoadCustomerBySearchTerm(HttpClient client, string servserURI)
        {
            var req = new CustomerRequestModel();

            req.searchTerm = "جلالی";

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/customer/ldsrch", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }

        #endregion

        #region personnel
        public static void TestLoadPersonByGroup(HttpClient client, string servserURI)
        {
            var req = new PersonelRequestModel();

            req.groupId = Guid.Parse("1AC6bFE1-6105-E611-825F-5C93A2E0943C"); 

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/personnel/ldperbygrp", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);

        }
        public static void TestLoadGroupGroupByArea(HttpClient client, string servserURI)
        {
            var req = new PersonelRequestModel();

            req.regionAreaId = Guid.Parse("1AC6bFE1-6105-E611-825F-5C93A2E0943C");

            string data = new JavaScriptSerializer().Serialize(req);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "api/dsd/personnel/ldgrpbyarea", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            // var obj = new { message = "", ModelState = new Dictionary<string, string[]>() };
            //var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            WriteToConsole(json8);
        }

        
        #endregion
    }
}
