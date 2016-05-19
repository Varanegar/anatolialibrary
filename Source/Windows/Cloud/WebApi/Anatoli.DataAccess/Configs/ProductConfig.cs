using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            /*
            this.HasOptional<ProductGroup>(pg => pg.ProductGroup)
                .WithMany(m => m.Products);
            */
            this.HasMany<ProductPicture>(pp => pp.ProductPictures)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(true);

            this.HasMany<StockProduct>(pp => pp.StockProducts)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<BasketItem>(pp => pp.BasketItems)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<IncompletePurchaseOrderLineItem>(pp => pp.IncompletePurchaseOrderLineItems)
                .WithOptional(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequestRule>(pp => pp.StockProductRequestRules)
                .WithOptional(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<StockProductRequestProduct>(pp => pp.StockProductRequestProducts)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<StockHistoryOnHand>(pp => pp.StockHistoryOnHands)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<StockActiveOnHand>(pp => pp.StockActiveOnHands)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<PurchaseOrderLineItem>(pr => pr.PurchaseOrderLineItems)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<PurchaseOrderLineItem>(pr2 => pr2.PurchaseOrderLineItems)
                .WithOptional(p => p.FinalProduct)
                .WillCascadeOnDelete(false);

            this.HasMany<ProductRate>(pr => pr.ProductRates)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<ProductComment>(pc => pc.ProductComments)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);


            this.HasMany<CharValue>(s => s.CharValues)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductId");
                    cs.MapRightKey("CharValueID");
                    cs.ToTable("ProductChars");
                });

            this.HasMany<Supplier>(s => s.Suppliers)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductId");
                    cs.MapRightKey("SupplierID");
                    cs.ToTable("ProductSuppliers");
                });

            this.HasMany<StoreActivePriceList>(sap => sap.StoreActivePriceLists)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

            this.HasMany<ProductTagValue>(sap => sap.ProductTagValues)
                .WithRequired(p => p.Product)
                .WillCascadeOnDelete(false);

        }
    }
}