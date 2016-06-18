using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class CharTypeDomain : BusinessDomainV2<CharType, CharTypeViewModel, CharTypeRepository, ICharTypeRepository>, IBusinessDomainV2<CharType, CharTypeViewModel>
    {
        #region Properties
        public IRepository<CharValue> CharValueRepository { get; set; }
        #endregion

        #region Ctors
        public CharTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public CharTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            CharValueRepository = new CharValueRepository(dbc);
        } 
        #endregion

        #region Methods
        protected override void AddDataToRepository(CharType currentType, CharType item)
        {
            if (currentType != null)
            {
                currentType.CharTypeDesc = item.CharTypeDesc;
                currentType.DefaultCharValueGuid = item.DefaultCharValueGuid;
                currentType.LastUpdate = DateTime.Now;
                currentType = SetCharValueData(currentType, item.CharValues.ToList(), ((AnatoliDbContext)DBContext));
                MainRepository.Update(currentType);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                item.CharValues.ToList().ForEach(itemDetail =>
                {
                    itemDetail.ApplicationOwnerId = item.ApplicationOwnerId;
                    itemDetail.CreatedDate = itemDetail.LastUpdate = item.CreatedDate;
                });
                MainRepository.Add(item);
            }
        }

        public CharType SetCharValueData(CharType data, List<CharValue> charValues, AnatoliDbContext context)
        {
            foreach (CharValue item in charValues)
            {
                var count = data.CharValues.ToList().Count(u => u.Id == item.Id);
                if (count == 0)
                {
                    item.CharTypeId = data.Id;
                    item.ApplicationOwnerId = data.ApplicationOwnerId;
                    item.CreatedDate = item.LastUpdate = data.CreatedDate;
                    CharValueRepository.Add(item);
                }
                else
                {
                    var currentCharValue = CharValueRepository.GetQuery().Where(p => p.Id == item.Id && p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey).FirstOrDefault();
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
            return data;
        }
        #endregion
    }
}
