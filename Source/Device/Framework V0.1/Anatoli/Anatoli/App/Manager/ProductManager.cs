using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model.Product;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.App.Model.Store;

namespace Anatoli.App.Manager
{
    public class ProductManager : BaseManager<ProductModel>
    {
        const string _productsTbl = "products";
        const string _productsView = "products_price_view";
        public static async Task<ProductModel> GetItemAsync(string id)
        {
            return await Anatoli.Framework.DataAdapter.BaseDataAdapter<ProductModel>.GetItemAsync(new StringQuery(string.Format("SELECT * FROM products_price_view WHERE product_id='{0}'", id)));
        }
        public static async Task SyncProducts(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync(SyncManager.ProductTbl);
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Products.ProductsView + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await BaseDataAdapter<ProductUpdateModel>.GetListAsync(q);
                Dictionary<string, ProductModel> items = new Dictionary<string, ProductModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM products");
                    var currentList = query.ExecuteQuery<ProductModel>();
                    foreach (var item in currentList)
                    {
                        items.Add(item.product_id, item);
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (items.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand("products", new EqFilterParam("product_id", item.UniqueId.ToUpper()),
                            new BasicParam("product_name", item.ProductName),
                            new BasicParam("cat_id", (item.ProductGroupIdString != null) ? item.ProductGroupIdString.ToUpper() : item.ProductGroupIdString));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand("products", new BasicParam("product_id", item.UniqueId.ToUpper()),
                            new BasicParam("product_name", item.ProductName),
                            new BasicParam("cat_id", (item.ProductGroupIdString != null) ? item.ProductGroupIdString.ToUpper() : item.ProductGroupIdString));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                }
                await SyncManager.SaveUpdateDateAsync(SyncManager.ProductTbl);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task SyncPrices(System.Threading.CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync(SyncManager.PriceTbl);
                var q = new RemoteQuery(TokenType.AppToken, Configuration.WebService.Stores.PricesView + "&dateafter=" + lastUpdateTime.ToString(), new BasicParam("after", lastUpdateTime.ToString()));
                q.cancellationTokenSource = cancellationTokenSource;
                var list = await BaseDataAdapter<ProductPriceUpdateModel>.GetListAsync(q);
                Dictionary<string, ProductPriceModel> items = new Dictionary<string, ProductPriceModel>();
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    var query = connection.CreateCommand("SELECT * FROM products_price");
                    var currentList = query.ExecuteQuery<ProductPriceModel>();
                    foreach (var item in currentList)
                    {
                        items.Add(item.product_id, item);
                    }
                }
                using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                {
                    connection.BeginTransaction();
                    foreach (var item in list)
                    {
                        if (items.ContainsKey(item.UniqueId))
                        {
                            UpdateCommand command = new UpdateCommand("products_price", new BasicParam("price", item.Price.ToString()),
                            new EqFilterParam("product_id", item.ProductGuid.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                        else
                        {
                            InsertCommand command = new InsertCommand("products_price", new BasicParam("price", item.Price.ToString()),
                            new BasicParam("product_id", item.ProductGuid.ToUpper()));
                            var query = connection.CreateCommand(command.GetCommand());
                            int t = query.ExecuteNonQuery();
                        }
                    }
                    connection.Commit();
                }
                await SyncManager.SaveUpdateDateAsync(SyncManager.PriceTbl);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task SyncFavorits()
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync(SyncManager.BasketTbl);
                var q = new RemoteQuery(TokenType.UserToken, Configuration.WebService.Users.BasketView, new BasicParam("after", lastUpdateTime.ToString()));
                var list = await BaseDataAdapter<BasketViewModel>.GetListAsync(q);
                await BaseDataAdapter<BasketViewModel>.UpdateItemAsync(new UpdateCommand("products", new BasicParam("favorit", "0")));
                foreach (var basket in list)
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        connection.BeginTransaction();
                        if (basket.BasketTypeValueId == BasketViewModel.FavoriteBasketTypeId)
                        {
                            foreach (var item in basket.BasketItems)
                            {
                                UpdateCommand command = new UpdateCommand("products", new EqFilterParam("product_id", item.ProductId.ToString().ToUpper()),
                              new BasicParam("favorit", "1"));
                                var query = connection.CreateCommand(command.GetCommand());
                                int t = query.ExecuteNonQuery();
                            }
                        }
                        connection.Commit();
                        await SyncManager.SaveUpdateDateAsync(SyncManager.BasketTbl);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }


        public static async Task<bool> RemoveFavorit(string pId)
        {
            var dbQuery = new UpdateCommand(_productsTbl, new EqFilterParam("product_id", pId.ToString()), new BasicParam("favorit", "0"));
            var r = await BaseDataAdapter<ProductModel>.UpdateItemAsync(dbQuery) > 0 ? true : false;
            if (r)
            {
                RemoveFavoritFromCloud(pId);
            }
            return r;
        }

        public static async Task<bool> AddToFavorits(string pId)
        {
            var dbQuery = new UpdateCommand(_productsTbl, new EqFilterParam("product_id", pId.ToString()), new BasicParam("favorit", "1"));
            var r = await BaseDataAdapter<ProductModel>.UpdateItemAsync(dbQuery) > 0 ? true : false;
            if (r)
            {
                AddFavoritToCloud();
            }
            return r;
        }

        public static async Task AddFavoritToCloud()
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync(SyncManager.BasketTbl);
                var q = new RemoteQuery(TokenType.UserToken, Configuration.WebService.Users.BasketView, new BasicParam("after", lastUpdateTime.ToString()));
                var list = await BaseDataAdapter<BasketViewModel>.GetListAsync(q);
                Guid basketId = default(Guid);
                foreach (var basket in list)
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        connection.BeginTransaction();
                        if (basket.BasketTypeValueId == BasketViewModel.FavoriteBasketTypeId)
                        {
                            basketId = Guid.Parse(basket.UniqueId);
                        }

                    }
                }

                var c = await CustomerManager.ReadCustomerAsync();
                var f = await ProductManager.GetFavorits();
                List<BasketItemViewModel> items = new List<BasketItemViewModel>();
                foreach (var item in f)
                {
                    var i = new BasketItemViewModel();
                    i.Qty = 1;
                    i.ProductId = Guid.Parse(item.product_id);
                    i.BasketId = basketId;
                    items.Add(i);
                }
                var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<List<BasketItemViewModel>>(TokenType.UserToken, Configuration.WebService.Users.FavoritSaveItem, items);

            }
            catch (Exception e)
            {


            }
        }

