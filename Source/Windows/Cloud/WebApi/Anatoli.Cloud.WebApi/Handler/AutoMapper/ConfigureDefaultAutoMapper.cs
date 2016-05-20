using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.PersonnelAcitvity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.PersonnelAcitvityModel;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.StoreModels;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anatoli.Cloud.WebApi.Handler
{
    public static class ConfigDefaultAutoMapperHelper
    {
        public static void Config()
        {
            ConfigModelToViewModel();
            ConfigViewModelToModel();
        }
        private static void ConfigModelToViewModel()
        {
            Mapper.CreateMap<FiscalYear, FiscalYearViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CityRegion, CityRegionViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ParentUniqueIdString, opt => opt.MapFrom(src => src.CityRegion2Id.ToString())).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Manufacture, ManufactureViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Supplier, SupplierViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<BaseType, BaseTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<BaseValue, BaseValueViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<BasketItem, BasketItemViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Basket, BasketViewModel>().ForMember(p => p.BasketTypeValueId, opt => opt.MapFrom(src => src.BasketTypeValueGuid)).ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ItemImage, ItemImageViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.BaseDataId, opt => opt.MapFrom(src => src.TokenId)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ReorderCalcType, ReorderCalcTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<CharType, CharTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CharGroup, CharGroupViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<CharValue, CharValueViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<ProductGroup, ProductGroupViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ParentUniqueIdString, opt => opt.MapFrom(src => src.ProductGroup2Id.ToString())).ForMember(p => p.ParentUniqueId, opt => opt.MapFrom(src => src.ProductGroup2Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Product, ProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductRate, ProductRateViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTag, ProductTagViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTagValue, ProductTagValueViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductType, ProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductPicture, ProductPictureViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<MainProductGroup, MainProductGroupViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ParentUniqueIdString, opt => opt.MapFrom(src => src.ProductGroup2Id.ToString())).ForMember(p => p.ParentUniqueId, opt => opt.MapFrom(src => src.ProductGroup2Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<IncompletePurchaseOrderLineItem, IncompletePurchaseOrderLineItemViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<IncompletePurchaseOrder, IncompletePurchaseOrderViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ShipAddressId, opt => opt.MapFrom(src => src.CustomerShipAddressId)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderLineItem, PurchaseOrderLineItemViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderStatusHistory, PurchaseOrderStatusHistoryViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrder, PurchaseOrderViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<CustomerShipAddress, CustomerShipAddressViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<Customer, CustomerViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());


            Mapper.CreateMap<Store, StoreViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreCalendar, StoreCalendarViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.FromTimeString, opt => opt.MapFrom(src => src.FromTime.ToString())).ForMember(p => p.ToTimeString, opt => opt.MapFrom(src => src.ToTime.ToString())).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreActiveOnhand, StoreActiveOnhandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ProductGuid, opt => opt.MapFrom(src => src.ProductId)).ForMember(p => p.StoreGuid, opt => opt.MapFrom(src => src.StoreId)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreActivePriceList, StoreActivePriceListViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ProductGuid, opt => opt.MapFrom(src => src.ProductId)).ForMember(p => p.StoreGuid, opt => opt.MapFrom(src => src.StoreId)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<Stock, StockViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProduct, StockProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockHistoryOnHand, StockHistoryOnHandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockOnHandSync, StockOnHandSyncViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockType, StockTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockActiveOnHand, StockActiveOnHandViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

            Mapper.CreateMap<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestProduct, StockProductRequestProductViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestRule, StockProductRequestRuleViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestStatus, StockProductRequestStatusViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestSupplyType, StockProductRequestSupplyTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestType, StockProductRequestTypeViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequest, StockProductRequestViewModel>().ForMember(p => p.UniqueId, opt => opt.MapFrom(src => src.Id)).ForMember(p => p.ID, opt => opt.Ignore());

        }

        private static void ConfigViewModelToModel()
        {
            Mapper.CreateMap<FiscalYearViewModel, FiscalYear>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<CityRegionViewModel, CityRegion>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.CityRegion2Id, opt => opt.ResolveUsing(src => ConvertNullableStringToGuid(src.ParentUniqueIdString))).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ManufactureViewModel, Manufacture>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<SupplierViewModel, Supplier>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<BaseTypeViewModel, BaseType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<BaseValueViewModel, BaseValue>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<BasketItemViewModel, BasketItem>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<BasketViewModel, Basket>().ForMember(p => p.BasketTypeValueGuid, opt => opt.MapFrom(src => src.BasketTypeValueId)).ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ItemImageViewModel, ItemImage>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.TokenId, opt => opt.MapFrom(src => src.BaseDataId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ReorderCalcTypeViewModel, ReorderCalcType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<CharTypeViewModel, CharType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<CharGroupViewModel, CharGroup>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<CharValueViewModel, CharValue>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<ProductGroupViewModel, ProductGroup>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.ProductGroup2Id, opt => opt.MapFrom(src => ConvertNullableStringToGuid(src.ParentUniqueIdString))).ForMember(p => p.ProductGroup2Id, opt => opt.MapFrom(src => src.ParentUniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductViewModel, Product>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductRateViewModel, ProductRate>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTagViewModel, ProductTag>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTagValueViewModel, ProductTagValue>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductTypeViewModel, Product>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<ProductPictureViewModel, ProductPicture>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<MainProductGroupViewModel, MainProductGroup>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.ProductGroup2Id, opt => opt.MapFrom(src => ConvertNullableStringToGuid(src.ParentUniqueIdString))).ForMember(p => p.ProductGroup2Id, opt => opt.MapFrom(src => src.ParentUniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<IncompletePurchaseOrderLineItemViewModel, IncompletePurchaseOrderLineItem>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<IncompletePurchaseOrderViewModel, IncompletePurchaseOrder>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.CustomerShipAddressId, opt => opt.MapFrom(src => src.ShipAddressId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderLineItemViewModel, PurchaseOrderLineItem>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderStatusHistoryViewModel, PurchaseOrderStatusHistory>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<PurchaseOrderViewModel, PurchaseOrder>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<CustomerShipAddressViewModel, CustomerShipAddress>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<CustomerViewModel, Customer>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());


            Mapper.CreateMap<StoreViewModel, Store>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreCalendarViewModel, StoreCalendar>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreActiveOnhandViewModel, StoreActiveOnhand>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.ProductId, opt => opt.MapFrom(src => src.ProductGuid)).ForMember(p => p.StoreId, opt => opt.MapFrom(src => src.StoreGuid)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StoreActivePriceListViewModel, StoreActivePriceList>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.ProductId, opt => opt.MapFrom(src => src.ProductGuid)).ForMember(p => p.StoreId, opt => opt.MapFrom(src => src.StoreGuid)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<StockViewModel, Stock>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductViewModel, StockProduct>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockHistoryOnHandViewModel, StockHistoryOnHand>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockOnHandSyncViewModel, StockOnHandSync>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockTypeViewModel, StockType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockActiveOnHandViewModel, StockActiveOnHand>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<StockProductRequestProductDetailViewModel, StockProductRequestProductDetail>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestProductViewModel, StockProductRequestProduct>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestRuleTypeViewModel, StockProductRequestRuleType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestRuleViewModel, StockProductRequestRule>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestStatusViewModel, StockProductRequestStatus>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestSupplyTypeViewModel, StockProductRequestSupplyType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestTypeViewModel, StockProductRequestType>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());
            Mapper.CreateMap<StockProductRequestViewModel, StockProductRequest>().ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId)).ForMember(p => p.Number_ID, opt => opt.Ignore());

            Mapper.CreateMap<PersonnelDailyActivityEventViewModel, PersonnelDailyActivityEvent>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId));
            Mapper.CreateMap<PersonnelDailyActivityPointViewModel, PersonnelDailyActivityPoint>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.UniqueId));

        }
        private static Guid? ConvertNullableStringToGuid(string data)
        {
            var guid = Guid.Empty;
            Guid.TryParse(data, out guid);
            return guid;
        }

    }
}