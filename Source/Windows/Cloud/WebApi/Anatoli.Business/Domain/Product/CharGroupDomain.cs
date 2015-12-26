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
        public IRepository<CharType> CharTypeRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CharGroupDomain() { }
        public CharGroupDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CharGroupDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CharGroupRepository(dbc), new CharTypeRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CharGroup, CharGroupViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CharGroupDomain(ICharGroupRepository CharGroupRepository, ICharTypeRepository charTypeRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CharGroup, CharGroupViewModel> proxy)
        {
            Proxy = proxy;
            Repository = CharGroupRepository;
            CharTypeRepository = charTypeRepository;
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
            try
            {
                var charGroups = Proxy.ReverseConvert(CharGroupViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                await DeleteAllGroups();
                foreach(CharGroup item in charGroups)
                {

                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                    var currentCharGroup = await SetCharTypeData(item, item.CharTypes.ToList(), Repository.DbContext);
                    await Repository.AddAsync(currentCharGroup);
                };
                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        private async Task DeleteAllGroups()
        {
            await Task.Factory.StartNew(() =>
            {
                Repository.DbContext.Database.ExecuteSqlCommand("delete from CharGroupTypes");
                Repository.DbContext.Database.ExecuteSqlCommand("delete from CharGroups");
            });
        }

        public async Task<CharGroup> SetCharTypeData(CharGroup data, List<CharType> charTypes, AnatoliDbContext context)
        {
            data.CharTypes.Clear();
            foreach(CharType item in charTypes)
            {
                var charType = await CharTypeRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefaultAsync();
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