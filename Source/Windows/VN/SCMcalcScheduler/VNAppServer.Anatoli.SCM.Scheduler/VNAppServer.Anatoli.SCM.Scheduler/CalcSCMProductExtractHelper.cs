using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Anatoli.SCM.Scheduler
{
    public class CalcSCMProductExtractHelper
    {
        private StockViewModel StockData;
        private List<StockProductRequestRuleViewModel> Rules;
        private List<ProductViewModel> Products;
        private List<MainProductGroupViewModel> ProductGroups;
        private Guid RequestTypeId;
        private List<StockProductRequestProductViewModel> RequestedProducts { get; set; }
        public List<StockProductRequestViewModel> StockRequests { get; private set; }

        public CalcSCMProductExtractHelper(StockViewModel stockData, List<StockProductRequestRuleViewModel> rules,
                List<ProductViewModel> products, List<MainProductGroupViewModel> productGroups, Guid stockRequestTypeId)
        {
            this.StockData = stockData;
            this.Rules = rules;
            this.Products = products;
            this.ProductGroups = productGroups;
            this.RequestTypeId = stockRequestTypeId;

            StockRequests = new List<StockProductRequestViewModel>();
            RequestedProducts = new List<StockProductRequestProductViewModel>();
        }

        //ابتدا لیست کالاها را بر اساس قوانین و نقطه سفارش استخراج می کند و سپس بر اساس نوع محاسبه مرتبط با کالا کالاهای مرتبط را مشخص می کند 
        // این حقله تا زمانی که کلیه کالاهای مرتبط استخراج شوند ادامه خواهد یافت
        public async Task ExtractProducts()
        {
            int counter = 0;
            await ExtractValidProducts();

            while (RequestedProducts.FindAll(f => !f.IsRelatedExtracted).Count > 0)
            {
                var notProcessedItems = RequestedProducts.FindAll(f => !f.IsRelatedExtracted);
                
                notProcessedItems.ForEach(item =>
                    {
                        //برای جلوگیری برای بینهایت این شرط گذاشته شده است
                        if (counter > 100000) throw new Exception("تعداد دفعات پردازش بیش از تعداد مجاز می باشد");

                        var mainItemAddDetail = item.StockProductRequestProductDetails.Find(f => f.IsMainRule == true);
                        //در صورتی که نوع پردازش صرفا حود کالا باشد کار خاصی انجام نشده و صرفا شرط پردازش تایید می شود و در صورتی که از نوع تامین کننده یا 
                        //گروه کالا باشد کالاهای مرتبط استخراج به لیست افزوده می شوند در چرخه بعدی مجددا محاسبه خواهند شد
                        item.IsRelatedExtracted = true;
                        if (item.ReorderCalcTypeId == ReorderCalcTypeViewModel.CalcProductAndItsGroup)
                            AddGroupProducts(item.ProductId, mainItemAddDetail.StockProductRequestRuleId, mainItemAddDetail.RuleDesc + " به صورت غیر مستقیم بر اساس گروه کالا", false);
                        else if (item.ReorderCalcTypeId == ReorderCalcTypeViewModel.CalcProductAndItsSupplier)
                            AddSupplierProducts(item.ProductId, mainItemAddDetail.StockProductRequestRuleId, mainItemAddDetail.RuleDesc + " به صورت غیر مستقیم بر اساس تامین کننده کالا", false);
                    });
            }

            Rules.ForEach(item =>
                {
                    if (item.RuleTypeId == StockProductRequestRuleTypeViewModel.AddToRequest)
                    {
                        AddWithRelatedRuleProducts(item, true, item.Qty);
                    }
                });
        }
        
        //پس استخراج کالاهایی که باید برای انها درخواست ثبت شود بر اساس اطلاعات کالاها درخواست تولید می شود
        public async Task GenerateStockRequests()
        {
            foreach(var item in RequestedProducts)
            {
                if (item.StockProductRequestSupplyTypeId == StockProductRequestSupplyTypeViewModel.SupplyFromMainStock)
                    await AddItemByStockToStockRequest(item, StockProductRequestSupplyTypeViewModel.SupplyFromMainStock);
                else if (item.StockProductRequestSupplyTypeId == StockProductRequestSupplyTypeViewModel.SupplyFromMainStock)
                    await AddItemByStockToStockRequest(item, StockProductRequestSupplyTypeViewModel.SupplyFromRelatedStock);
                else if (item.StockProductRequestSupplyTypeId == StockProductRequestSupplyTypeViewModel.SupplyFromSupplier)
                    await AddItemBySupplierToStockRequest(item, StockProductRequestSupplyTypeViewModel.SupplyFromRelatedStock);
            }
        }

        private async Task AddItemBySupplierToStockRequest(StockProductRequestProductViewModel requestedProduct, Guid stockSupplyTypeId)
        {
            await Task.Factory.StartNew(() =>
            {

                bool isNew = false;
                var currentRequest = StockRequests.Find(f => f.StockProductRequestSupplyTypeId == stockSupplyTypeId &&
                                                            f.ProductTypeId == requestedProduct.ProductTypeId && 
                                                            f.SupplierId == requestedProduct.SupplierGuid);
                if (currentRequest == null)
                {
                    isNew = true;
                    currentRequest = new StockProductRequestViewModel()
                    {
                        ProductTypeId = requestedProduct.ProductTypeId??ProductTypeViewModel.NormalRequestProducts,
                        RequestDate = DateTime.Now,
                        StockId = StockData.UniqueId,
                        StockOnHandSyncId = StockData.LatestStockOnHandSyncId,
                        StockProductRequestStatusId = StockProductRequestStatusViewModel.WaitForStoreManagerAcceptance,
                        StockProductRequestSupplyTypeId = stockSupplyTypeId,
                        StockProductRequestTypeId = RequestTypeId,
                        StockTypeId = StockData.StockTypeId??StockTypeViewModel.StoreStock,
                        SupplierId = (requestedProduct.SupplierGuid == null) ? Guid.Empty : requestedProduct.SupplierGuid,
                        SupplyByStockId = (StockData.RelatedSCMStockId==null)?Guid.Empty:(Guid)StockData.RelatedSCMStockId,
                        UniqueId = Guid.NewGuid(),
                    };
                    currentRequest.StockProductRequestProducts = new List<StockProductRequestProductViewModel>();
                }
                requestedProduct.StockProductRequestId = currentRequest.UniqueId;
                currentRequest.StockProductRequestProducts.Add(requestedProduct);
                if (isNew) StockRequests.Add(currentRequest);
            });
        }

        private async Task AddItemByStockToStockRequest(StockProductRequestProductViewModel requestedProduct, Guid stockSupplyTypeId)
        {
            await Task.Factory.StartNew(() =>
                {

                    bool isNew = false;
                    var currentRequest = StockRequests.Find(f => f.StockProductRequestSupplyTypeId == stockSupplyTypeId &&
                                                                f.ProductTypeId == requestedProduct.ProductTypeId);
                    if (currentRequest == null)
                    {
                        isNew = true;
                        currentRequest = new StockProductRequestViewModel()
                        {
                            ProductTypeId = (Guid)requestedProduct.ProductTypeId,
                            RequestDate = DateTime.Now,
                            StockId = StockData.UniqueId,
                            StockOnHandSyncId = StockData.LatestStockOnHandSyncId,
                            StockProductRequestStatusId = StockProductRequestStatusViewModel.WaitForStoreManagerAcceptance,
                            StockProductRequestSupplyTypeId = stockSupplyTypeId,
                            StockProductRequestTypeId = RequestTypeId,
                            StockTypeId = (Guid)StockData.StockTypeId,
                            SupplierId = null,
                            SupplyByStockId = (stockSupplyTypeId == StockProductRequestSupplyTypeViewModel.SupplyFromRelatedStock) ? (Guid)StockData.RelatedSCMStockId : (Guid)StockData.MainSCMStockId,
                            UniqueId = Guid.NewGuid(),
                        };
                        currentRequest.StockProductRequestProducts = new List<StockProductRequestProductViewModel>();

                    }

                    currentRequest.StockProductRequestProducts.Add(requestedProduct);
                    if (isNew) StockRequests.Add(currentRequest);
                });
        }

        //بر اساس لیست کالا های انبار و قوانین تعریف شده ای که مبتنی بر نقطه سفارش کار می کنند لیست کالاهایی که باید در محاسبه شرکت کنند مشخص می شوند
        private async Task ExtractValidProducts()
        {
            await Task.Factory.StartNew(() =>
                {
                    var defaultRule = StockProductRequestRuleViewModel.ReorderRule;
                    //برای کلیه کالاهای تعریف شده در انیار بر اساس اینکه کالا نقطه سفارش داشته باشد به لیست افزوده می شود
                    StockData.StockProduct.ForEach(item =>{ 
                        if(item.MaxQty > 0 )
                            AddProductByReorderLevel(item.ProductGuid,defaultRule.UniqueId, defaultRule.StockProductRequestRuleName, true ); 
                    });

                    //قوانینی که بر اساس افزودن به درخواست نیستند و قانون پیش فرض سیستم نیز نمی باشد و نوع ان افزودن بر اساس کالاهای مرتبط باشد
                    Rules.ForEach(rule =>
                    {
                        if (rule.UniqueId != StockProductRequestRuleViewModel.ReorderRule.UniqueId && 
                            rule.RuleTypeId != StockProductRequestRuleTypeViewModel.AddToRequest &&
                            rule.RuleTypeId == StockProductRequestRuleTypeViewModel.BasedOnRerderLevelWithRelated)
                            AddWithRelatedRuleProducts(rule, true);
                    });
                });
        }
        //بر اساس قانونی اقدام می کند
        private void AddWithRelatedRuleProducts(StockProductRequestRuleViewModel rule, bool isMainRule, decimal forceQty=0)
        {
            if (rule.ReorderCalcTypeId == ReorderCalcTypeViewModel.CalcProductOnly && rule.ProductId != null)
                AddProductByReorderLevel((Guid)rule.ProductId, rule.UniqueId, rule.StockProductRequestRuleName, isMainRule, forceQty);
            else if (rule.ReorderCalcTypeId == ReorderCalcTypeViewModel.CalcProductAndItsGroup && rule.MainProductGroupId != null)
                AddGroupProducts((Guid)rule.MainProductGroupId, rule.UniqueId, rule.StockProductRequestRuleName, isMainRule, forceQty);
            else if (rule.ReorderCalcTypeId == ReorderCalcTypeViewModel.CalcProductOnly && rule.SupplierId != null)
                AddSupplierProducts((Guid)rule.SupplierId, rule.UniqueId, rule.StockProductRequestRuleName, isMainRule, forceQty);

        }

        //لیست کالاهای گروه را استخراج و برای افزودن به لیست اقدام می شود
        private void AddSupplierProducts(Guid supplierId, Guid ruleId, string ruleDesc, bool isMainRule = false, decimal forceQty = 0)
        {
            var result = new List<StockProductRequestProductViewModel>();
            var supplierProducts = Products.FindAll(p => p.MainSupplierId == supplierId.ToString());

            supplierProducts.ForEach(item =>
            {
                AddProductByReorderLevel(item, ruleId, ruleDesc, isMainRule, forceQty);
            });
        }
        //لیست کالاهای گروه را استخراج و برای افزودن به لیست اقدام می شود
        private void AddGroupProducts(Guid productGroupId, Guid ruleId, string ruleDesc, bool isMainRule = false, decimal forceQty = 0)
        {   
            var result = new List<StockProductRequestProductViewModel>();
            var currentGroup = ProductGroups.Find(p => p.UniqueId == productGroupId);
            var groups = ProductGroups.FindAll(p => p.ID >= currentGroup.NLeft && p.ID <= currentGroup.NRight);

            groups.ForEach(item =>
            {
                var groupProducts = Products.FindAll(p => p.MainProductGroupIdString == item.UniqueId.ToString());
                groupProducts.ForEach(productItem =>
                {
                    AddProductByReorderLevel(productItem, ruleId, ruleDesc, isMainRule, forceQty);
                });
            });
        }
        //گالا را شناسایی و بر اساس کالا اقدام می شود
        private void AddProductByReorderLevel(Guid productGuid, Guid ruleId, string ruleDesc, bool isMainRule = false, decimal forceQty = 0)
        {
            AddProductByReorderLevel(Products.Find(f => f.UniqueId == productGuid), ruleId, ruleDesc, isMainRule, forceQty);
        } 

        //در صورت وجود کالا در لیست کالاهی انبار به لیست اضافه می شود
        private void AddProductByReorderLevel(ProductViewModel product, Guid ruleId, string ruleDesc, bool isMainRule = false, decimal forceQty = 0)
        {
            if (StockData.StockActiveOnHand == null) return;
            var stockProduct = StockData.StockProduct.Find(p => p.ProductGuid == product.UniqueId);
            if (stockProduct != null)
            {
                AddStockProductByReorderLevel(stockProduct, product.ProductTypeId??ProductTypeViewModel.NormalRequestProducts ,
                    (product.MainSupplierId == null)?Guid.Empty:Guid.Parse(product.MainSupplierId), 
                    (product.MainProductGroupIdString == null)?Guid.Empty: Guid.Parse(product.MainProductGroupIdString),
                    (product.ManufactureIdString == null)?Guid.Empty:Guid.Parse(product.ManufactureIdString), 
                    ruleId, ruleDesc, isMainRule, forceQty);
            }
        }

        //پس از کنترل نبودن کالا در لیست با استخراج موجودی به لیست اضافه می شود
        private void AddStockProductByReorderLevel(StockProductViewModel stockProduct, Guid productTypeGuid, Guid supplierGuid, Guid productGroupGuid,
            Guid manufactureGuid, Guid ruleId, string ruleDesc, bool isMainRule = true, decimal forceQty = 0)
        {
            if (forceQty == 0)
                AddStockProductByReorderLevelNotForced(stockProduct, productTypeGuid, supplierGuid, productGroupGuid, manufactureGuid, ruleId, ruleDesc, isMainRule);
            else
                AddStockProductByReorderLevelForced(stockProduct, productTypeGuid, supplierGuid, productGroupGuid, manufactureGuid, ruleId, ruleDesc, forceQty);
            
        }

        //در صورتی که قانون افزودن اجباری تعداد وجود نداشته باشد از این مدل استفاده می شود
        private void AddStockProductByReorderLevelNotForced(StockProductViewModel stockProduct, Guid productTypeGuid, Guid supplierGuid, Guid productGroupGuid,
            Guid manufactureGuid, Guid ruleId, string ruleDesc, bool isMainRule = true)
        {
            if (RequestedProducts.FindAll(s => s.ProductId == stockProduct.ProductGuid).Count > 0) return;

            Guid reorderCalcTypeId = stockProduct.ReorderCalcTypeId ?? ReorderCalcTypeViewModel.CalcProductOnly;

            var onHand = StockData.StockActiveOnHand.Find(p => p.ProductGuid == stockProduct.ProductGuid);
            //در صورتی که کالا در انبار موجودی داشته باشد بر اساس تفاوت موجودی و سقف مورد نیاز سفارش گذاشته می شود
            if (onHand != null)
            {
                if (stockProduct.MaxQty != 0 && stockProduct.ReorderLevel >= onHand.Qty && stockProduct.ReorderLevel < stockProduct.MaxQty)
                    RequestedProducts.Add(new StockProductRequestProductViewModel(stockProduct.ProductGuid, stockProduct.MaxQty - onHand.Qty, reorderCalcTypeId,
                            ruleId, ruleDesc, stockProduct.MaxQty - onHand.Qty, isMainRule, stockProduct.StockProductRequestSupplyTypeId, productTypeGuid, supplierGuid));
            }
            //در صورتی که کالا در انبار موجودی داشته باشد بر اساس سقف مورد نیاز سفارش گذاشته می شود
            else
            {
                if (stockProduct.MaxQty != 0 && stockProduct.ReorderLevel < stockProduct.MaxQty)
                {
                    RequestedProducts.Add(new StockProductRequestProductViewModel(stockProduct.ProductGuid, stockProduct.MaxQty, reorderCalcTypeId,
                            ruleId, ruleDesc, stockProduct.MaxQty, isMainRule, stockProduct.StockProductRequestSupplyTypeId, productTypeGuid, supplierGuid));
                }

            }
        }

        //در صورتی که قانون افزودن اجباری تعداد وجود داشته باشد از این مدل استفاده می شود
        private void AddStockProductByReorderLevelForced(StockProductViewModel stockProduct, Guid productTypeGuid, Guid supplierGuid, Guid productGroupGuid,
            Guid manufactureGuid, Guid ruleId, string ruleDesc, decimal forceQty)
        {
            if (RequestedProducts.FindAll(s => s.ProductId == stockProduct.ProductGuid).Count > 0)
            {
                var currentRequest = RequestedProducts.Find(s => s.ProductId == stockProduct.ProductGuid);
                currentRequest.RequestQty += forceQty;
                currentRequest.StockProductRequestProductDetails.Add(new StockProductRequestProductDetailViewModel(currentRequest.UniqueId, ruleId, ruleDesc, forceQty, false));
            }
            else
            {
                RequestedProducts.Add(new StockProductRequestProductViewModel(stockProduct.ProductGuid, forceQty, ReorderCalcTypeViewModel.CalcProductOnly,
                    ruleId, ruleDesc, forceQty, true, stockProduct.StockProductRequestSupplyTypeId, productTypeGuid, supplierGuid));

            }
        }
    }
}
