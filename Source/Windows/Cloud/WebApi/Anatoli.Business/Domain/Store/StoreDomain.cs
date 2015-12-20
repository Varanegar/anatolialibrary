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
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Domain
{
    public class StoreDomain : IBusinessDomain<Store, StoreViewModel>
        {

            #region Properties
            public IAnatoliProxy<Store, StoreViewModel> Proxy { get; set; }
            public IRepository<Store> Repository { get; set; }
            public IPrincipalRepository PrincipalRepository { get; set; }
            public Guid PrivateLabelOwnerId { get; private set; }

            #endregion

            #region Ctors
            StoreDomain() { }
            public StoreDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
            public StoreDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
                : this(new StoreRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Store, StoreViewModel>.Create())
            {
                PrivateLabelOwnerId = privateLabelOwnerId;
            }
            public StoreDomain(IRepository<Store> repository, IPrincipalRepository principalRepository, IAnatoliProxy<Store, StoreViewModel> proxy)
            {
                Proxy = proxy;
                Repository = repository;
                PrincipalRepository = principalRepository;
            }
            #endregion

            #region Methods
            public async Task<List<StoreViewModel>> GetAll()
            {
                var stores = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

                return Proxy.Convert(stores.ToList()); ;
            }

            public async Task<List<StoreViewModel>> GetAllChangedAfter(DateTime selectedDate)
            {
                var stores = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

                return Proxy.Convert(stores.ToList()); ;
            }

            public void Publish(List<StoreViewModel> StoreViewModels)
            {
                //await Task.Factory.StartNew(() =>
                //{
                var stores = Proxy.ReverseConvert(StoreViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                stores.ForEach(item =>
                {
                    //var product = ProductRepository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();


                    //if (product != null)
                    //{
                    //    product.ProductName = item.ProductName;
                    //    ProductRepository.UpdateAsync(product);

                    //}
                    //else
                    //{
                    //item.Suppliers.ToList().ForEach(itm =>
                    //{
                    //    itm.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    //    itm.CreatedDate = itm.LastUpdate = DateTime.Now;
                    //});

                    //item.PrivateLabelOwner = item.Manufacture.PrivateLabelOwner = item.ProductGroup.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;

                    //item.CreatedDate = item.LastUpdate = item.Manufacture.CreatedDate =
                    //item.Manufacture.LastUpdate = item.ProductGroup.CreatedDate = item.ProductGroup.LastUpdate = DateTime.Now;

                    //item.ProductGroup.ProductGroup2 = null;

                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                    Repository.Add(item);
                    //}
                });

                Repository.SaveChanges();
                //});
            }
            public async Task PublishAsync(List<StoreViewModel> StoreViewModels)
            {
                var stores = Proxy.ReverseConvert(StoreViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                stores.ForEach(item =>
                {
                    //var product = ProductRepository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                    //if (product != null)
                    //{
                    //    product.ProductName = item.ProductName;
                    //    ProductRepository.UpdateAsync(product);

                    //}
                    //else
                    //{

                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                    Repository.AddAsync(item);
                    //}
                });
                await Repository.SaveChangesAsync();
            }

            public async Task Delete(List<StoreViewModel> StoreViewModels)
            {
                await Task.Factory.StartNew(() =>
                {
                    var stores = Proxy.ReverseConvert(StoreViewModels);

                    stores.ForEach(item =>
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