        public static async Task RemoveFavoritFromCloud(string productId)
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync(SyncManager.BasketTbl);
                var q = new RemoteQuery(TokenType.UserToken, Configuration.WebService.Users.BasketView, new BasicParam("after", lastUpdateTime.ToString()));
                var list = await BaseDataAdapter<BasketViewModel>.GetListAsync(q);
                Guid basketId = default(Guid);
                foreach (var basket in list)
                {
                    using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
                    {
                        connection.BeginTransaction();
                        if (basket.BasketTypeValueId == BasketViewModel.FavoriteBasketTypeId)
                        {
                            basketId = Guid.Parse(basket.UniqueId);
                        }

                    }
                }

                var c = await CustomerManager.ReadCustomerAsync();

                List<BasketItemViewModel> items = new List<BasketItemViewModel>();
                BasketItemViewModel favoritItem = new BasketItemViewModel();
                favoritItem.Qty = 1;
                favoritItem.ProductId = Guid.Parse(productId);
                favoritItem.BasketId = basketId;
                items.Add(favoritItem);
                var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<List<BasketItemViewModel>>(TokenType.UserToken, Configuration.WebService.Users.FavoritDeleteItem, items);

            }
            catch (Exception e)
            {


            }
        }


        public static async Task<List<string>> GetSuggests(string key, int no)
        {
            try
            {
                var dbQuery = new SelectQuery(_productsTbl, new SearchFilterParam("product_name", key));
                dbQuery.Limit = 10000;
                var listModel = await BaseDataAdapter<ProductModel>.GetListAsync(dbQuery);
                if (listModel.Count > 0)
                    return ShowSuggests(listModel, no);
                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        static List<string> ShowSuggests(List<ProductModel> list, int no)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<string> suggestions = new List<string>();
            foreach (var item in list)
            {
                var pname = item.product_name;
                var splits = pname.Split(new char[] { ' ' });
                string word = splits[0];
                if (!dict.ContainsKey(word))
                    dict.Add(word, 1);
                else
                    dict[word]++;

                if (splits.Length > 1)
                {
                    word = splits[0] + " " + splits[1];
                    if (!dict.ContainsKey(word))
                        dict.Add(word, 1);
                    else
                        dict[word]++;
                }

                if (splits.Length > 2)
                {
                    word = splits[0] + " " + splits[1] + " " + splits[2];
                    if (!dict.ContainsKey(word))
                        dict.Add(word, 1);
                    else
                        dict[word]++;
                }
            }

            foreach (KeyValuePair<string, int> item in dict.OrderByDescending(k => k.Value))
            {
                suggestions.Add(item.Key);
            }
            List<string> output = new List<string>();
            for (int i = 0; i < Math.Min(no, suggestions.Count); i++)
            {
                output.Add(suggestions[i]);
            }
            return output;
        }


        public static async Task<bool> RemoveFavoritsAll()
        {
            UpdateCommand command = new UpdateCommand("products", new BasicParam("favorit", "0"));
            try
            {
                var result = await BaseDataAdapter<ProductModel>.UpdateItemAsync(command);
                return (result > 0) ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static StringQuery Search(string value)
        {
            StringQuery query = new StringQuery(string.Format("SELECT * FROM products_price_view WHERE (product_name LIKE '{0}%' OR cat_name LIKE '{0}%') OR (product_name LIKE '% {0} %' OR cat_name LIKE '% {0} %') OR (product_name LIKE '% {0}' OR cat_name LIKE '% {0}') ORDER BY cat_id", value));
            return query;
        }

        public static string GetImageAddress(string productId, string imageId)
        {
            if (String.IsNullOrEmpty(productId) || string.IsNullOrEmpty(imageId))
                return null;
            else
            {
                string imguri = String.Format("{2}/content/Images/635126C3-D648-4575-A27C-F96C595CDAC5/100x100/{0}/{1}.png", productId, imageId,Configuration.WebService.PortalAddress);
                return imguri;
            }
        }
        public static async Task<List<ProductModel>> GetFavorits()
        {
            try
            {
                var dbQuery = new SelectQuery(_productsTbl, new EqFilterParam("favorit", "1"));
                return await BaseDataAdapter<ProductModel>.GetListAsync(dbQuery);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static StringQuery GetFavoritsQueryString()
        {
            var query = new StringQuery("SELECT * FROM products_price_view WHERE favorit = 1");
            return query;
        }

        public bool ShowGroups = false;
        string lastGroupId = Guid.NewGuid().ToString();
        public override async Task<List<ProductModel>> GetNextAsync()
        {
            var list = await base.GetNextAsync();
            if (!ShowGroups)
                return list;
            List<ProductModel> list2 = new List<ProductModel>();
            foreach (var item in list)
            {
                if (!item.cat_id.Equals(lastGroupId))
                {
                    lastGroupId = item.cat_id;
                    ProductModel g = new ProductModel();
                    g.is_group = 1;
                    g.cat_id = lastGroupId;
                    g.cat_name = item.cat_name;
                    g.product_name = await CategoryManager.GetFullName(item.cat_id);
                    list2.Add(g);
                    list2.Add(item);
                }
                else
                    list2.Add(item);
            }
            return list2;
        }
    }
}
