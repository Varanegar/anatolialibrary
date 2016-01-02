﻿using System;
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
    public class ItemImageDomain : BusinessDomain<ItemImageViewModel>, IBusinessDomain<ItemImage, ItemImageViewModel>
    {
        #region Properties
        public IAnatoliProxy<ItemImage, ItemImageViewModel> Proxy { get; set; }
        public IRepository<ItemImage> Repository { get; set; }

        #endregion

        #region Ctors
        ItemImageDomain() { }
        public ItemImageDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ItemImageDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ItemImageRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<ItemImage, ItemImageViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ItemImageDomain(IItemImageRepository itemImageRepository, IPrincipalRepository principalRepository, IAnatoliProxy<ItemImage, ItemImageViewModel> proxy)
        {
            Proxy = proxy;
            Repository = itemImageRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ItemImageViewModel>> GetAll()
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task<List<ItemImageViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var itemImages = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(itemImages.ToList()); ;
        }

        public async Task PublishAsync(List<ItemImageViewModel> itemImageViewModels)
        {
            try
            {
                var itemImages = Proxy.ReverseConvert(itemImageViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (ItemImage item in itemImages)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentItemImage = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentItemImage != null)
                    {
                        currentItemImage.LastUpdate = DateTime.Now;
                        currentItemImage.TokenId = item.TokenId;
                        currentItemImage.ImageName = item.ImageName;
                        currentItemImage.ImageType = item.ImageType;
                        await Repository.UpdateAsync(currentItemImage);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
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

        public async Task Delete(List<ItemImageViewModel> itemImageViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var itemImages = Proxy.ReverseConvert(itemImageViewModels);

                itemImages.ForEach(item =>
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