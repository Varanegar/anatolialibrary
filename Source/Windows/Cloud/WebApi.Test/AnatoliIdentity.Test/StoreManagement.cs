using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StoreModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Thunderstruck;

namespace ClientApp
{
    public static class StoreManagement
    {
        public static void UploadStoreDataToServer(HttpClient client, string servserURI)
        {
            var storeInfo = GetStoreInfo();

            //obj.Baskets.RemoveAt(1);
            string data = new JavaScriptSerializer().Serialize(storeInfo);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result8 = client.PostAsync(servserURI + "/api/gateway/customer/save?privateOwnerId=3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C", content).Result;
            var json8 = result8.Content.ReadAsStringAsync().Result;
            var obj2 = new { message = "", ModelState = new Dictionary<string, string[]>() };
            var x = JsonConvert.DeserializeAnonymousType(json8, obj2);
        }

        public static List<StoreViewModel> GetStoreInfo()
        {
            List<StoreViewModel> storeList = new List<StoreViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<StoreViewModel>("SELECT CenterId as ID, CenterId CenterId, CenterCode as StoreCode, CenterName as StoreName, Address, 0 as Lat, 0 as Lng, 0 as Hasdelivery, 1 as HasCourier, 1 as SupportAppOrder, 1 as SupportWebOrder, 0 as SupportCallCenterOrder FROM Center where centertypeid=3 order by centerId");
                storeList = data.ToList();
                storeList.ForEach(item =>
                    {
                        var ts = TimeSpan.ParseExact("0:0", @"h\:m",
                             CultureInfo.InvariantCulture);

                        var storeCalendar = context.All<StoreCalendarViewModel>("select DayTimeWorkingId as ID, WorkingDate as PDate, dbo.ToMiladi(WorkingDate), FromHour as FromTimeString, ToHour as ToTimeString from DayTimeWorking where CenterId = " + item.CenterId);
                        item.StoreCalendar = storeCalendar.ToList();

                        var storeValidRegion = context.All<CityRegionViewModel>(@"select DeliveryRegionTreeId as ID, CityRegionID as UniqueIdString from (
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=CityId and  center.CenterId =  CenterDeliveryRegion.CenterId and RegionId is null and AreaId is null
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=RegionId and  center.CenterId =  CenterDeliveryRegion.CenterId and  RegionId is not null and AreaId is null
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=AreaId and  center.CenterId =  CenterDeliveryRegion.CenterId and  AreaId is not null
                                ) as tt where CenterId =" + item.CenterId);
                        item.StoreValidRegionInfo = storeValidRegion.ToList();
                    });
            }

            return storeList;
        }

        public static List<StoreActiveOnhandViewModel> GetStoreActiveOnhand()
        {
            List<StoreActiveOnhandViewModel> storeOnhandList = new List<StoreActiveOnhandViewModel>();
            using(var context = new DataContext())
            {
                var fiscalYear = context.First<int>(@"select FiscalYearId from FiscalYear where StartDate <= dbo.ToShamsi(getdate()) and enddate>= dbo.ToShamsi(getdate())");
                var data = context.All<StoreActiveOnhandViewModel>(@"select Product.UniqueId as ProductGuid, Qty, Center.UniqueId as StoreGuid from (      
                      SELECT  ProductId,sum(Qty) as  Qty,CenterId    
                      FROM         
                      (        
                        SELECT iv.FiscalYearId,iv.StockId,id.ProductId ,   
                       (CASE WHEN vt.IsInput=1 THEN 1 ELSE -1 END * id.Qty) Qty          
                       ,iv.CenterId    
                        FROM InvVoucher iv           
                       INNER JOIN InvVoucherDetail id ON iv.InvVoucherId=id.InvVoucherId          
                       INNER JOIN InvVoucherType vt ON vt.InvVoucherTypeId=iv.InvVoucherTypeId         
                       where fiscalYearId = 10       
                      UNION ALL          
             
                      SELECT s.FiscalYearId,s.StockId,sd.ProductId     
                       ,-SUM(Qty) Qty ,   CenterId    
                      FROM SellM s           
                       INNER JOIN SellDetail sd ON s.SellId=sd.SellId          
                      WHERE s.IsCanceled=0       
                       AND s.InvVoucherId IS NULL      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,s.CenterId ,s.StockId,sd.ProductId,s.InvoiceDate     
             
                
                      UNION ALL        
             
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty)*(-1) AS Qty ,         
                        s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =5     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
      
                      UNION ALL     
                      SELECT s.FiscalYearId,srd.StockId,srd.ProductId     
                        ,SUM(Qty) AS Qty ,    s.CenterId    
                      FROM  SalesReceipt s        
                       INNER JOIN SalesReceiptDetail srd ON s.SalesReceiptId=srd.SalesReceiptId        
                      WHERE srd.InvVoucherDetailId IS NULL      
                       AND s.SalesReceiptStatusId =4     
                       AND s.IsCanceled=0        
                       AND srd.IsRemoved=0      
                       and fiscalYearId = " + fiscalYear + @" 
                      GROUP BY s.FiscalYearId ,srd.StockId ,srd.ProductId ,s.CenterId   
                      ) A 
                    group by FiscalYearId, ProductId,CenterId
                    ) as onhand, Product, Center where onhand.ProductId = Product.ProductId and Center.centerid = onhand.CenterId");

                storeOnhandList = data.ToList();
            }

            return storeOnhandList;
        }

        public static List<StoreActivePriceListViewModel> GetStorePriceList()
        {
            List<StoreActivePriceListViewModel> storeOnhandList = new List<StoreActivePriceListViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<StoreActivePriceListViewModel>(@"IF OBJECT_ID('tempdb..#CenterPrice') IS NOT NULL drop table  #CenterPrice
                        select ROW_NUMBER() over (order by p.ProductId) as rowNo, p.ProductId, c.CenterId, c.UniqueId as StoreGuidString,  p.SellPrice as price, Product.UniqueId as ProductGuidString 
	                        into #CenterPrice
	                        from Price p, Center c, Product 
	                        where p.CenterId is null and p.CustomerId is null and p.CustomerGroupId is null and p.CenterGroupId is null and product.ProductId = p.ProductId
		                        and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 

                        delete #CenterPrice where rowNo in (
	                        select rowNo from price p, #CenterPrice 
		                        where p.CenterId = #CenterPrice.CenterId and p.ProductId = #CenterPrice.ProductId 
			                        and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 
			                        and p.CustomerId is null and p.CustomerGroupId is null and p.CenterGroupId is null 
	                        )

                        insert into #CenterPrice
	                        select 1, p.ProductId, p.CenterId, c.UniqueId as StoreGuidString,  p.SellPrice as price, Product.UniqueId as ProductGuidString 
		                        from Price p, Center c, Product
			                         where p.CenterId is not null and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 
				                        and p.CenterId = c.CenterId
				                        and p.ProductId = Product.ProductId
				                        and p.CustomerId is null and CustomerGroupId is null and p.CenterGroupId is null 

                        select price, StoreGuidString, ProductGuidString from #CenterPrice
                        ");

                storeOnhandList = data.ToList();
            }

            return storeOnhandList;
        }

    }
}
