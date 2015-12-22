using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class CharGroupProxy : AnatoliProxy<CharGroup, CharGroupViewModel>, IAnatoliProxy<CharGroup, CharGroupViewModel>
    {
        public IAnatoliProxy<CharType, CharTypeViewModel> CharTypeProxy { get; set; }
               
        #region Ctors
        public CharGroupProxy() :
            this(AnatoliProxy<CharType, CharTypeViewModel>.Create()
            )
        { }

        public CharGroupProxy(IAnatoliProxy<CharType, CharTypeViewModel> charTypeProxy
            )
        {
            CharTypeProxy = charTypeProxy;
        }
        #endregion
        public override CharGroupViewModel Convert(CharGroup data)
        {
            return new CharGroupViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateLabelKey = data.PrivateLabelOwner.Id,
                CharGroupCode = data.CharGroupCode,
                CharGroupName = data.CharGroupName,

                CharTypes = CharTypeProxy.Convert(data.CharTypes.ToList()),
            };
        }

        public override CharGroup ReverseConvert(CharGroupViewModel data)
        {
            return new CharGroup
            {
                Number_ID = data.ID,
                Id = data.UniqueId,

                CharGroupCode = data.CharGroupCode,
                CharGroupName = data.CharGroupName,

                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },

                CharTypes = CharTypeProxy.ReverseConvert(data.CharTypes.ToList()),
            };
        }
    }
}