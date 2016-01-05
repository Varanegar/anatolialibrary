using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CityRegionConfig : EntityTypeConfiguration<CityRegion>
    {
        public CityRegionConfig()
        {
            this.HasMany<Customer>(pp => pp.CustomerInfos)
                .WithOptional(p => p.RegionInfo)
                .HasForeignKey( p => p.RegionInfoId);

            this.HasMany<Customer>(pp => pp.CustomerInfos)
                .WithOptional(p => p.RegionLevel1)
                .HasForeignKey(P => P.RegionLevel1Id);

            this.HasMany<Customer>(pp => pp.CustomerInfos)
                .WithOptional(p => p.RegionLevel2)
                .HasForeignKey(P => P.RegionLevel2Id);

            this.HasMany<Customer>(pp => pp.CustomerInfos)
                .WithOptional(p => p.RegionLevel3)
                .HasForeignKey(P => P.RegionLevel3Id);

            this.HasMany<Customer>(pp => pp.CustomerInfos)
                .WithOptional(p => p.RegionLevel4)
                .HasForeignKey(P => P.RegionLevel4Id);

            this.HasMany<IncompletePurchaseOrder>(cr => cr.IncompletePurchaseOrders)
               .WithRequired(svr => svr.CityRegion);

        }
    }
}