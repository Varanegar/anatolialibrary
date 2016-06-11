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
    [RoutePrefix("api/modules")]
    public class ModuleController : AnatoliApiController
    {
        static ModuleController()
        {
        }

        [Route("list/{appId?}"), HttpGet]
        public IHttpActionResult List(Guid? appId = null)
        {
            try
            {
                var moduleDomain = new ModuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                IEnumerable<ApplicationModule> modules = null;

                if (appId.HasValue)
                {
                    modules = moduleDomain.MainRepository.GetAllModulesOfApp(appId.Value);
                }
                else
                {
                    modules = moduleDomain.MainRepository.GetAllModulesWithApp();
                }

                var viewModel = modules.Select(module => new ModuleViewModel
                {
                    AppId = module.ApplicationId,
                    AppName = module.Application.Name,
                    Id = module.Id,
                    ModuleName = module.Name
                }).ToList();

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save"), HttpPost]
        public async Task<IHttpActionResult> Save([FromBody]ModuleViewModel module)
        {
            try
            {
                var moduleDomain = new ModuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                if (module == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }
                else if (module.Id != Guid.Empty)
                {
                    var dbModule = await moduleDomain.MainRepository.FindAsync(m => m.Id == module.Id);
                    dbModule.ApplicationId = module.AppId;
                    dbModule.Name = module.ModuleName;
                    await moduleDomain.MainRepository.UpdateAsync(dbModule);
                }
                else
                {
                    var dbModule = new ApplicationModule
                    {
                        Id = Guid.NewGuid(),
                        ApplicationId = module.AppId,
                        Name = module.ModuleName
                    };
                    await moduleDomain.MainRepository.AddAsync(dbModule);
                }

                await moduleDomain.MainRepository.SaveChangesAsync();

                return Ok(module);
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
                var moduleDomain = new ModuleDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var dbModule = await moduleDomain.MainRepository.FindAsync(m => m.Id == id);
                if (dbModule == null)
                    return NotFound();

                await moduleDomain.MainRepository.DeleteAsync(dbModule);
                await moduleDomain.MainRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
