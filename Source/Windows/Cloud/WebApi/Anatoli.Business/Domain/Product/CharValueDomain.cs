using System;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class CharValueDomain : BusinessDomainV2<CharValue, CharValueViewModel, CharValueRepository, ICharValueRepository>, IBusinessDomainV2<CharValue, CharValueViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public CharValueDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public CharValueDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(CharValue currentCharValue, CharValue item)
        {
            if (currentCharValue != null)
            {
                if (currentCharValue.CharValueText != item.CharValueText ||
                        currentCharValue.CharValueFromAmount != item.CharValueFromAmount ||
                        currentCharValue.CharValueToAmount != item.CharValueToAmount
                    )
                {
                    currentCharValue.CharValueText = item.CharValueText;
                    currentCharValue.CharValueFromAmount = item.CharValueFromAmount;
                    currentCharValue.CharValueToAmount = item.CharValueToAmount;
                    currentCharValue.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentCharValue);
                }
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        #endregion
    }
}
