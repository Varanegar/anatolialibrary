using Anatoli.Business.Domain.Application;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Anatoli.DataAccess.Models.Identity;
using System.Threading.Tasks;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/apps")]
    public class ApplicationController : AnatoliApiController
    {
        public ApplicationDomain GetDomain()
        {
            return new ApplicationDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
        }

        [Route("list"), HttpGet]
        public IHttpActionResult Apps()
        {
            var appDomain = GetDomain();
            var apps = appDomain.MainRepository.GetAll();
            var appViewModels = apps.Select(app => new ApplicationViewModel() { Id = app.Id, Name = app.Name });
            return Ok(appViewModels);
        }

        [Route("save"), HttpPost]
        public async Task<IHttpActionResult> Save([FromBody]ApplicationViewModel app)
        {
            try
            {
                var appDomain = GetDomain();

                if (app == null)
                {
                    return BadRequest();
                }
                else if (app.Id != Guid.Empty)
                {
                    //ApplicationDb.First(dbApp => dbApp.Id == app.Id).Name = app.Name;
                    var appToUpdate = await appDomain.MainRepository.FindAsync(dbApp => dbApp.Id == app.Id);
                    if (appToUpdate == null)
                        return NotFound();

                    appToUpdate.Name = app.Name;
                    appDomain.MainRepository.Update(appToUpdate);
                }
                else
                {
                    var newApp = new Application
                    {
                        Id = Guid.NewGuid(),
                        Name = app.Name
                    };
                    appDomain.MainRepository.Add(newApp);
                }

                appDomain.MainRepository.SaveChanges();
                return Ok();
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
                var domain = GetDomain();
                var appToDelete = await domain.MainRepository.FindAsync(app => app.Id == id);
                if (appToDelete == null)
                    return NotFound();

                await domain.MainRepository.DeleteAsync(appToDelete);
                await domain.MainRepository.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("appsWithModules"), HttpGet]
        public IHttpActionResult AppsWithModules()
        {
            try
            {
                var appsWithDependencies = GetDomain().MainRepository.GetAppsWithModulesAndResources();
                var appViewModels = new List<ApplicationItemViewModel>();
                foreach (var app in appsWithDependencies)
                {
                    var appVM = new ApplicationItemViewModel
                    {
                        ApplicationId = app.Id,
                        ApplicationName = app.Name
                    };

                    foreach (var module in app.ApplicationModules)
                    {
                        var moduleVM = new ApplicationModuleItemViewModel
                        {
                            ApplicationId = appVM.ApplicationId,
                            ModuleId = module.Id,
                            ModuleName = module.Name
                        };

                        foreach (var resource in module.ApplicationModuleResources)
                        {
                            var resourceVM = new ApplicationModuleResourceItemViewModel
                            {
                                ModuleId = moduleVM.ModuleId,
                                ResourceId = resource.Id,
                                ResourceName = resource.Name
                            };

                            moduleVM.Resources.Add(resourceVM);
                        }

                        appVM.Modules.Add(moduleVM);
                    }

                    appViewModels.Add(appVM);
                }

                return Ok(appViewModels);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}