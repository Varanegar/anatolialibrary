﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.ViewModels.Order
{
    public class RastakSellViewModel : RastakBaseViewModel
    {
        public int SellId { get; set; }
        [Ignore]
        public int InvoiceNo { get; set; }
        [Ignore]
        public string InvoiceDate { get; set; }
        public int StockId { get; set; }
        public int SellTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public int SLId { get; set; }
        public int PayTypeId { get; set; }
        public string Comment { get; set; }
        public decimal NetAmount { get; set; }
        public decimal ManualDiscount { get; set; }
        public decimal Freight { get; set; }
        public decimal PayableAmount { get; set; }
        public int FiscalYearId { get; set; }
        public string InvoiceDateTime { get; set; }
        public bool IsCanceled { get; set; }
        public decimal SettlementAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public int CenterId { get; set; }
        public decimal DiscountAmount2 { get; set; }
        public long RequestNo { get; set; }
        public string RequestDateTime { get; set; }
        public int SellCategoryId { get; set; }
        public bool IsConfirmed { get; set; }
        [Ignore]
        public string SettlementDate { get; set; }
        [Ignore]
        public string DeliveryNo { get; set; }
        [Ignore]
        public string DeliveryDate { get; set; }
        [Ignore]
        public int? DeliveryStatusId { get; set; }
        public int SellStatusId { get; set; }
        public int CashSessionId { get; set; }
        public int CashSessionStatusId { get; set; }
        public decimal FreightCustomerAmount { get; set; }
        [Ignore]
        public List<RastakSellDetailViewModel> SellDetail { get; set; }
    }
}
