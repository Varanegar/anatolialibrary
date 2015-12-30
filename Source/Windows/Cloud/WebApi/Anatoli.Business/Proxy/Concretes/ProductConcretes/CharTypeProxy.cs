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
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                CharTypeDesc = data.CharTypeDesc,
                DefaultCharValueID = data.DefaultCharValueGuid,

                CharValues = CharValueProxy.Convert(data.CharValues.ToList()),
            };
        }

        public override CharType ReverseConvert(CharTypeViewModel data)
        {
            return new CharType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,

                CharTypeDesc = data.CharTypeDesc,
                DefaultCharValueGuid = data.DefaultCharValueID,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                CharValues = data.CharValues != null? CharValueProxy.ReverseConvert(data.CharValues.ToList()) : null,
            };
        }
    }
}