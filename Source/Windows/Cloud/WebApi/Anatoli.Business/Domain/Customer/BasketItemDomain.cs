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
    public class BasketItemDomain : IBusinessDomain<BasketItem, BasketItemViewModel>
    {
        #region Properties
        public IAnatoliProxy<BasketItem, BasketItemViewModel> Proxy { get; set; }
        public IRepository<BasketItem> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        BasketItemDomain() { }
        public BasketItemDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public BasketItemDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new BasketItemRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<BasketItem, BasketItemViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public BasketItemDomain(IBasketItemRepository basketRepository, IPrincipalRepository principalRepository, IAnatoliProxy<BasketItem, BasketItemViewModel> proxy)
        {
            Proxy = proxy;
            Repository = basketRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<BasketItemViewModel>> GetAll()
        {
            var basketItems = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(basketItems.ToList()); ;
        }
        public async Task<List<BasketItemViewModel>> GetBasketByBasketId(string basketId)
        {
            Guid basketGuid = Guid.Parse(basketId);
            var basketItems = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.BasketId == basketGuid);

            return Proxy.Convert(basketItems.ToList());
        }

        public async Task<List<BasketItemViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var basketItems = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(basketItems.ToList()); ;
        }

        public async Task PublishAsync(List<BasketItemViewModel> basketItemViewModels)
        {
            var basketItems = Proxy.ReverseConvert(basketItemViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            basketItems.ForEach(item =>
            {
                item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                var currentBasket = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (currentBasket != null)
                {
                    currentBasket.ProductId = item.ProductId;
                    currentBasket.Comment = item.Comment;
                    currentBasket.Qty = item.Qty;
                    Repository.UpdateAsync(currentBasket);
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                    Repository.AddAsync(item);
                }
            });

            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<BasketItemViewModel> basketItemViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var basketItems = Proxy.ReverseConvert(basketItemViewModels);

                basketItems.ForEach(item =>
                {
                    var basketItem = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                    Repository.DeleteAsync(basketItem);
                });

                Repository.SaveChangesAsync();
            });
        }
        #endregion
    }
}
