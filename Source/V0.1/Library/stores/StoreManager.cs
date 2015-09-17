using AnatoliaLibrary.products;
using AnatoliaLibrary.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.stores
{
    public class StoreManager
    {
        public async Task<List<StoreModel>> SearchAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<List<StoreModel>> SearchAsync(string storeName)
        {
            throw new NotImplementedException();
        }
        public async Task<List<StoreModel>> SearchAsync(DistrictModel district)
        {
            throw new NotImplementedException();
        }
        public async Task<List<StoreModel>> SearchAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public async Task<List<StoreModel>> SearchAsync(BrandModel pType)
        {
            throw new NotImplementedException();
        }
        public async Task<RateResponse> SaveRateAsync(StoreModel store, AnatoliaUserModel user)
        {
            throw new NotImplementedException();
        }
        public class RateResponse
        {
            public int RateCount { get; set; }
            public bool Result { get; set; }
        }
    }
}
