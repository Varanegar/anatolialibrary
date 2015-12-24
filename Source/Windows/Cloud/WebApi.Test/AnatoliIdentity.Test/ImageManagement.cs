using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace ClientApp
{
    public class ImageManagement
    {
        public static void TestImageManagement(HttpClient client, string servserURI)
        {
            var requestContent = new MultipartFormDataContent();
            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(File.ReadAllBytes(@"c:\resid-2.jpg"));
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");

            requestContent.Add(imageContent, "resid-2", "resid-2.jpg");
            var response = client.PostAsync(servserURI + "/api/v0/imageManager/Save?token=" + Guid.NewGuid().ToString(), requestContent).Result;

        }

        public static void ProductPicture()
        {
            using (var context = new DataContext())
            {
                using (IDataReader dr = context.Query("select * from users"))
                {
                    while (dr.Read())
                    {
                    }
                }
            }
        }

    }
}
