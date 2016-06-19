using Anatoli.Business.Domain.Application;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/dataOwners")]
    public class DataOwnerController : AnatoliApiController
    {
        [Route("list")]
        [HttpGet]
        public IHttpActionResult List()
        {
            try
            {
                var dataOwnerDomain = new DataOwnerDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var dataOwners = dataOwnerDomain.MainRepository.GetDataOwnerWithDetails();

                var viewModel = dataOwners.Select(dataOwner => new DataOwnerViewModel
                {
                    Id = dataOwner.Id,
                    Title = dataOwner.Title,
                    WebHookUsername = dataOwner.WebHookUsername,
                    WebHookPassword = dataOwner.WebHookPassword,
                    WebHookURI = dataOwner.WebHookURI,
                    AnatoliContactId = dataOwner.AnatoliContactId,
                    AnatoliContactName = dataOwner.AnatoliContact.ContactName,

                    ApplicationOwnerId = dataOwner.ApplicationOwnerId,
                    ApplicationOwnerName = dataOwner.ApplicationOwner.Application.Name
                });

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}