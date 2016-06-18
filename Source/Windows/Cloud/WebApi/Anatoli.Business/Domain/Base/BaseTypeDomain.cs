using System;
using System.Linq;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.BaseModels;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.Common.Business;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class BaseTypeDomain : BusinessDomainV2<BaseType, BaseTypeViewModel, BaseTypeRepository, IBaseTypeRepository>, IBusinessDomainV2<BaseType, BaseTypeViewModel>
    {
        #region Properties
        public IRepository<BaseValue> BaseValueRepository { get; set; }
        #endregion

        #region Ctors
        public BaseTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public BaseTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            BaseValueRepository = new BaseValueRepository(dbc);
        }
        #endregion

        #region Methods
        public override async Task PublishAsync(List<BaseType> dataListInfo)
        {
            try
            {
                
                var currentTypes = MainRepository.GetQuery().Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && p.DataOwnerCenterId == DataOwnerCenterKey).ToList();

                foreach (BaseType item in dataListInfo)
                {
                    var currentType = currentTypes.Find(p => p.Id == item.Id);
                    if (currentType != null)
                    {
                        currentType.BaseTypeDesc = item.BaseTypeDesc;
                        currentType.LastUpdate = DateTime.Now;
                        currentType = await SetBaseValueData(currentType, item.BaseValues.ToList(), ((AnatoliDbContext)MainRepository.DbContext));
                        await MainRepository.UpdateAsync(currentType);
                    }
                    else
                    {
                        item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;

                        item.BaseValues.ToList().ForEach(itemDetail =>
                        {
                            itemDetail.ApplicationOwnerId = item.ApplicationOwnerId;
                            itemDetail.DataOwnerId = DataOwnerKey;
                            itemDetail.DataOwnerCenterId = DataOwnerCenterKey;
                            itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                        });
                        await MainRepository.AddAsync(item);
                    }
                }
                await MainRepository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
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
                        item.ApplicationOwnerId = data.ApplicationOwnerId;
                        item.CreatedDate = item.LastUpdate = data.CreatedDate;
                        BaseValueRepository.Add(item);
                    }
                    else
                    {
                        BaseValueRepository.DeleteBatch(p => p.BaseTypeId == data.Id);
                        var currentBaseValue = BaseValueRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                        if (currentBaseValue.BaseValueName != item.BaseValueName)
                        {
                            currentBaseValue.BaseValueName = item.BaseValueName;
                            currentBaseValue.LastUpdate = DateTime.Now;
                            BaseValueRepository.Update(currentBaseValue);
                        }
                    }
                }
                BaseValueRepository.SaveChanges();
            });
            return data;
        }
        #endregion
    }
}
