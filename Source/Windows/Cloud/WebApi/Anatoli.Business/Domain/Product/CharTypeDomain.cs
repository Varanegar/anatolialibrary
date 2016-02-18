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
    public class CharTypeDomain : BusinessDomain<CharTypeViewModel>, IBusinessDomain<CharType, CharTypeViewModel>
    {
        #region Properties
        public IAnatoliProxy<CharType, CharTypeViewModel> Proxy { get; set; }
        public IRepository<CharType> Repository { get; set; }
        public IRepository<CharValue> CharValueRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CharTypeDomain() { }
        public CharTypeDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CharTypeDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CharTypeRepository(dbc), new CharValueRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CharType, CharTypeViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CharTypeDomain(ICharTypeRepository charTypeRepository, ICharValueRepository charValueRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CharType, CharTypeViewModel> proxy)
        {
            Proxy = proxy;
            Repository = charTypeRepository;
            CharValueRepository = charValueRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CharTypeViewModel>> GetAll()
        {
            var charTypes = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(charTypes.ToList()); ;
        }

        public async Task<List<CharTypeViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var charTypes = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(charTypes.ToList()); ;
        }

        public async Task<List<CharTypeViewModel>> PublishAsync(List<CharTypeViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var charTypes = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentTypes = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                foreach (CharType item in charTypes)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentType = currentTypes.Find(p => p.Id == item.Id);
                    if (currentType != null)
                    {
                        currentType.CharTypeDesc = item.CharTypeDesc;
                        currentType.DefaultCharValueGuid = item.DefaultCharValueGuid;
                        currentType.LastUpdate = DateTime.Now;
                        currentType = await SetCharValueData(currentType, item.CharValues.ToList(), Repository.DbContext);
                        await Repository.UpdateAsync(currentType);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        item.CharValues.ToList().ForEach(itemDetail =>
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
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;

        }
        public async Task<List<CharTypeViewModel>> CheckDeletedAsync(List<CharTypeViewModel> dataViewModels)
        {
            try
            {
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.Id) == null)
                    {
                        item.IsRemoved = true;
                        Repository.UpdateAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("CheckForDeletedAsync", ex);
                throw ex;
            }

            return dataViewModels;
        }

        public async Task<List<CharTypeViewModel>> Delete(List<CharTypeViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var charTypes = Proxy.ReverseConvert(dataViewModels);

                charTypes.ForEach(item =>
                {
                    var charType = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.CharTypes.Remove(charType);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }

        public async Task<CharType> SetCharValueData(CharType data, List<CharValue> charValues, AnatoliDbContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (CharValue item in charValues)
                {
                    var count = data.CharValues.ToList().Count(u => u.Id == item.Id);
                    if (count == 0)
                    {
                        item.CharTypeId = data.Id;
                        item.PrivateLabelOwner = data.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = data.CreatedDate;
                        CharValueRepository.Add(item);
                    }
                    else
                    {
                        var currentCharValue = CharValueRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                        if (currentCharValue.CharValueText != item.CharValueText ||
                            currentCharValue.CharValueFromAmount != item.CharValueFromAmount ||
                            currentCharValue.CharValueToAmount != item.CharValueToAmount)
                        {
                            currentCharValue.CharValueText = item.CharValueText;
                            currentCharValue.CharValueFromAmount = item.CharValueFromAmount;
                            currentCharValue.CharValueToAmount = item.CharValueToAmount;
                            currentCharValue.LastUpdate = DateTime.Now;
                            CharValueRepository.Update(currentCharValue);
                        }
                    }
                }
            });
            return data;
        }
        #endregion
    }
}
