namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Stock : AnatoliBaseModel
    {
        public int StockCode { get; set; }
        [StringLength(100)]
        public string StockName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [ForeignKey("Store")]
        public Nullable<Guid> StoreId { get; set; }
        [ForeignKey("CompanyCenter")]
        public Nullable<Guid> CompanyCenterId { get; set; }
        [ForeignKey("Accept1By")]
        public Nullable<Guid> Accept1ById { get; set; }
        [ForeignKey("Accept2By")]
        public Nullable<Guid> Accept2ById { get; set; }
        [ForeignKey("Accept3By")]
        public Nullable<Guid> Accept3ById { get; set; }
        [ForeignKey("StockType")]
        public Nullable<Guid> StockTypeId { get; set; }
        [ForeignKey("MainSCMStock2")]
        public Nullable<Guid> MainSCMStock2Id { get; set; }
        [ForeignKey("RelatedSCMStock2")]
        public Nullable<Guid> RelatedSCMStock2Id { get; set; }
        public virtual Store Store { get; set; }
        public virtual CompanyCenter CompanyCenter { get; set; }
        public virtual Stock MainSCMStock2 { get; set; }
        public virtual Stock RelatedSCMStock2 { get; set; }
        public virtual ICollection<Stock> MainSCMStock1 { get; set; }
        public virtual ICollection<Stock> RelatedSCMStock1 { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        public virtual ICollection<StockOnHandSync> StockOnHandSyncs { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequestSourceStocks { get; set; }
        public virtual StockType StockType { get; set; }
        public virtual Principal Accept1By { get; set; }
        public virtual Principal Accept2By { get; set; }
        public virtual Principal Accept3By { get; set; }
        public virtual bool OverRequest { get; set; }
        public virtual bool OverAfterFirstAcceptance { get; set; }
        public virtual bool OverAfterSecondAcceptance { get; set; }
        public virtual bool OverAfterThirdAcceptance { get; set; }

        public virtual ICollection<PrincipalStock> Principals { get; set; }

        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
    