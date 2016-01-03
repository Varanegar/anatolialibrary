using Anatoli.ViewModels.BaseModels;
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
    public class BaseDataManagement
    {
        public static void SaveBaseTypeInfoToServer(HttpClient client, string servserURI)
        {
            var dataList = GetBaseTypeInfo();

            string jsonData = new JavaScriptSerializer().Serialize(dataList);
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/basedata/basedatas/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
        }
        public static List<BaseTypeViewModel> GetBaseTypeInfo()
        {
            List<BaseTypeViewModel> baseTypes = new List<BaseTypeViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<BaseTypeViewModel>(@"select 1 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as uniqueid, 'OrderSource' as BaseTypeDesc
                    union all 
                    select 2 as id, convert(uniqueidentifier, 'F5FFAD55-6E39-40BD-A95D-12A34BA4D005') as uniqueid, 'DeliveryType' as BaseTypeDesc
                    union all 
                    select 3 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as uniqueid, 'BasketType' as BaseTypeDesc
                    union all 
                    select 5 as id, convert(uniqueidentifier, 'CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7') as uniqueid, 'ProductType' as BaseTypeDesc
                    union all 
                    select 6 as id, convert(uniqueidentifier, 'F17B8898-D39F-4955-9757-A6B31767F5C7') as uniqueid, 'PayType' as BaseTypeDesc
                    ");
                data.ToList().ForEach(item =>
                {
                    var detail = context.All<BaseValueViewModel>(@"select * from (select paytypename as BaseValueName, uniqueid, PayTypeId as id, convert(uniqueidentifier, 'F17B8898-D39F-4955-9757-A6B31767F5C7') as BaseTypeId from PayType
                            union all
                            select producttypename as BaseValueName, uniqueid, ProductTypeId as id, convert(uniqueidentifier, 'CF0016A1-E832-4EA4-9DE8-3E4C32C1EDA7') as BaseTypeId from ProductType
                            union all
                            select 'Checkout' as BaseValueName, convert(uniqueidentifier, 'F6CE03E2-8A2A-4996-8739-DA9C21EAD787') as uniqueid, 1 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Checkout
                            union all
                            select 'Favorite' as BaseValueName, convert(uniqueidentifier, 'AE5DE00D-3391-49FE-985B-9DA7045CDB13') as uniqueid, 2 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Favorite
                            union all
                            select 'سفارش ناقص' as BaseValueName, convert(uniqueidentifier, '194CA845-2E34-4A06-9A89-DCAFF956FE4D') as uniqueid, 3 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Incomplete
                            union all
                            select 'سایر' as BaseValueName, convert(uniqueidentifier, '41581E50-9928-4A3C-A513-A32DBB3B3D0D') as uniqueid, 4 as id, convert(uniqueidentifier, 'CA2BE6B3-7187-4D81-9198-B9433E726C9C') as BaseTypeId  --Others
                            union all
                            select 'دریافت از محل' as BaseValueName, convert(uniqueidentifier, 'BE2919AB-5564-447A-BE49-65A81E6AF712') as uniqueid, 1 as id, convert(uniqueidentifier, 'F5FFAD55-6E39-40BD-A95D-12A34BA4D005') as BaseTypeId  --DeliveryType : Pickup
                            union all
                            select 'تحویل درب منزل' as BaseValueName, convert(uniqueidentifier, 'CE4AEE25-F8A7-404F-8DBA-80340F7339CC') as uniqueid, 2 as id, convert(uniqueidentifier, 'F5FFAD55-6E39-40BD-A95D-12A34BA4D005') as BaseTypeId  --DeliveryType : Delivery
                            union all
                            select 'سایت' as BaseValueName, convert(uniqueidentifier, '0410F5BD-0C01-4E32-A4D9-D2F4DCC46003') as uniqueid, 2 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as BaseTypeId  --OrderSource : Site
                            union all
                            select 'App' as BaseValueName, convert(uniqueidentifier, '65DEC223-059E-48BA-8281-E4FAAFF6E32D') as uniqueid, 2 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as BaseTypeId  --OrderSource : App
                            union all
                            select 'سایر' as BaseValueName,  convert(uniqueidentifier, '6CF27F09-E162-4802-A451-9BC3304A8130') as uniqueid, 2 as id, convert(uniqueidentifier, 'A0EA0430-6E42-4E5F-B809-E05E13D73E54') as BaseTypeId  --OrderSource : Others
                             ) as tt where BaseTypeId='" + item.UniqueId + "'"
                            );
                    item.BaseValues = detail.ToList();
                });
                baseTypes = data.ToList();
            }
            return baseTypes;
        }
    }
}
