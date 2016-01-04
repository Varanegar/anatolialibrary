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
    public class ProductPictureDomain : BusinessDomain<ProductPictureViewModel>, IBusinessDomain<ProductPicture, ProductPictureViewModel>
    {
        #region Properties
        public IAnatoliProxy<ProductPicture, ProductPictureViewModel> Proxy { get; set; }
        public IRepository<ProductPicture> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductPictureDomain() { }
        public ProductPictureDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductPictureDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductPictureRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<ProductPicture, ProductPictureViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductPictureDomain(IProductPictureRepository ProductPictureRepository, IPrincipalRepository principalRepository, IAnatoliProxy<ProductPicture, ProductPictureViewModel> proxy)
        {
            Proxy = proxy;
            Repository = ProductPictureRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductPictureViewModel>> GetAll()
        {
            var productPictures = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(productPictures.ToList()); ;
        }

        public async Task<List<ProductPictureViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var productPictures = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(productPictures.ToList()); ;
        }

        public async Task PublishAsync(List<ProductPictureViewModel> ProductPictureViewModels)
        {
            var productPictures = Proxy.ReverseConvert(ProductPictureViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            productPictures.ForEach(item =>
            {
                item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                var currentProductPicture = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                if (currentProductPicture != null)
                {
                    currentProductPicture.ProductPictureName = item.ProductPictureName;
                    currentProductPicture.IsDefault = item.IsDefault;
                    currentProductPicture.PictureTypeValueGuid = item.PictureTypeValueGuid;
                    currentProductPicture = SetProductData(currentProductPicture, item.Product, Repository.DbContext);
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                }
            });

            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<ProductPictureViewModel> ProductPictureViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var productPictures = Proxy.ReverseConvert(ProductPictureViewModels);

                productPictures.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public ProductPicture SetProductData(ProductPicture data, Product product, AnatoliDbContext context)
        {
            ProductDomain productDomain = new ProductDomain(data.PrivateLabelOwner.Id, context);
            var result = productDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == product.PrivateLabelOwner.Id && p.Number_ID == product.Number_ID).FirstOrDefault();
            data.Product = result;
            return data;
        }
        #endregion
    }
}
