using Anatoli.App.Model.Product;
using Anatoli.App.Model.Store;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class BasketManager : BaseManager<BasketViewModel>
    {
        public static async Task SyncDataBase()
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("baskets");
                var q = new RemoteQuery(TokenType.UserToken, Configuration.WebService.Users.BasketView, new BasicParam("after", lastUpdateTime.ToString()));
                var list = await BaseDataAdapter<BasketViewModel>.GetListAsync(q);
                await ShoppingCardManager.ClearAsync();
                await BaseDataAdapter<BasketViewModel>.UpdateItemAsync(new UpdateCommand("products", new BasicParam("favorit", "0"), new BasicParam("count", "0")));
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
                        await SyncManager.SaveUpdateDateAsync("baskets");
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        public static async Task AddFavoritToCloud()
        {
            try
            {
                var lastUpdateTime = await SyncManager.GetLastUpdateDateAsync("baskets");
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
                // todo: add delete
                var result = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<List<BasketItemViewModel>>(TokenType.UserToken, Configuration.WebService.Users.FavoritSaveItem, items);

            }
            catch (Exception e)
            {


            }
        }
    }
}
