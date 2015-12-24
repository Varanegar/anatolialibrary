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
    public class BasketItemDomain : IBusinessDomain<Manufacture, ManufactureViewModel>
    {
        #region Properties
        public IAnatoliProxy<Manufacture, ManufactureViewModel> Proxy { get; set; }
        public IRepository<Manufacture> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        BasketItemDomain() { }
        public BasketItemDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public BasketItemDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ManufactureRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Manufacture, ManufactureViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public BasketItemDomain(IManufactureRepository manufactureRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Manufacture, ManufactureViewModel> proxy)
        {
            Proxy = proxy;
            Repository = manufactureRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ManufactureViewModel>> GetAll()
        {
            var manufactures = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(manufactures.ToList()); ;
        }

        public async Task<List<ManufactureViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var manufactures = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(manufactures.ToList()); ;
        }

        public async Task PublishAsync(List<ManufactureViewModel> manufactureViewModels)
        {
            var manufactures = Proxy.ReverseConvert(manufactureViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            manufactures.ForEach(item =>
            {
                item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                var currentManufacture = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (currentManufacture != null)
                    currentManufacture.ManufactureName = item.ManufactureName;
                else
                {
                    item.Id = Guid.NewGuid();
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                }
            });

            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<ManufactureViewModel> manufactureViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var manufactures = Proxy.ReverseConvert(manufactureViewModels);

                manufactures.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }
        #endregion
    }
}
