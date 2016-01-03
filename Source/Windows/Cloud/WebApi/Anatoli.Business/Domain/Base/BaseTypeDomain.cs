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
    public class BaseTypeDomain : BusinessDomain<BaseTypeViewModel>, IBusinessDomain<BaseType, BaseTypeViewModel>
    {
        #region Properties
        public IAnatoliProxy<BaseType, BaseTypeViewModel> Proxy { get; set; }
        public IRepository<BaseType> Repository { get; set; }
        public IRepository<BaseValue> BaseValueRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        BaseTypeDomain() { }
        public BaseTypeDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public BaseTypeDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new BaseTypeRepository(dbc), new BaseValueRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<BaseType, BaseTypeViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public BaseTypeDomain(IBaseTypeRepository baseTypeRepository, IBaseValueRepository baseValueRepository, IPrincipalRepository principalRepository, IAnatoliProxy<BaseType, BaseTypeViewModel> proxy)
        {
            Proxy = proxy;
            Repository = baseTypeRepository;
            BaseValueRepository = baseValueRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<BaseTypeViewModel>> GetAll()
        {
            var baseTypes = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(baseTypes.ToList()); ;
        }

        public async Task<List<BaseTypeViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var baseTypes = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(baseTypes.ToList()); ;
        }

        public async Task PublishAsync(List<BaseTypeViewModel> BaseTypeViewModels)
        {
            try
            {
                var baseTypes = Proxy.ReverseConvert(BaseTypeViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentTypes = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                foreach (BaseType item in baseTypes)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentType = currentTypes.Find(p => p.Id == item.Id);
                    if (currentType != null)
                    {
                        currentType.BaseTypeDesc = item.BaseTypeDesc;
                        currentType.LastUpdate = DateTime.Now;
                        currentType = await SetBaseValueData(currentType, item.BaseValues.ToList(), Repository.DbContext);
                        await Repository.UpdateAsync(currentType);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        item.BaseValues.ToList().ForEach(itemDetail =>
                        {
                            itemDetail.PrivateLabelOwner = item.PrivateLabelOwner;
                            itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                        });
                        await Repository.AddAsync(item);
                    }
                }
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task Delete(List<BaseTypeViewModel> BaseTypeViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var baseTypes = Proxy.ReverseConvert(BaseTypeViewModels);

                baseTypes.ForEach(item =>
                {
                    var baseType = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DeleteAsync(baseType);
                });

                Repository.SaveChangesAsync();
            });
        }

        public async Task<BaseType> SetBaseValueData(BaseType data, List<BaseValue> baseValues, AnatoliDbContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (BaseValue item in baseValues)
                {
                    var count = data.BaseValues.ToList().Count(u => u.Id == item.Id);
                    if (count == 0)
                    {
                        item.BaseTypeId = data.Id;
                        item.PrivateLabelOwner = data.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = data.CreatedDate;
                        BaseValueRepository.Add(item);
                    }
                    else
                    {
                        var currentBaseValue = BaseValueRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                        if (currentBaseValue.BaseValueName != item.BaseValueName)
                        {
                            currentBaseValue.BaseValueName = item.BaseValueName;
                            currentBaseValue.LastUpdate = DateTime.Now;
                            BaseValueRepository.Update(currentBaseValue);
                        }
                    }
                }
            });
            return data;
        }
        #endregion
    }
}
