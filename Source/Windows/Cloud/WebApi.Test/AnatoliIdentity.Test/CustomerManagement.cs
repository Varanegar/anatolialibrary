﻿using Anatoli.ViewModels.CustomerModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClientApp
{
    public static class CustomerManagement
    {
        public static CustomerViewModel GetCustomerFromServer(HttpClient client, string servserURI)
        {
            //F125EDC7-473D-4C59-B966-3EF9E6E6A7D9
            var result8 = client.GetAsync(servserURI + "/api/gateway/customer/customers?id=308E176C-74E0-4916-95F5-FFDF4E35AE22&privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj = new CustomerViewModel();
            var x = JsonConvert.DeserializeAnonymousType(json8, obj);
            return x;
        }

        public static void UpdateCustomerFromServer(HttpClient client, string servserURI)
        {
            //try
            //{
            //    log.Info("Start CallServerService URI ");
            //    var currentTime = DateTime.Now;
            //    var lastUpload = Utility.GetLastUploadTime(CustomerDataType);
            //    var dbData = CustomerAdapter.Instance.GetNewUsers(DateTime.MinValue);
            //    if (dbData != null)
            //    {
            //        dbData.ForEach(item =>
            //        {
            //            try
            //            {
            //                item.SendPassSMS = false;
            //                string data = JsonConvert.SerializeObject(item);
            //                string getUserURI = serverURI + UriInfo.GetUserURI + "/" + item.Mobile;
            //                var user = ConnectionHelper.CallServerServicePost<UserReturnModel>(data, getUserURI, client, privateOwnerId, dataOwner, dataOwnerCenter);
            //                if (user != null)
            //                    CustomerAdapter.Instance.SetCustomerSiteUserId(user.Id, user.Mobile);
            //                else
            //                {
            //                    string URI = serverURI + UriInfo.SaveUserURI;
            //                    UserReturnModel result = ConnectionHelper.CallServerServicePost<UserReturnModel>(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
            //                    CustomerAdapter.Instance.SetCustomerSiteUserId(result.Id, result.Mobile);
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                log.Error("Failed CallServerService ", ex);
            //            }

            //        });
            //    }
            //    Utility.SetLastUploadTime(CustomerDataType, currentTime);

            //    log.Info("Completed CallServerService ");
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Failed CallServerService ", ex);
            //}

            var obj = GetCustomerFromServer(client, servserURI);

            //obj.Baskets.RemoveAt(1);
            string data = new JavaScriptSerializer().Serialize(obj);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/customer/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }
    }
}
