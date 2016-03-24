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
using System.Data.Entity;

namespace Anatoli.Business.Domain
{
    public class CharGroupDomain : BusinessDomainV2<CharGroup, CharGroupViewModel, CharGroupRepository, ICharGroupRepository>, IBusinessDomainV2<CharGroup, CharGroupViewModel>
    {
        #region Properties
        public IRepository<CharType> CharTypeRepository { get; set; }
        #endregion

        #region Ctors
        public CharGroupDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public CharGroupDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            CharTypeRepository = new CharTypeRepository(dbc);
        } 
        #endregion

        #region Methods
        public override async Task PublishAsync(List<CharGroup> charGroups)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                
                var currentGroups = MainRepository.GetQuery().Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey).ToList();
                var currentTypes = CharTypeRepository.GetQuery().Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey).ToList();

                foreach(CharGroup item in charGroups)
                {
                    var currentGroup = currentGroups.Find(p => p.Id == item.Id);
                    if (currentGroup != null)
                    {
                        currentGroup.CharGroupCode = item.CharGroupCode;
                        currentGroup.CharGroupName = item.CharGroupName;
                        currentGroup.LastUpdate = DateTime.Now;
                        currentGroup = SetCharTypeData(item, item.CharTypes.ToList(), currentTypes);
                    }
                    else
                    {
                        item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        var currentCharGroup = SetCharTypeData(item, item.CharTypes.ToList(), currentTypes);
                        await MainRepository.AddAsync(currentCharGroup);
                    }
                };
                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                Logger.Info("PublishAsync Finish" + charGroups.Count);
            }
        }


        public CharGroup SetCharTypeData(CharGroup data, List<CharType> charTypes, List<CharType> currentCharTypes)
        {
            data.CharTypes.Clear();
            foreach(CharType item in charTypes)
            {
                var charType = currentCharTypes.Find(p => p.Id == item.Id);
                if (charType != null)
                {
                    data.CharTypes.Add(charType);
                }
            }
            return data;
        }

        #endregion
    }
}