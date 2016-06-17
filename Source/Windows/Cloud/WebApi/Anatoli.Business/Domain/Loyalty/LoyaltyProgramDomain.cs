using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Anatoli.Business.Domain
{
    public class LoyaltyProgramDomain : BusinessDomainV3<LoyaltyProgram>, IBusinessDomainV3<LoyaltyProgram>
    {
        #region Ctors
        public LoyaltyProgramDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyProgramDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyProgram currentLoyaltyProgram, LoyaltyProgram item)
        {
            if (currentLoyaltyProgram != null)
            {
                if (currentLoyaltyProgram.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyProgram.LastUpdate = DateTime.Now;
                    currentLoyaltyProgram.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyProgram);
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
