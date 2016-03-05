using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class CharValueProxy : AnatoliProxy<CharValue, CharValueViewModel>, IAnatoliProxy<CharValue, CharValueViewModel>
    {
        public override CharValueViewModel Convert(CharValue data)
        {
            return new CharValueViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                IsRemoved = data.IsRemoved,
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                CharValueText = data.CharValueText,
                CharValueFromAmount = data.CharValueFromAmount,
                CharValueToAmount = data.CharValueToAmount,
            };
        }

        public override CharValue ReverseConvert(CharValueViewModel data)
        {
            return new CharValue
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,

                CharValueText = data.CharValueText,
                CharValueFromAmount = data.CharValueFromAmount,
                CharValueToAmount = data.CharValueToAmount,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
            };
        }
    }
}