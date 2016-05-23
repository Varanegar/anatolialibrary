using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using App.Authorise;
//using App.Areas.User.Models;

namespace App.Authorise
{
    public class CrowdSSO
    {

        /// <summary>
        /// Instatiate the single sign on service
        /// </summary>
        /// <param name="crowdUrl">Url of the Crowd Server instance</param>
        /// <param name="appName">The app name in Crowd</param>
        /// <param name="appPassword">The password for the app in Crowd</param>
        public CrowdSSO(string crowdUrl, string appName, string appPassword)
        {
            crowdLocation = crowdUrl;
            applicationName = appName;
            applicationPassword = appPassword;
        }

        #region Global Variables

        private string applicationName = string.Empty;
        private string applicationPassword = string.Empty;
        private string crowdLocation = string.Empty;

        private const string displayName = "display-name";
        private const string email = "email";
        private const string firstName = "first-name";
        private const string lastName = "last-name";

        #endregion

        /// <summary>
        /// Authenticate against Crowd
        /// </summary>
        /// <param name="username">The username of the user that is set in Crowd</param>
        /// <param name="password">The password of the user that is set in Crowd</param>
        /// <returns>Returns TRUE if the user has provided correct credentials. Otherwise False.</returns>
        /// <remarks>Throws WebException if authentication fails</remarks>
        public HttpStatusCode Authenticate(string username, string password)
        {

            CrowdSSORequest SSORequest = new CrowdSSORequest
            {
                apiCall = CrowdSSOAPICall.Authenticate,
                group = "",
                username = username,
                password = password,
                method = "POST",
            };

            try
            {
                var result = getJSON(SSORequest);

                if (result.GetType() != typeof(Exception))
                {
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.NotFound;
                }

            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return 
                return HttpStatusCode.InternalServerError;
            }

        }

