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
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class BasketDomain : BusinessDomain<BasketViewModel>, IBusinessDomain<Basket, BasketViewModel>
    {
        #region Properties
        public IAnatoliProxy<Basket, BasketViewModel> Proxy { get; set; }
        public IRepository<Basket> Repository { get; set; }
        public IRepository<BasketItem> ItemRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        BasketDomain() { }
        public BasketDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public BasketDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new BasketRepository(dbc), new BasketItemRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Basket, BasketViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public BasketDomain(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Basket, BasketViewModel> proxy)
        {
            Proxy = proxy;
            Repository = basketRepository;
            ItemRepository = basketItemRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<BasketViewModel>> GetAll()
        {
            var baskets = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(baskets.ToList()); ;
        }
        public async Task<List<BasketViewModel>> GetBasketByCustomerId(string customerId)
        {
            Guid  customerGuid = Guid.Parse(customerId);
            var baskets = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.CustomerId == customerGuid);

            return Proxy.Convert(baskets.ToList());
        }

        public async Task<List<BasketViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var baskets = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(baskets.ToList()); ;
        }

        public async Task PublishAsync(List<BasketViewModel> basketViewModels)
        {
            try
            {
                var baskets = Proxy.ReverseConvert(basketViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach(Basket item in baskets)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentBasket = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Id == item.Id).FirstOrDefault();
                    if (currentBasket != null)
                    {
                        currentBasket.BasketName = item.BasketName;
                        currentBasket.LastUpdate = DateTime.Now;
                        currentBasket = await SetBasketItemData(currentBasket, item.BasketItems.ToList(), Repository.DbContext);
                        await Repository.UpdateAsync(currentBasket);
                    }
                    else
                    {
                        item.Id = item.Id == Guid.Empty ? Guid.NewGuid(): item.Id;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        item.BasketItems.ToList().ForEach(itemDetail =>
                            {
                                itemDetail.PrivateLabelOwner = item.PrivateLabelOwner;
                                itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                                itemDetail.Id = itemDetail.Id == Guid.Empty ? Guid.NewGuid() : itemDetail.Id;
                            });
                        await Repository.AddAsync(item);
                    }
                };
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task Delete(List<BasketViewModel> basketViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var baskets = Proxy.ReverseConvert(basketViewModels);

                baskets.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public async Task<Basket> SetBasketItemData(Basket data, List<BasketItem> basketItems, AnatoliDbContext context)
        {
            if (data.BasketItems != null)
                data.BasketItems.ToList().ForEach(item => { ItemRepository.DeleteAsync(item); });

            foreach (BasketItem item in basketItems)
            { 
                item.BasketId = data.Id;
                item.PrivateLabelOwner = data.PrivateLabelOwner;
                item.CreatedDate = item.LastUpdate = data.CreatedDate;
                item.Id = Guid.NewGuid();
                await ItemRepository.AddAsync(item);
            };
            return data;
        }

        #endregion
    }
}
