using System;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Common.WebApi;

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

                return Ok(items);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("images/compress"), HttpGet, HttpPost, GzipCompression]
        public async Task<IHttpActionResult> GetImagesCompress()
        {
            return await GetImages();
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("images/after"), HttpPost, HttpGet]
        public async Task<IHttpActionResult> GetImages(string dateAfter)
        {
            try
            {
                var productDomain = new ItemImageDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(dateAfter);
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
        [Route("images/compress/after"), HttpPost, HttpGet, GzipCompression]
        public async Task<IHttpActionResult> GetImagesCompress(string dateAfter)
        {
            return await GetImages(dateAfter);
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

                var _token = Guid.Empty;
                if (httpRequest.Form["token"] != null)
                    Guid.TryParse(httpRequest.Form["token"], out _token);

                if (token != "")
                    Guid.TryParse(token, out _token);

                if (httpRequest.Files.Count > 0)
                {
                    var dataList = new List<ItemImage>();
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFileBase postedFile = new HttpPostedFileWrapper(httpRequest.Files[file]);

                        //you can still use same old method without ImageMagick.
                        await FileManager.Save(postedFile, imagetype, _token.ToString(), file);

                        dataList.Add(new ItemImage()
                        {
                            TokenId = token,
                            Id = Guid.Parse(imageId),
                            ImageType = imagetype,
                            ImageName = file,
                            IsDefault = isDefault,
                        });
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