using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.Business.Domain
{
    public class LoyaltyCardBatchDomain : BusinessDomainV3<LoyaltyCardBatch>, IBusinessDomainV3<LoyaltyCardBatch>
    {
        #region Ctors
        public LoyaltyCardBatchDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyCardBatchDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyCardBatch currentLoyaltyCardBatch, LoyaltyCardBatch item)
        {
            if (currentLoyaltyCardBatch != null)
            {
                if (currentLoyaltyCardBatch.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyCardBatch.LastUpdate = DateTime.Now;
                    currentLoyaltyCardBatch.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyCardBatch);
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
