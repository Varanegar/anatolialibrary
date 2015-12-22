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
    public class CharGroupDomain : IBusinessDomain<CharGroup, CharGroupViewModel>
    {
        #region Properties
        public IAnatoliProxy<CharGroup, CharGroupViewModel> Proxy { get; set; }
        public IRepository<CharGroup> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CharGroupDomain() { }
        public CharGroupDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CharGroupDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CharGroupRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CharGroup, CharGroupViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CharGroupDomain(ICharGroupRepository CharGroupRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CharGroup, CharGroupViewModel> proxy)
        {
            Proxy = proxy;
            Repository = CharGroupRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CharGroupViewModel>> GetAll()
        {
            var charGroups = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(charGroups.ToList()); ;
        }

        public async Task<List<CharGroupViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var charGroups = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(charGroups.ToList()); ;
        }

        public async Task PublishAsync(List<CharGroupViewModel> CharGroupViewModels)
        {
            var charGroups = Proxy.ReverseConvert(CharGroupViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            charGroups.ForEach(item =>
            {

                item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                var currentGroup = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (currentGroup != null)
                {
                    currentGroup.CharGroupCode = item.CharGroupCode;
                    currentGroup.CharGroupName = item.CharGroupName;
                    
                    Repository.UpdateAsync(SetCharTypeData(currentGroup, item.CharTypes.ToList(), Repository.DbContext));
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    item.CreatedDate = item.LastUpdate = DateTime.Now;

                    Repository.AddAsync(SetCharTypeData(item, item.CharTypes.ToList(), Repository.DbContext));
                }
            });
            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<CharGroupViewModel> CharGroupViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var charGroups = Proxy.ReverseConvert(CharGroupViewModels);

                charGroups.ForEach(item =>
                {
                    var charGroup = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                    Repository.DeleteAsync(charGroup);
                });

                Repository.SaveChangesAsync();
            });
        }

        public CharGroup SetCharTypeData(CharGroup data, List<CharType> charTypes, AnatoliDbContext context)
        {
            CharTypeDomain charTypeDomain = new CharTypeDomain(data.PrivateLabelOwner.Id, context);
            data.CharTypes.Clear();
            charTypes.ForEach(item =>
            {
                var charType = charTypeDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (charType != null)
                    data.CharTypes.Add(charType);
            });
            return data;
        }
        #endregion
    }
}
