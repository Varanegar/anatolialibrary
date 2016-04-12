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
using Anatoli.Business.Proxy.ProductConcretes;
using Anatoli.ViewModels;
using Anatoli.DataAccess.Models;

namespace Anatoli.Cloud.WebApi.Controllers.ImageManager
{
    [RoutePrefix("api/imageManager")]
    public class ImageManagerController : AnatoliApiController
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
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("images"), HttpGet, HttpPost]
        public async Task<IHttpActionResult> GetImages()
        {
            try
            {
                var items = await new ItemImageDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();

                return Ok(items) ;
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("images/after"), HttpPost, HttpGet]
        public async Task<IHttpActionResult> GetImages(string dateAfter)
        {
            try
            {
                var productDomain = new ItemImageDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(dateAfter);
                var result = await productDomain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [HttpPost, Route("Save")]
        public async Task<IHttpActionResult> SaveImageSimple(string token, string imagetype, string imageId)
        {
            return await SaveImage(token, imagetype, imageId, true);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [HttpPost, Route("Save")]
        public async Task<IHttpActionResult> SaveImage(string token, string imagetype, string imageId, bool isDefault)
        {
            try
            {

                var itemImageDomain = new ItemImageDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var httpRequest = HttpContext.Current.Request;
                Guid _token = Guid.Empty;
                if (httpRequest.Form["token"] != null )
                    Guid.TryParse(httpRequest.Form["token"], out _token);

                if(token != "")
                    Guid.TryParse(token, out _token);

                List<ItemImage> dataList = new List<ItemImage>();
                if (httpRequest.Files.Count > 0){
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFileBase postedFile = new HttpPostedFileWrapper(httpRequest.Files[file]);

                        await FileManager.Save(postedFile, imagetype, _token.ToString(), file);

                        ItemImage imageViewModel = new ItemImage()
                            {
                                TokenId = token,
                                Id = Guid.Parse(imageId),
                                ImageType = imagetype,
                                ImageName = file,
                                IsDefault = isDefault,
                            };
                        dataList.Add(imageViewModel);
                    }

                    await itemImageDomain.PublishAsync(dataList);

                }

                return Ok("");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return GetErrorResult(ex);
            }
        }

        #endregion
    }
}