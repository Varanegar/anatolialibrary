﻿using System;
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

namespace Anatoli.Business.Domain
{
    public class CharTypeDomain : IBusinessDomain<CharType, CharTypeViewModel>
    {
        #region Properties
        public IAnatoliProxy<CharType, CharTypeViewModel> Proxy { get; set; }
        public IRepository<CharType> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CharTypeDomain() { }
        public CharTypeDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CharTypeDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CharTypeRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CharType, CharTypeViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CharTypeDomain(ICharTypeRepository CharTypeRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CharType, CharTypeViewModel> proxy)
        {
            Proxy = proxy;
            Repository = CharTypeRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CharTypeViewModel>> GetAll()
        {
            var charTypes = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(charTypes.ToList()); ;
        }

        public async Task<List<CharTypeViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var charTypes = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(charTypes.ToList()); ;
        }

        public async Task PublishAsync(List<CharTypeViewModel> CharTypeViewModels)
        {
            var charTypes = Proxy.ReverseConvert(CharTypeViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            charTypes.ForEach(item =>
            {
                item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                var currentType = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (currentType != null)
                {
                    currentType.CharTypeDesc = item.CharTypeDesc;
                    currentType.DefaultCharValueGuid = item.DefaultCharValueGuid;

                    Repository.UpdateAsync(SetCharValueData(currentType, item.CharValues.ToList(), Repository.DbContext));
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    item.CreatedDate = item.LastUpdate = DateTime.Now;

                    Repository.AddAsync(SetCharValueData(item, item.CharValues.ToList(), Repository.DbContext));
                }
            });
            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<CharTypeViewModel> CharTypeViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var charTypes = Proxy.ReverseConvert(CharTypeViewModels);

                charTypes.ForEach(item =>
                {
                    var charType = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                    Repository.DeleteAsync(charType);
                });

                Repository.SaveChangesAsync();
            });
        }

        public CharType SetCharValueData(CharType data, List<CharValue> charValues, AnatoliDbContext context)
        {
            CharValueDomain charTypeDomain = new CharValueDomain(data.PrivateLabelOwner.Id, context);
            data.CharValues.Clear();
            charValues.ForEach(item =>
            {
                var charType = charTypeDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (charType != null)
                    data.CharValues.Add(charType);
            });
            return data;
        }
        #endregion
    }
}