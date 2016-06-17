using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Anatoli.Business.Domain
{
    public class LoyaltyRuleTypeDomain : BusinessDomainV3<LoyaltyRuleType>, IBusinessDomainV3<LoyaltyRuleType>
    {
        #region Ctors
        public LoyaltyRuleTypeDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyRuleTypeDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyRuleType currentLoyaltyRuleType, LoyaltyRuleType item)
        {
            if (currentLoyaltyRuleType != null)
            {
                if (currentLoyaltyRuleType.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyRuleType.LastUpdate = DateTime.Now;
                    currentLoyaltyRuleType.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyRuleType);
                }
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        public override void SetConditionForFetchingData()
        {
            MainRepository.ExtraPredicate = p => true;
        }
        #endregion
    }
}
