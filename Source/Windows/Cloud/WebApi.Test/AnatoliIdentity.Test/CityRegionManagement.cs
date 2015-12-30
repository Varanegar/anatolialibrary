using Anatoli.ViewModels.BaseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thunderstruck;

namespace ClientApp
{
    public static class CityRegionManagement
    {
        public static List<CityRegionViewModel> GetCityRegionFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            var result8 = client.GetAsync(servserURI + "/api/gateway/base/region/cityregions?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new List<CityRegionViewModel>();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        public static void UpdateCityRegionFromServer(HttpClient client, string servserURI)
        {
            var obj = GetCityRegionInfo();

            //obj.Baskets.RemoveAt(1);
            string data = new JavaScriptSerializer().Serialize(obj);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/base/region/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }

        public static List<CityRegionViewModel> GetCityRegionInfo()
        {
            List<CityRegionViewModel> productGroup = new List<CityRegionViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CityRegionViewModel>(@"WITH DeliveryRegionTreeLevels AS
                    (
                        SELECT
                            p.DeliveryRegionTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.UniqueId,
		                    CONVERT(varchar(66),'') as ParentUniqueId,
                            CONVERT(VARCHAR(MAX), p.DeliveryRegionTreeId) AS thePath,
                            1 AS Level
                        FROM DeliveryRegionTree p--, DeliveryRegionTree p2 
                        WHERE p.ParentId IS NULL --and p.ParentId = p2.DeliveryRegionTreeId

                        UNION ALL

                        SELECT
                            e.DeliveryRegionTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.UniqueId,
		                    e.ParentUniqueId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.DeliveryRegionTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM DeliveryRegionTreeLevels x
                        JOIN (select p.DeliveryRegionTreeId, p.ParentId, p.Title, p.UniqueId, p2.UniqueId as ParentUniqueId from  DeliveryRegionTree as p , DeliveryRegionTree as p2
			                    where p.ParentId = p2.DeliveryRegionTreeId)  e on e.ParentId = x.DeliveryRegionTreeId
                    ),
                    DeliveryRegionTreeRows AS
                    (
                        SELECT
                             DeliveryRegionTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM DeliveryRegionTreeLevels
                    )
                    SELECT 
                         ER.DeliveryRegionTreeId,
                         ER.ParentId,
	                     ER.UniqueId as uniqueIdString,
	                     ER.parentUniqueId as ParentUniqueIdString,
	                     ER.Title as GroupName,
                         --ER.thePath,
                         ER.Level as NLevel,
                         --ER.Row,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM DeliveryRegionTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM DeliveryRegionTreeRows ER
                    ");
                productGroup = data.ToList();
            }
            return productGroup;
        }

    }
}
