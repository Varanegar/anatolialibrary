using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class ManufactureProxy : AnatoliProxy<Manufacture, ManufactureViewModel>, IAnatoliProxy<Manufacture, ManufactureViewModel>
    {
        public override ManufactureViewModel Convert(Manufacture data)
        {
            return new ManufactureViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateLabelKey = data.PrivateLabelOwner.Id,
                Name = data.Name
            };
        }

        public override Manufacture ReverseConvert(ManufactureViewModel data)
        {
            return new Manufacture
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                Name = data.Name,

                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },
            };
        }
    }
}