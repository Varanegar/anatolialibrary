﻿using Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            ParseClient.Initialize("wUAgTsRuLdin0EvsBhPniG40O24i2nEGVFl8R5OI", "G7guVuyx35bb4fBOwo7BVhlG2L2E2qKLQI0sLAe0");
            ParseInstallation.CurrentInstallation.SaveAsync();
            //ParsePush.ParsePushNotificationReceived += ParsePush_ParsePushNotificationReceived;
        }

        private void test(string installationId)
        {
            try
            {
                var data = new Dictionary<string, object>();
                data.Add("product", "[{\'uniqueIdString\':null,\'parentId\':-1,\'parentUniqueIdString\':\'a4f21a45-9e38-4337-b4f4-79b1de61bf35\',\'groupName\':\'غذاهاي آماده\',\'nLeft\':184,\'nRight\':185,\'nLevel\':2,\'charGroupIdString\':null,\'isRemoved\':false,\'id\':82,\'uniqueId\':\'5254eccf-d423-40f8-99bb-00cb6cd82148\',\'privateOwnerId\':\'3eee33ce-e2fd-4a5d-a71c-103cc5046d0c\'}]");
                
                var push = new ParsePush()
                {
                    Query = from installation in ParseInstallation.Query
                            //where installation.Get<string>("installationId") == "01c7875b-4851-4c22-9a39-9b17c9091991"
                            where installation.Get<string>("userUniqueId") == "dpowedoiwejdwoeduweopd-dwedwe-dedwe-dwed"
                        select installation,
                    //Alert = "hello world",
                    Data = data,
                };
                
                //var push2 = new ParsePush()
                //{
                //    Channels = new List<string> { "Eigg" },
                //    Alert = "heloo channel",
                //};
                push.SendAsync().Wait();
            }
            catch (Exception ex)
            {
                //Todo: log4net
                throw;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            test("b3cfc74e-2004-47f5-acd7-a9b6f8811076");
            //var result2 = await ParseCloud.CallFunctionAsync<string>("hello", new Dictionary<string, object>());
            //var result = await ParseCloud.CallFunctionAsync<IList<ParseInstallation>>("pushToAll", new Dictionary<string, object>());
            //string s = "";
            ///*
            // * ParseInstallation parsInstall = ParseInstallation.CurrentInstallation;
            //parsInstall["userId"] = "anonymous";
            //await parsInstall.SaveAsync();
            //*/
            ////var q2 = new Parse.Query(Parse.Installation);
            //var q2 = ParseInstallation.Query;
            //q2.WhereEqualTo("installationId", "b3cfc74e-2004-47f5-acd7-a9b6f8811076");
            //var res = q2.Select("installationId");
            ////var data = res.FirstOrDefaultAsync();
            //var query = from installation in ParseInstallation.Query
            //            where installation.Get<string>("userId") == "anonymous"
            //            select installation;
            //var results = query.FirstOrDefaultAsync().Result;
            
            ////post.UpdatedAt
            ////var user = ParseUser.CurrentUser;
            ///*
            //// Create a new Parse object
            //var post = new ParseObject("Post");
            //post["title"] = "Hello World";

            //// Save it to Parse
           
            //*/

            
            ///*var query = from post in ParseInstallation.GetQuery("post")
            //            //where post["author"] == ParseUser.CurrentUser
            //            orderby post.CreatedAt descending
            //            select post;
            //*/
            
            // Retrieve the results
            //IEnumerable<ParseObject> postsByUser = await query.FindAsync();
        }

        //void ParsePush_ParsePushNotificationReceived(object sender, ParsePushNotificationEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
