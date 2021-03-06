using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class CharTypeProxy : AnatoliProxy<CharType, CharTypeViewModel>, IAnatoliProxy<CharType, CharTypeViewModel>
    {
        public IAnatoliProxy<CharValue, CharValueViewModel> CharValueProxy { get; set; }

        #region Ctors
        public CharTypeProxy() :
            this(AnatoliProxy<CharValue, CharValueViewModel>.Create()
            )
        { }

        public CharTypeProxy(IAnatoliProxy<CharValue, CharValueViewModel> charValueProxy
            )
        {
            CharValueProxy = charValueProxy;
        }
        #endregion

        public override CharTypeViewModel Convert(CharType data)
        {
            return new CharTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                CharTypeDesc = data.CharTypeDesc,
                DefaultCharValueID = data.DefaultCharValueGuid,
                IsRemoved = data.IsRemoved,

                CharValues = CharValueProxy.Convert(data.CharValues.ToList()),
            };
        }

        public override CharType ReverseConvert(CharTypeViewModel data)
        {
            return new CharType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,

                CharTypeDesc = data.CharTypeDesc,
                DefaultCharValueGuid = data.DefaultCharValueID,

                ApplicationOwnerId = data.ApplicationOwnerId,

                CharValues = data.CharValues != null? CharValueProxy.ReverseConvert(data.CharValues.ToList()) : null,
            };
        }
    }
}