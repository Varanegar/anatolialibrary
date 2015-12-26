using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thunderstruck;

namespace ClientApp
{
    public class CharGroupManagement
    {
        public static void SaveCharGroupInfoToServer(HttpClient client, string servserURI)
        {
            var dataList = GetCharGroupInfo();

            var result = client.GetAsync(servserURI + "/api/gateway/product/chargroups").Result;
            var json = result.Content.ReadAsStringAsync().Result;

            string jsonData = new JavaScriptSerializer().Serialize(dataList);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/product/chargroups/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
        }

        public static List<CharGroupViewModel> GetCharGroupInfo()
        {
            List<CharGroupViewModel> charGroups = new List<CharGroupViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CharGroupViewModel>(@"SELECT ProductSpecificityGId as Id, ProductSpecificityGroupName as CharGroupName, Convert(uniqueidentifier, UniqueId)  as UniqueId
                        FROM ProductSpecificityG");
                data.ToList().ForEach(item =>
                {
                    var detail = context.All<CharTypeViewModel>(@"select Convert(uniqueidentifier,UniqueId) as UniqueId from ProductSpecificityGDetail g,  ProductSpecificity p 
                                    where p.ProductSpecificityId = g.ProductSpecificityId and ProductSpecificityGId=" + item.ID
                            );
                    item.CharTypes = detail.ToList();
                });
                charGroups = data.ToList();
            }
            return charGroups;
        }

        public static void SaveCharTypeInfoToServer(HttpClient client, string servserURI)
        {
            var dataList = GetCharTypeInfo();


            string jsonData = new JavaScriptSerializer().Serialize(dataList);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/product/chartypes/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;

        }
        public static void ReadCharTypeInfoFromServer(HttpClient client, string servserURI)
        {
            var result = client.GetAsync(servserURI + "/api/gateway/product/chargroups").Result;
            var json = result.Content.ReadAsStringAsync().Result;
        }

        public static List<CharTypeViewModel> GetCharTypeInfo()
        {
            List<CharTypeViewModel> charTypes = new List<CharTypeViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<CharTypeViewModel>(@"SELECT ProductSpecificityId as Id, ProductSpecificityName as CharTypeDesc, Convert(uniqueidentifier, UniqueId)  as UniqueId
                        FROM            ProductSpecificity");
                data.ToList().ForEach(item =>
                    {
                        var detail = context.All<CharValueViewModel>(@"SELECT ProductSpecificityValueName as CharValueText, Convert(uniqueidentifier,UniqueId) as UniqueId
                                FROM ProductSpecificityValue where ProductSpecificityId=" + item.ID
                                );
                        item.CharValues = detail.ToList();
                    });
                charTypes = data.ToList();
            }
            return charTypes;
        }
    }
}
