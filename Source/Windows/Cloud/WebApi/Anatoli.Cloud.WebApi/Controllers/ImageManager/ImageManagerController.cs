using System;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System.Drawing;
using System.Web.Http;
using System.Net.Http;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using Anatoli.Cloud.WebApi.Classes;

namespace Anatoli.Cloud.WebApi.Controllers.ImageManager
{
    [RoutePrefix("api/v0/imageManager")]
    public class ImageManagerController : ApiController
    {
        #region Properties
        public IFileManager FileManager { get; set; }
        #endregion

        #region Ctors
        public ImageManagerController()
            : this(new FileManager())
        { }
        public ImageManagerController(IFileManager fileManager)
        {
            FileManager = fileManager;
        }
        #endregion

        #region Actions
        [HttpPost, Route("Save")]
        public async Task<string> SaveImage(string token = "")
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                Guid _token;
                Guid.TryParse(httpRequest.Form["token"], out _token);

                if (httpRequest.Files.Count > 0)
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFileBase postedFile = new HttpPostedFileWrapper(httpRequest.Files[file]);

                        await FileManager.Save(postedFile, ObjectTypes.Products, _token.ToString());
                    }

                return "";
            }
            catch (Exception ex)
            {
                return "Invalid Operation!";
                //todo: log error!
            }
        }
        [HttpPost, Route("Remove")]
        public async Task<string> RemoveImages(string[] fileNames)
        {
            try
            {
                if (fileNames != null)
                    foreach (var fullName in fileNames)
                        await FileManager.Remove("", ObjectTypes.Products, fullName);

                // Return an empty string to signify success
                return "";
            }
            catch (Exception ex)
            {
                return "Invalid Operation!";

                //todo: log error!
            }
        }
        [HttpGet, Route("Names")]
        public List<string> GetImageNames(string token)
        {
            return FileManager.GetFileNames(token, ObjectTypes.Products);
        }

        [HttpGet, Route("Image")]
        public HttpResponseMessage GetImage(string filename, string token = "", int width = 0, int height = 0)
        {
            string filepath = new FileManager().GetPath(token, ObjectTypes.Products, filename);

            var imgActual = Scale(Image.FromFile(filepath), width, height);

            var ms = new MemoryStream();

            imgActual.Save(ms, ImageFormat.Png);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ms.ToArray())
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            return result;
        }
        private Image Scale(Image imgPhoto, int Width = 0, int Height = 0)
        {
            float sourceWidth = imgPhoto.Width,
                    sourceHeight = imgPhoto.Height,
                    destHeight = imgPhoto.Height,
                    destWidth = imgPhoto.Width;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else if (Width != 0)
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight);//,
            // PixelFormat.Format32bppPArgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.Default;//HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(0, 0, (int)destWidth, (int)destHeight),
                new Rectangle(0, 0, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
        #endregion
    }
}