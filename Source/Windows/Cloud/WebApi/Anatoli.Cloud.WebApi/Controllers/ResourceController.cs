using Anatoli.Business.Domain.Application;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/resources")]
    public class ResourceController : AnatoliApiController
    {
        [Route("list"), HttpGet]
        public IHttpActionResult List()
        {
            //var appDomain = new ApplicationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
            var resourceDomain = new ResourceDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

            //var viewModel = new ModuleListViewModel
            //{
            //    Applications = appDomain.MainRepository.GetAll().Select(app => new ApplicationViewModel
            //    {
            //        Id = app.Id,
            //        Name = app.Name
            //    }).ToList(),

            //    Modules = moduleDomain.MainRepository.GetAllModulesWithApp().Select(module => new ModuleViewModel
            //    {
            //        AppId = module.ApplicationId,
            //        AppName = module.Application.Name,
            //        Id = module.Id,
            //        ModuleName = module.Name
            //    }).ToList()
            //};

            var viewModel = resourceDomain.MainRepository.GetAllResourcesWithApp().Select(resource => new ResourceViewModel
            {
                ApplicationId = resource.ApplicationModule.ApplicationId,
                ApplicationName = resource.ApplicationModule.Application.Name,
                ModuleId = resource.ApplicationModuleId,
                ModuleName = resource.ApplicationModule.Name,
                ResourceId = resource.Id,
                ResourceName = resource.Name
            }).ToList();

            return Ok(viewModel);
        }

        [Route("save"), HttpPost]
        public async Task<IHttpActionResult> Save([FromBody]ApplicationModuleResourceItemViewModel resource)
        {
            try
            {
                var resourceDomain = new ResourceDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                if (resource == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }
                else if (resource.ResourceId != Guid.Empty)
                {
                    var dbResource = await resourceDomain.MainRepository.FindAsync(m => m.Id == resource.ResourceId);
                    dbResource.ApplicationModuleId = resource.ModuleId;
                    dbResource.Name = resource.ResourceName;
                    await resourceDomain.MainRepository.UpdateAsync(dbResource);
                }
                else
                {
                    var dbResource = new ApplicationModuleResource
                    {
                        Id = Guid.NewGuid(),
                        ApplicationModuleId = resource.ModuleId,
                        Name = resource.ResourceName
                    };
                    await resourceDomain.MainRepository.AddAsync(dbResource);
                }

                await resourceDomain.MainRepository.SaveChangesAsync();

                return Ok(resource);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("remove/{id}"), HttpPost]
        public async Task<IHttpActionResult> Remove(Guid id)
        {
            try
            {
                var resourceDomain = new ResourceDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var dbResource = await resourceDomain.MainRepository.FindAsync(m => m.Id == id);
                if (dbResource == null)
                    return NotFound();

                await resourceDomain.MainRepository.DeleteAsync(dbResource);
                await resourceDomain.MainRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
