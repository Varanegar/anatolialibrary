using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Anatoli.Business.Domain
{
    public class LoyaltyProgramRuleDomain : BusinessDomainV3<LoyaltyProgramRule>, IBusinessDomainV3<LoyaltyProgramRule>
    {
        #region Ctors
        public LoyaltyProgramRuleDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyProgramRuleDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyProgramRule currentLoyaltyProgramRule, LoyaltyProgramRule item)
        {
            if (currentLoyaltyProgramRule != null)
            {
                if (currentLoyaltyProgramRule.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyProgramRule.LastUpdate = DateTime.Now;
                    currentLoyaltyProgramRule.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyProgramRule);
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
