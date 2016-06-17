using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Anatoli.Business.Domain
{
    public class LoyaltyTriggerTypeDomain : BusinessDomainV3<LoyaltyTriggerType>, IBusinessDomainV3<LoyaltyTriggerType>
    {
        #region Ctors
        public LoyaltyTriggerTypeDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyTriggerTypeDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyTriggerType currentLoyaltyTriggerType, LoyaltyTriggerType item)
        {
            if (currentLoyaltyTriggerType != null)
            {
                if (currentLoyaltyTriggerType.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyTriggerType.LastUpdate = DateTime.Now;
                    currentLoyaltyTriggerType.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyTriggerType);
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
