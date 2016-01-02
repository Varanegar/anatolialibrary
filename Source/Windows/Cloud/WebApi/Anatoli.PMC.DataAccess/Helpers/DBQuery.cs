﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.DataAccess.Helpers
{
    public static class DBQuery
    {
        public static string GetStoreQuery()
        {
            return @"SELECT Convert(Uniqueidentifier, UniqueID) as UniqueID,  CenterId as ID, CenterId CenterId, CenterCode as StoreCode, CenterName as StoreName, Address, 0 as Lat, 0 as Lng, 0 as Hasdelivery, 1 as HasCourier, 1 as SupportAppOrder, 1 as SupportWebOrder, 0 as SupportCallCenterOrder FROM Center where centertypeid=3 ";
        }
        public static string GetStoreCalendarQuery(int storeId) 
        {
            return @"select Convert(uniqueidentifier, uniqueId) as uniqueId,  DayTimeWorkingId as ID, WorkingDate as PDate, dbo.ToMiladi(WorkingDate) as Date, FromHour as FromTimeString, '23:59' as ToTimeString from DayTimeWorking where CenterId = " + storeId;
        }
        public static string GetStoreDeliveryRegion(int storeId) 
        {
            return @"select DeliveryRegionTreeId as ID, CityRegionID as UniqueIdString from (
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=CityId and  center.CenterId =  CenterDeliveryRegion.CenterId and RegionId is null and AreaId is null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=RegionId and  center.CenterId =  CenterDeliveryRegion.CenterId and  RegionId is not null and AreaId is null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                union all
                                select DeliveryRegionTreeId, center.CenterId, center.UniqueId as StoreId, DeliveryRegionTree.UniqueId CityRegionID from CenterDeliveryRegion, center, DeliveryRegionTree where DeliveryRegionTreeid=AreaId and  center.CenterId =  CenterDeliveryRegion.CenterId and  AreaId is not null and CenterDeliveryRegion.CenterId =" + storeId + @" 
                                ) as tt ";
        }
        public static string GetStorePriceList() 
        {
            return @"IF OBJECT_ID('tempdb..#CenterPrice') IS NOT NULL drop table  #CenterPrice
                        select ROW_NUMBER() over (order by p.ProductId) as rowNo, p.ProductId, c.CenterId, c.UniqueId as StoreGuidString,  p.SellPrice as price, Product.UniqueId as ProductGuidString, p.Modifieddate 
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
	                        select 1, p.ProductId, p.CenterId, c.UniqueId as StoreGuidString,  p.SellPrice as price, Product.UniqueId as ProductGuidString , p.Modifieddate 
		                        from Price p, Center c, Product
			                         where p.CenterId is not null and (select dbo.ToShamsi(GETDATE()))  between StartDate and ISNULL(EndDate, '1499/12/12') 
				                        and p.CenterId = c.CenterId
				                        and p.ProductId = Product.ProductId
				                        and p.CustomerId is null and CustomerGroupId is null and p.CenterGroupId is null 

                        select  price, convert(uniqueidentifier, StoreGuidString) as StoreGuid, convert(uniqueidentifier, ProductGuidString) as ProductGuid from #CenterPrice
                            where StoreGuidString in (select uniqueid FROM Center where centertypeid=3) 
                        ";
        }
        public static string GetFiscalYearId()
        {
            return @"select FiscalYearId from FiscalYear where StartDate <= dbo.ToShamsi(getdate()) and enddate>= dbo.ToShamsi(getdate())";
        }
        public static string GetStoreStockOnHand(int fiscalYear)
        {
            return
            @"select convert(uniqueidentifier, Product.UniqueId) as ProductGuid, Qty, convert(uniqueidentifier, Center.UniqueId) as StoreGuid from (      
                      SELECT  ProductId,sum(Qty) as  Qty,CenterId    
                      FROM         
                      (        
                        SELECT iv.FiscalYearId,iv.StockId,id.ProductId ,   
                       (CASE WHEN vt.IsInput=1 THEN 1 ELSE -1 END * id.Qty) Qty          
                       ,iv.CenterId    
                        FROM InvVoucher iv           
                       INNER JOIN InvVoucherDetail id ON iv.InvVoucherId=id.InvVoucherId          
                       INNER JOIN InvVoucherType vt ON vt.InvVoucherTypeId=iv.InvVoucherTypeId         
                       where fiscalYearId = " + fiscalYear + @"        
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
                    ) as onhand, Product, Center where onhand.ProductId = Product.ProductId and Center.centerid = onhand.CenterId and centertypeid = 3";

        }
        public static string GetProduct()
        {
            return @"SELECT p.ProductId AS id, CONVERT(uniqueidentifier, p.UniqueId) AS uniqueid, p.ProductCode, p.ProductName, p.StoreProductName, p.PackVolume, p.PackWeight, 
                    p.Description, null  as PackUnitId, CASE ProductTypeId WHEN 1 THEN CONVERT(uniqueidentifier, '21B7F88F-42B2-40F6-83C9-EF20943440B9') 
                    WHEN 2 THEN CONVERT(uniqueidentifier, '594120A7-1312-45B2-883B-605000D33D0F') WHEN 3 THEN CONVERT(uniqueidentifier, 
                    '9DA7C343-CE14-4CBB-81AE-709E075D4E10') END AS ProductTypeId, pg.UniqueId as ProductGroupIdString, m.UniqueId as ManufactureIdString
                    FROM            Product AS p LEFT OUTER JOIN
                    Manufacturer AS m ON p.ManufacturerId = m.ManufacturerId LEFT OUTER JOIN
                    ProductGroupTreeSite AS pg ON p.ProductGroupTreeSiteId = pg.ProductGroupTreeSiteId
                    ";
        }
        public static string GetProductSupplier(int productId)
        {
            return @"select CONVERT(uniqueidentifier, s.uniqueId) as uniqueId from SupplierProduct as sp, supplier as s, product as p 
	                        where sp.SupplierId = s.SupplierId and p.ProductId = sp.ProductId and sp.ProductId='" + productId + "'";
        }
        public static string GetProductCharValue(int productId)
        {
            return @"select CONVERT(uniqueidentifier, pv.uniqueId) as uniqueId from ProductSpecificRel as ps , product as p, ProductSpecificityValue as pv
	                            where ps.ProductSpecificityValueId = pv.ProductSpecificityValueId and p.productId = ps.ProductID and p.productId ='" + productId + "'";
        }
        public static string GetProductGroupData()
        {
            return @"IF OBJECT_ID('tempdb..#GroupRec') IS NOT NULL drop table  #GroupRec
                    select p.ProductGroupTreeSiteId as ProductGroupTreeId, p.ParentId, p.Title, p.uniqueid, p2.uniqueid as Parentuniqueid, null  as CharGroupId
		                     into #GroupRec
		                     from ProductGroupTreeSite AS p INNER JOIN
                                             ProductGroupTreeSite AS p2 ON p.ParentId = p2.ProductGroupTreeSiteId 
		                     where p.parentid = p2.ProductGroupTreeSiteId";
        }
        public static string GetProductGroupTree()
        {
            return @"
                    WITH ProductGroupTreeLevels AS
                    (
                        SELECT
                            p.ProductGroupTreeSiteId as ProductGroupTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.uniqueid,
		                    convert(varchar(66),null) as Parentuniqueid,
		                    null as CharGroupId,
                            CONVERT(VARCHAR(MAX), p.ProductGroupTreeSiteId) AS thePath,
                            1 AS Level
	                    FROM            ProductGroupTreeSite AS p 
                        WHERE p.ParentId IS NULL 

                        UNION ALL

                        SELECT
                            e.ProductGroupTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.uniqueid,
		                    e.Parentuniqueid,
		                    e.CharGroupId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.ProductGroupTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM ProductGroupTreeLevels x
                        JOIN #GroupRec e on e.ParentId = x.ProductGroupTreeId
                    ),
                    ProductGroupTreeRows AS
                    (
                        SELECT
                             ProductGroupTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM ProductGroupTreeLevels
                    )
                    SELECT
	                    Er.UniqueId as UniqueIdString,
	                    Er.ParentUniqueId as ParentUniqueIdString,
                         ER.ProductGroupTreeId as ID,
                         ER.ParentId as ParentId,
	                     ER.Title as GroupName,
	                     ER.CharGroupId as CharGroupIdString,
                         --ER.thePath,
                         ER.Level as NLevel,
                         ER.Row,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM ProductGroupTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM ProductGroupTreeRows ER
                    ORDER BY thePath";
        }
        public static string GetManufacture()
        {
            return "select ManufacturerId as Id, convert(uniqueidentifier, uniqueid) as uniqueid, ManufacturerName as ManufactureName from Manufacturer";
        }
        public static string GetCharType()
        {
            return @"SELECT ProductSpecificityId as Id, ProductSpecificityName as CharTypeDesc, Convert(uniqueidentifier, UniqueId)  as UniqueId
                        FROM            ProductSpecificity";
        }
        public static string GetCharGroupCharType(int charGroupId )
        {
            return @"select Convert(uniqueidentifier,UniqueId) as UniqueId from ProductSpecificityGDetail g,  ProductSpecificity p 
                                    where p.ProductSpecificityId = g.ProductSpecificityId and ProductSpecificityGId=" + charGroupId;
        }
        public static string GetCharValue(int charTypeId)
        {
            return @"SELECT ProductSpecificityValueName as CharValueText, Convert(uniqueidentifier,UniqueId) as UniqueId
                                FROM ProductSpecificityValue where ProductSpecificityId=" + charTypeId;
        }
        public static string GetCharGroup()
        {
            return @"SELECT ProductSpecificityGId as Id, ProductSpecificityGroupName as CharGroupName, Convert(uniqueidentifier, UniqueId)  as UniqueId
                        FROM ProductSpecificityG";
        }
        public static string GetSupplir()
        {
            return "select SupplierId as Id, convert(uniqueidentifier, uniqueid) as uniqueid, SupplierName from Supplier ";
        }
        public static string GetCityRegion()
        {
            return @"WITH DeliveryRegionTreeLevels AS
                    (
                        SELECT
                            p.DeliveryRegionTreeId,
		                    p.ParentId,
		                    p.Title,
		                    p.UniqueId,
		                    CONVERT(varchar(66),'') as ParentUniqueId,
                            CONVERT(VARCHAR(MAX), p.DeliveryRegionTreeId) AS thePath,
                            1 AS Level
                        FROM DeliveryRegionTree p--, DeliveryRegionTree p2 
                        WHERE p.ParentId IS NULL --and p.ParentId = p2.DeliveryRegionTreeId

                        UNION ALL

                        SELECT
                            e.DeliveryRegionTreeId,
		                    e.ParentId,
		                    e.Title,
		                    e.UniqueId,
		                    e.ParentUniqueId,
                            x.thePath + '.' + CONVERT(VARCHAR(MAX), e.DeliveryRegionTreeId) AS thePath,
                            x.Level + 1 AS Level
                        FROM DeliveryRegionTreeLevels x
                        JOIN (select p.DeliveryRegionTreeId, p.ParentId, p.Title, p.UniqueId, p2.UniqueId as ParentUniqueId from  DeliveryRegionTree as p , DeliveryRegionTree as p2
			                    where p.ParentId = p2.DeliveryRegionTreeId)  e on e.ParentId = x.DeliveryRegionTreeId
                    ),
                    DeliveryRegionTreeRows AS
                    (
                        SELECT
                             DeliveryRegionTreeLevels.*,
                             ROW_NUMBER() OVER (ORDER BY thePath) AS Row
                        FROM DeliveryRegionTreeLevels
                    )
                    SELECT 
                         ER.DeliveryRegionTreeId,
                         ER.ParentId,
	                     ER.UniqueId as uniqueIdString,
	                     ER.parentUniqueId as ParentUniqueIdString,
	                     ER.Title as GroupName,
                         --ER.thePath,
                         ER.Level as NLevel,
                         --ER.Row,
                         (ER.Row * 2) - ER.Level AS NLeft,
                         ((ER.Row * 2) - ER.Level) + 
                            (
                                SELECT COUNT(*) * 2
                                FROM DeliveryRegionTreeRows ER2 
                                WHERE ER2.thePath LIKE ER.thePath + '.%'
                            ) + 1 AS NRight
                    FROM DeliveryRegionTreeRows ER
                    ";
        }
        public static string GetCenterPicture()
        {
            return @"select center.UniqueId as baseDataId, centerimageId as ID,  convert(uniqueidentifier, CenterImage.uniqueid) as uniqueid, CenterImage as image, CenterImageName as ImageName,
                                                    '9CED6F7E-D08E-40D7-94BF-A6950EE23915' as ImageType from CenterImage, Center
													where Center.CenterId = CenterImage.CenterId and CenterTypeId = 3";
        }

        public static string GetProductPicture()
        {
            return @"select Product.UniqueId as baseDataId, ProductimageId as ID,  convert(uniqueidentifier, ProductImage.uniqueid) as uniqueid, ProductImage as image, ProductImageName as ImageName,
                                                    '635126C3-D648-4575-A27C-F96C595CDAC5' as ImageType from ProductImage, Product
													where Product.ProductId = ProductImage.ProductId ";
        }

        public static string GetProductSiteGroupPicture()
        {
            return @"select ProductGroupTreeSite.UniqueId as baseDataId, ProductGroupTreeSiteimageId as ID,  convert(uniqueidentifier, ProductGroupTreeSiteImage.uniqueid) as uniqueid, ProductGroupTreeSiteImage as image, ProductGroupTreeSiteImageName as ImageName,
                                                    '149E61EF-C4DC-437D-8BC9-F6037C0A1ED1' as ImageType from ProductGroupTreeSiteImage, ProductGroupTreeSite
													where ProductGroupTreeSite.ProductGroupTreeSiteId = ProductGroupTreeSiteImage.ProductGroupTreeSiteId  ";
        }

    }
}