        /// <summary>
        /// Change user's password in Crowd
        /// </summary>
        /// <param name="username">The username of the user that is set in Crowd</param>
        /// <param name="password">The new password for the user</param>
        /// <returns>Returns TRUE if the user has provided correct credentials. Otherwise False.</returns>
        /// <remarks>Throws WebException if authentication fails</remarks>
        public HttpStatusCode ChangePassword(string username, string password)
        {
            CrowdSSORequest SSORequest = new CrowdSSORequest
            {
                apiCall = CrowdSSOAPICall.ChangePassword,
                group = "",
                username = username,
                password = password,
                method = "PUT",
            };

            try
            {
                var result = (HttpWebResponse)getJSON(SSORequest);
                return result.StatusCode;
            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return 
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// Request an e-mail link to reset the user password
        /// </summary>
        /// <param name="username">The username of the user that is set in Crowd</param>
        /// <returns>Returns TRUE if the user exists and a pasword reset e-mail is sent. Otherwise False.</returns>
        /// <remarks>Throws WebException if authentication fails</remarks>
        public HttpStatusCode RequestPasswordResetEmail(string username)
        {
            CrowdSSORequest SSORequest = new CrowdSSORequest
            {
                apiCall = CrowdSSOAPICall.RequestPasswordReset,
                group = "",
                username = username,
                password = "",
                method = "POST",
            };

            try
            {
                var result = (HttpWebResponse)getJSON(SSORequest);
                return result.StatusCode;
            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return 
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// Get user details from Crowd
        /// </summary>
        /// <param name="username">The username of the user that is set in Crowd</param>
        /// <returns>Returns UserDetail object</returns>
        /// <remarks>Throws WebException if authentication fails</remarks>
        public UserDetail UserDetail(string username)
        {

            UserDetail details = new UserDetail();

            CrowdSSORequest SSORequest = new CrowdSSORequest
            {
                apiCall = CrowdSSOAPICall.UserDetail,
                group = "",
                username = username,
                password = "",
                method = "GET",
            };

            try
            {
                var result = getJSON(SSORequest);
                List<CrowdSSOAttribute> attributes = new List<CrowdSSOAttribute>();                

                foreach (var prop in result)
                {
                    attributes.Add(new CrowdSSOAttribute { Name = prop.Key, Values = prop.Value.ToString() });
                }

                details.FirstName = attributes[7].Values.ToString();
                details.LastName = attributes[8].Values.ToString();
                details.Username = attributes[10].Values.ToString();
                details.Email = attributes[10].Values.ToString();

                //attributes = UserAttributes(username);

                //details.VectusUsername = attributes[3].Values.ToString();
                //details.VectusUsername = details.VectusUsername.Replace("[", "");
                //details.VectusUsername = details.VectusUsername.Replace("]", "");
                //details.VectusUsername = details.VectusUsername.Replace("\"", "");
                //details.VectusUsername = details.VectusUsername.Trim();

                return details;
            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return 
                return details;
            }
        }

        /// <summary>
        /// Get users in a group from Crowd
        /// </summary>
        /// <param name="username">The username of the user that is set in Crowd</param>
        /// <returns>Returns UserDetail object</returns>
        /// <remarks>Throws WebException if authentication fails</remarks>
        public List<CrowdSSOUser> UsersInGroup(string groupName)
        {
            List<CrowdSSOUser> userList = new List<CrowdSSOUser>();

            CrowdSSORequest SSORequest = new CrowdSSORequest
            {
                apiCall = CrowdSSOAPICall.UsersInGroup,
                group = groupName,
                username = "",
                password = "",
                method = "GET",
            };

            try
            {

                var result = getJSON(SSORequest);

                userList = JsonConvert.DeserializeObject<List<CrowdSSOUser>>(result["users"].ToString());

                return userList;
            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return 
                return userList;
            }
        }

        /// <summary>
        /// Get user attributes from Crowd via Rest API
        /// </summary>
        /// <param name="username">The username of the user that is set in Crowd</param>
        /// <returns>Returns a list of CrowdSSOAttribute object </returns>
        /// <remarks>Throws WebException if authentication fails</remarks>
        public List<CrowdSSOAttribute> UserAttributes(string username)
        {
            List<CrowdSSOAttribute> userAttributes = new List<CrowdSSOAttribute>();

            CrowdSSORequest SSORequest = new CrowdSSORequest
            {
                apiCall = CrowdSSOAPICall.UserAttribute,
                group = "",
                username = username,
                password = "",
                method = "GET",
            };

            try
            {
                var result = getJSON(SSORequest);
                userAttributes = JsonConvert.DeserializeObject<List<CrowdSSOAttribute>>(result["attributes"].ToString());

                return userAttributes;
            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return 
                return userAttributes;
            }

        }

        // Generic code we will use
        #region Private Methods

        private static string Encode(string username, string password)
        {
            string auth = string.Join(":", username, password);
            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth));
        }

        private dynamic getJSON(CrowdSSORequest SSORequest)
        {
            //Dynamic as the JSON will need to be multiple types when returned
            dynamic returnJSON = "";
            string requestURL = buildAPICall(SSORequest);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(crowdLocation + "rest/usermanagement/1/" + requestURL);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Method = SSORequest.method;
            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic " + Encode(applicationName, applicationPassword));

            if (SSORequest.password != "")
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(
                        new
                        {
                            value = SSORequest.password
                        });

                    writer.Write(json);

                }
            }
            try
            {
                HttpWebResponse result = (HttpWebResponse)request.GetResponse();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(result.GetResponseStream()))
                    {
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(reader.ReadToEnd());
                        returnJSON = json;
                    }

                }
            }
            catch (WebException ex)
            {
                //Log it, alert someone and then return the empty details
                returnJSON = ex;
            }

            return returnJSON;

        }

        private string buildAPICall(CrowdSSORequest SSORequest)
        {

            string requestURL = "";

            if (SSORequest.apiCall == CrowdSSOAPICall.Authenticate)
            {
                requestURL += CrowdSSOAPICall.Authenticate.ToString() + SSORequest.username;
            }

            if (SSORequest.apiCall == CrowdSSOAPICall.ChangePassword)
            {
                requestURL += CrowdSSOAPICall.ChangePassword.ToString() + SSORequest.username;
            }

            if (SSORequest.apiCall == CrowdSSOAPICall.RequestPasswordReset)
            {
                requestURL += CrowdSSOAPICall.RequestPasswordReset.ToString() + SSORequest.username;
            }

            if (SSORequest.apiCall == CrowdSSOAPICall.UserDetail)
            {
                requestURL += CrowdSSOAPICall.UserDetail.ToString() + SSORequest.username;
            }

            if (SSORequest.apiCall == CrowdSSOAPICall.UsersInGroup)
            {
                requestURL += CrowdSSOAPICall.UsersInGroup.ToString() + SSORequest.group;
            }

            if (SSORequest.apiCall == CrowdSSOAPICall.UserAttribute)
            {
                requestURL += CrowdSSOAPICall.UserAttribute.ToString() + SSORequest.username;
            }

            return requestURL;

        }

        #endregion
    }

}
