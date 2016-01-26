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
using Anatoli.ViewModels.BaseModels;
using Anatoli.Business.Domain;

namespace Anatoli.Cloud.WebApi.Controllers.ImageManager
{
    [RoutePrefix("api/imageManager")]
    public class ImageManagerController : BaseApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("images")]
        public async Task<IHttpActionResult> GetImages(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var imageDomain = new ItemImageDomain(owner);
                var result = await imageDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("images/after")]
        public async Task<IHttpActionResult> GetProducts(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var productDomain = new ItemImageDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await productDomain.GetAllChangedAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [HttpPost, Route("Save")]
        public async Task<string> SaveImage(string token, string imagetype, string privateOwnerId, string imageId)
        {
            try
            {

                var owner = Guid.Parse(privateOwnerId);
                var itemImageDomain = new ItemImageDomain(owner);

                var httpRequest = HttpContext.Current.Request;
                Guid _token = Guid.Empty;
                if (httpRequest.Form["token"] != null )
                    Guid.TryParse(httpRequest.Form["token"], out _token);

                if(token != "")
                    Guid.TryParse(token, out _token);

                List<ItemImageViewModel> dataList = new List<ItemImageViewModel>();
                if (httpRequest.Files.Count > 0){
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFileBase postedFile = new HttpPostedFileWrapper(httpRequest.Files[file]);

                        await FileManager.Save(postedFile, imagetype, _token.ToString(), file);

                        ItemImageViewModel imageViewModel = new ItemImageViewModel()
                            {
                                BaseDataId = token,
                                UniqueId = Guid.Parse(imageId),
                                ImageType = imagetype,
                                ImageName = file,
                            };
                        dataList.Add(imageViewModel);
                    }

                    await itemImageDomain.PublishAsync(dataList);

                }

                return "";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return "Invalid Operation!";
                //todo: log error!
            }
        }

        //[HttpPost, Route("Remove")]
        //public async Task<string> RemoveImages(string[] fileNames, string imageType)
        //{
        //    try
        //    {
        //        if (fileNames != null)
        //            foreach (var fullName in fileNames)
        //                await FileManager.Remove("", imageType, fullName);

        //        // Return an empty string to signify success
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Invalid Operation!";

        //        //todo: log error!
        //    }
        //}
        //[HttpGet, Route("Names")]
        //public List<string> GetImageNames(string token, string imageType)
        //{
        //    return FileManager.GetFileNames(token, imageType);
        //}

        //[HttpGet, Route("Image")]
        //public HttpResponseMessage GetImage(string filename, string imageType, string token = "", int width = 0, int height = 0)
        //{
        //    string filepath = new FileManager().GetPath(token, imageType, filename);

        //    var imgActual = new FileManager().Scale(Image.FromFile(filepath), width, height);

        //    var ms = new MemoryStream();

        //    imgActual.Save(ms, ImageFormat.Png);

        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(ms.ToArray())
        //    };

        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

        //    return result;
        //}

        #endregion
    }
}