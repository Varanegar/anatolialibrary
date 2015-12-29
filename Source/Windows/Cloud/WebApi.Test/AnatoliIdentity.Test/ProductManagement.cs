using Anatoli.ViewModels.ProductModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
                    select p.ProductGroupTreeId, p.ParentId, p.Title, p.uniqueid, p2.uniqueid as Parentuniqueid, g.UniqueId  as CharGroupId
		                     into #GroupRec
		                     from ProductGroupTree AS p INNER JOIN
                                             ProductGroupTree AS p2 ON p.ParentId = p2.ProductGroupTreeId LEFT OUTER JOIN
                                             ProductSpecificityG AS g ON p.ProductSpecificityGID = g.ProductSpecificityGId
		                     where p.parentid = p2.ProductGroupTreeId");
		        var data = context.All<ProductGroupViewModel>(@"
                    WITH ProductGroupTreeLevels AS
                    (
                        SELECT
                            p.ProductGroupTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.uniqueid,
		                    convert(varchar(66),null) as Parentuniqueid,
		                    g.uniqueId as CharGroupId,
                            CONVERT(VARCHAR(MAX), p.ProductGroupTreeId) AS thePath,
                            1 AS Level
	                    FROM            ProductGroupTree AS p LEFT OUTER JOIN
                                             ProductSpecificityG AS g ON p.ProductSpecificityGID = g.ProductSpecificityGId
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

            string data = new JavaScriptSerializer().Serialize(manufacture);
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
                var data = context.All<ProductViewModel>(@"SELECT        p.ProductId AS id, CONVERT(uniqueidentifier, p.UniqueId) AS uniqueid, p.ProductCode, p.ProductName, p.StoreProductName, p.PackVolume, p.PackWeight, 
                    p.Description, null  as PackUnitId, CASE ProductTypeId WHEN 1 THEN CONVERT(uniqueidentifier, '21B7F88F-42B2-40F6-83C9-EF20943440B9') 
                    WHEN 2 THEN CONVERT(uniqueidentifier, '594120A7-1312-45B2-883B-605000D33D0F') WHEN 3 THEN CONVERT(uniqueidentifier, 
                    '9DA7C343-CE14-4CBB-81AE-709E075D4E10') END AS ProductTypeId, pg.UniqueId as ProductGroupIdString, m.UniqueId as ManufactureIdString
                    FROM            Product AS p LEFT OUTER JOIN
                    Manufacturer AS m ON p.ManufacturerId = m.ManufacturerId LEFT OUTER JOIN
                    ProductGroup AS pg ON p.ProductGroupId = pg.ProductGroupId");
                data.ToList().ForEach(item =>
                    {

                    });
                manufacture = data.ToList();
                
            }
            return manufacture;
        }
        #endregion
    }
}
