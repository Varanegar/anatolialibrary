using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Domain
{
    public class ManufactureDomain : BusinessDomain<ManufactureViewModel>, IBusinessDomain<Manufacture, ManufactureViewModel>
    {
        #region Properties
        public IAnatoliProxy<Manufacture, ManufactureViewModel> Proxy { get; set; }
        public IRepository<Manufacture> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ManufactureDomain() { }
        public ManufactureDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ManufactureDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ManufactureRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Manufacture, ManufactureViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ManufactureDomain(IManufactureRepository manufactureRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Manufacture, ManufactureViewModel> proxy)
        {
            Proxy = proxy;
            Repository = manufactureRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ManufactureViewModel>> GetAll()
        {
            var dataLists = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataLists.ToList()); ;
        }

        public async Task<List<ManufactureViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataLists = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataLists.ToList()); ;
        }

        public async Task<List<ManufactureViewModel>> PublishAsync(List<ManufactureViewModel> dataViewModels)
        {
            try
            {
                var dataLists = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                dataLists.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentManufacture = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentManufacture != null)
                    {
                        if (currentManufacture.ManufactureName != item.ManufactureName)
                        {
                            currentManufacture.ManufactureName = item.ManufactureName;
                            currentManufacture.LastUpdate = DateTime.Now;
                            Repository.UpdateAsync(currentManufacture);
                        }
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.AddAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            return dataViewModels;
        }

        public async Task<List<ManufactureViewModel>> Delete(List<ManufactureViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataLists = Proxy.ReverseConvert(dataViewModels);

                dataLists.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DbContext.Manufactures.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
