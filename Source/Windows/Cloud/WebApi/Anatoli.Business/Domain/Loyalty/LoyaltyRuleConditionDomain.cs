using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Anatoli.Business.Domain
{
    public class LoyaltyRuleConditionDomain : BusinessDomainV3<LoyaltyRuleCondition>, IBusinessDomainV3<LoyaltyRuleCondition>
    {
        #region Ctors
        public LoyaltyRuleConditionDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyRuleConditionDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyRuleCondition currentLoyaltyRuleCondition, LoyaltyRuleCondition item)
        {
            if (currentLoyaltyRuleCondition != null)
            {
                if (currentLoyaltyRuleCondition.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyRuleCondition.LastUpdate = DateTime.Now;
                    currentLoyaltyRuleCondition.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyRuleCondition);
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
