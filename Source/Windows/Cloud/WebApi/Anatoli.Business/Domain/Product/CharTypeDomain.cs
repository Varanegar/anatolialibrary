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
    public class CharTypeDomain : IBusinessDomain<CharType, CharTypeViewModel>
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

        public async Task PublishAsync(List<CharTypeViewModel> CharTypeViewModels)
        {
            try
            {
                var charTypes = Proxy.ReverseConvert(CharTypeViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (CharType item in charTypes)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentType = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentType != null)
                    {
                        currentType.CharTypeDesc = item.CharTypeDesc;
                        currentType.DefaultCharValueGuid = item.DefaultCharValueGuid;
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
                throw ex;
            }
        }

        public async Task Delete(List<CharTypeViewModel> CharTypeViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var charTypes = Proxy.ReverseConvert(CharTypeViewModels);

                charTypes.ForEach(item =>
                {
                    var charType = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                    Repository.DeleteAsync(charType);
                });

                Repository.SaveChangesAsync();
            });
        }

        public async Task<CharType> SetCharValueData(CharType data, List<CharValue> charValues, AnatoliDbContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                CharValueDomain charTypeDomain = new CharValueDomain(data.PrivateLabelOwner.Id, context);
                foreach (CharValue item in charValues)
                {
                    var count = data.CharValues.ToList().Count(u => u.Id == item.Id);
                    if (count == 0)
                    {
                        item.CharTypeId = data.Id;
                        item.PrivateLabelOwner = data.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = data.CreatedDate;
                        CharValueRepository.AddAsync(item);
                    }
                    else
                    {
                        var currentCharValue = CharValueRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                        currentCharValue.CharValueText = item.CharValueText;
                        currentCharValue.CharValueFromAmount = item.CharValueFromAmount;
                        currentCharValue.CharValueToAmount = item.CharValueToAmount;
                        CharValueRepository.UpdateAsync(currentCharValue);
                    }
                }
            });
            return data;
        }
        #endregion
    }
}
