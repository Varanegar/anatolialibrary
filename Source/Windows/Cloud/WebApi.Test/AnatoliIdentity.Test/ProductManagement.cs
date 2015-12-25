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
        public static void UploadProductGroupToServer(HttpClient client, string servserURI)
        {
            var storeInfo = GetProductGroupInfo();

            //obj.Baskets.RemoveAt(1);
            string data = new JavaScriptSerializer().Serialize(storeInfo);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/customer/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
        public static List<ProductGroupViewModel> GetProductGroupInfo()
        {
            List<ProductGroupViewModel> productGroup = new List<ProductGroupViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<ProductGroupViewModel>(@"IF OBJECT_ID('tempdb..#GroupRec') IS NOT NULL drop table  #GroupRec
                    select p.ProductGroupTreeId, p.ParentId, p.Title, p.uniqueid, p2.uniqueid as Parentuniqueid, g.UniqueId  as CharGroupId
		                     into #GroupRec
		                     from ProductGroupTree AS p INNER JOIN
                                             ProductGroupTree AS p2 ON p.ParentId = p2.ProductGroupTreeId LEFT OUTER JOIN
                                             ProductSpecificityG AS g ON p.ProductSpecificityGID = g.ProductSpecificityGId
		                     where p.parentid = p2.ProductGroupTreeId
		                     go
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
    }
}
