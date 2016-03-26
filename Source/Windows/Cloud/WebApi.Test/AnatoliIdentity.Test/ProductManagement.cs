using Anatoli.ViewModels.ProductModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thunderstruck;

namespace ClientApp
{
    public static class ProductManagement
    {
        #region Product Group
        public static void UploadProductGroupToServer(HttpClient client, string servserURI)
        {
            var storeInfo = GetProductGroupInfo();

            string data = new JavaScriptSerializer().Serialize(storeInfo);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/product/productgroups/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
        public static List<ProductGroupViewModel> GetProductGroupInfo()
        {
            List<ProductGroupViewModel> productGroup = new List<ProductGroupViewModel>();
            using (var context = new DataContext())
            {
                context.Execute(@"IF OBJECT_ID('tempdb..#GroupRec') IS NOT NULL drop table  #GroupRec
                    select p.ProductGroupTreeSiteId as ProductGroupTreeId, p.ParentId, p.Title, p.uniqueid, p2.uniqueid as Parentuniqueid, null  as CharGroupId
		                     into #GroupRec
		                     from ProductGroupTreeSite AS p INNER JOIN
                                             ProductGroupTreeSite AS p2 ON p.ParentId = p2.ProductGroupTreeSiteId 
		                     where p.parentid = p2.ProductGroupTreeSiteId");
		        var data = context.All<ProductGroupViewModel>(@"
                    WITH ProductGroupTreeLevels AS
                    (
                        SELECT
                            p.ProductGroupTreeSiteId as ProductGroupTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.uniqueid,
		                    convert(varchar(66),null) as Parentuniqueid,
		                    null as CharGroupId,
                            CONVERT(VARCHAR(MAX), p.ProductGroupTreeSiteId) AS thePath,
                            1 AS Level
	                    FROM            ProductGroupTreeSite AS p 
                        WHERE p.ParentId IS NULL 

                        UNION ALL

                        SELECT
                            e.ProductGroupTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.uniqueid,
		                    e.Parentuniqueid,
		                    e.CharGroupId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.ProductGroupTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM ProductGroupTreeLevels x
                        JOIN #GroupRec e on e.ParentId = x.ProductGroupTreeId
                    ),
                    ProductGroupTreeRows AS
                    (
                        SELECT
                             ProductGroupTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM ProductGroupTreeLevels
                    )
                    SELECT
	                    Er.UniqueId as UniqueIdString,
	                    Er.ParentUniqueId as ParentUniqueIdString,
                         ER.ProductGroupTreeId as ID,
                         ER.ParentId as ParentId,
	                     ER.Title as GroupName,
	                     ER.CharGroupId as CharGroupIdString,
                         --ER.thePath,
                         ER.Level as NLevel,
                         ER.Row,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM ProductGroupTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM ProductGroupTreeRows ER
                    ORDER BY thePath");
                productGroup = data.ToList();
            }
            return productGroup;
        }
        #endregion

        #region Manufacture
        public static void UploadManufactureToServer(HttpClient client, string servserURI)
        {
            var manufacture = GetManufactureInfo();

            string data = new JavaScriptSerializer().Serialize(manufacture);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/base/manufacture/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
        public static List<ManufactureViewModel> GetManufactureInfo()
        {
            List<ManufactureViewModel> manufacture = new List<ManufactureViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<ManufactureViewModel>("select ManufacturerId as Id, convert(uniqueidentifier, uniqueid) as uniqueid, ManufacturerName as ManufactureName from Manufacturer");
                manufacture = data.ToList();
            }
            return manufacture;
        }
        #endregion

        #region Supplier
        public static void UploadSupplierToServer(HttpClient client, string servserURI)
        {
            var manufacture = GetSupplierInfo();

            string data = new JavaScriptSerializer().Serialize(manufacture);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/base/supplier/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
        public static List<SupplierViewModel> GetSupplierInfo()
        {
            List<SupplierViewModel> manufacture = new List<SupplierViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<SupplierViewModel>("select SupplierId as Id, convert(uniqueidentifier, uniqueid) as uniqueid, SupplierName from Supplier");
                manufacture = data.ToList();
            }
            return manufacture;
        }
        #endregion

        #region Product
        public static void UploadProductToServer(HttpClient client, string servserURI)
        {
            var manufacture = GetProductInfo();
            var js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            string data = js.Serialize(manufacture);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/product/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
        public static List<ProductViewModel> GetProductInfo()
        {
            List<ProductViewModel> manufacture = new List<ProductViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<ProductViewModel>(@"SELECT p.ProductId AS id, CONVERT(uniqueidentifier, p.UniqueId) AS uniqueid, p.ProductCode, p.ProductName, p.StoreProductName, p.PackVolume, p.PackWeight, 
                    p.Description, null  as PackUnitId, CASE ProductTypeId WHEN 1 THEN CONVERT(uniqueidentifier, '21B7F88F-42B2-40F6-83C9-EF20943440B9') 
                    WHEN 2 THEN CONVERT(uniqueidentifier, '594120A7-1312-45B2-883B-605000D33D0F') WHEN 3 THEN CONVERT(uniqueidentifier, 
                    '9DA7C343-CE14-4CBB-81AE-709E075D4E10') END AS ProductTypeId, pg.UniqueId as ProductGroupIdString, m.UniqueId as ManufactureIdString
                    FROM            Product AS p LEFT OUTER JOIN
                    Manufacturer AS m ON p.ManufacturerId = m.ManufacturerId LEFT OUTER JOIN
                    ProductGroupTreeSite AS pg ON p.ProductGroupTreeSiteId = pg.ProductGroupTreeSiteId
                    ");
                data.ToList().ForEach(item =>
                    {
                        var productSuppllier = context.All<SupplierViewModel>(@"select CONVERT(uniqueidentifier, s.uniqueId) as uniqueId from SupplierProduct as sp, supplier as s, product as p 
	                        where sp.SupplierId = s.SupplierId and p.ProductId = sp.ProductId and sp.ProductId='" + item.ID + "'");
                        item.Suppliers = productSuppllier.ToList();
                        var productValue = context.All<CharValueViewModel>(@"select CONVERT(uniqueidentifier, pv.uniqueId) as uniqueId from ProductSpecificRel as ps , product as p, ProductSpecificityValue as pv
	                            where ps.ProductSpecificityValueId = pv.ProductSpecificityValueId and p.productId = ps.ProductID and p.productId ='" + item.ID + "'");
                        item.CharValues = productValue.ToList();
                    });
                manufacture = data.ToList();
                
            }
            return manufacture;
        }
        #endregion

        internal static List<ProductGroupViewModel> DownloadProductGroupFromServer(HttpClient client, string servserURI)
        {
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");

            var result8 = client.PostAsync(servserURI + "/api/gateway/product/productgroups",content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<ProductGroupViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }
        internal static List<ProductViewModel> DownloadProductFromServer(HttpClient client, string servserURI)
        {
            var result8 = client.GetAsync(servserURI + "/api/gateway/product/products?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<ProductViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }
        internal static List<ProductViewModel> DownloadSimpleProductFromServer(HttpClient client, string servserURI)
        {
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            content.Headers.Add("OwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
            content.Headers.Add("DataOwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
            content.Headers.Add("DataOwnerCenterKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
            var result8 = client.PostAsync(servserURI + "/api/gateway/product/products/v2", content).Result;
            if (result8.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json8 = result8.Content.ReadAsStreamAsync().Result;
                var responseStream = new GZipStream(json8, CompressionMode.Decompress);
                StreamReader Reader = new StreamReader(responseStream, Encoding.UTF8);
                var tempResult = Reader.ReadToEnd();
                var obj = new List<ProductViewModel>();
                var x = JsonConvert.DeserializeAnonymousType(tempResult, obj);
                return x;
            }
            else
            {
                var json8 = result8.Content.ReadAsStringAsync().Result;
                var obj = new List<ProductViewModel>();
                var x = JsonConvert.DeserializeAnonymousType(json8, obj);
                return x;

            }
        }
        internal static List<ProductViewModel> DownloadSupplierFromServer(HttpClient client, string servserURI)
        {
            var result8 = client.GetAsync(servserURI + "/api/gateway/base/manufacture/manufactures?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<ProductViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        internal static List<ProductViewModel> DownloadProductRateFromServer(HttpClient client, string servserURI)
        {
            var result8 = client.GetAsync(servserURI + "/api/gateway/productrate/productrateavgs/after?dateAfter=2012-01-01&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<ProductViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }
    }
}
