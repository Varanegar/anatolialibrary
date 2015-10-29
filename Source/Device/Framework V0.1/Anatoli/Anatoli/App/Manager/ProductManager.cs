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

namespace Anatoli.App.Manager
{
    public class ProductManager : BaseManager<BaseDataAdapter<ProductModel>, ProductModel>
    {
        const string _productsTbl = "products";
        const string _productsView = "products_price_view";
        public List<Tuple<int, string>> GetCategories(int catId)
        {
            switch (catId)
            {
                case 0:
                    Tuple<int, string>[] l0 = { new Tuple<int,string>(1,"لبنیات"),
                                                 new Tuple<int,string>(2,"پروتئینی"),
                                                 new Tuple<int,string>(3,"خواربار"),
                                                 new Tuple<int,string>(4,"روغن"),
                                                 new Tuple<int,string>(5,"نوشیدنی")};
                    var list = l0.ToList<Tuple<int, string>>();
                    return list;
                case 1:
                    Tuple<int, string>[] l1 = {new Tuple<int,string>(0,"همه محصولات"),
                                                  new Tuple<int,string>(6,"پنیر"),
                                                 new Tuple<int,string>(7,"شیر"),
                                                 new Tuple<int,string>(8,"ماست"),
                                                 new Tuple<int,string>(9,"کره"),
                                                 new Tuple<int,string>(10,"دوغ") };
                    list = l1.ToList<Tuple<int, string>>();
                    return list;
                case 2:
                    Tuple<int, string>[] l2 = {new Tuple<int,string>(0,"همه محصولات"),
                                                   new Tuple<int,string>(11,"گوشت"),
                                                 new Tuple<int,string>(12,"مرغ"),
                                                 new Tuple<int,string>(13,"تخم مرغ")};
                    list = l2.ToList<Tuple<int, string>>();
                    return list;
                case 3:
                    Tuple<int, string>[] l3 = {new Tuple<int,string>(0,"همه محصولات"),
                                                 new Tuple<int,string>(14,"برنج"),
                                                 new Tuple<int,string>(15,"غلات"),
                                                 new Tuple<int,string>(16,"حبوبات"),
                                                 new Tuple<int,string>(17,"قند و شکر و نبات"),
                                                 new Tuple<int,string>(18,"لازانیا")};
                    list = l3.ToList<Tuple<int, string>>();
                    return list;
                case 4:
                    Tuple<int, string>[] l4 = { new Tuple<int,string>(0,"همه محصولات"),
                                                 new Tuple<int,string>(19,"روغن جامد"),
                                                 new Tuple<int,string>(20,"روغن مایع سرخ کردنی"),
                                                 new Tuple<int,string>(21,"روغن زیتون") };
                    list = l4.ToList<Tuple<int, string>>();
                    return list;
                case 5:
                    Tuple<int, string>[] l5 = {new Tuple<int,string>(0,"همه محصولات"),
                                                 new Tuple<int,string>(22,"چای"),
                                                 new Tuple<int,string>(23,"دمنوش"),
                                                 new Tuple<int,string>(24,"قهوه"),
                                                 new Tuple<int,string>(25,"نسکافه"),
                                                 new Tuple<int,string>(26,"آبمیوه") };
                    list = l5.ToList<Tuple<int, string>>();
                    return list;
                default:
                    return null;
            }
        }

        //public async Task<ProductModel> GetByIdAsync(string id)
        //{
        //    var parameter = new Query.SearchFilterParam("product_id", id);
        //    return await GetItemAsync(new DBQuery("products",parameter));
        //}

        public static async Task<bool> RemoveFavorit(int pId)
        {
            var dbQuery = new UpdateCommand(_productsTbl, new SearchFilterParam("product_id", pId.ToString()), new BasicParam("favorit", "0"));
            return await LocalUpdateAsync(dbQuery);
        }

        public static async Task<bool> AddToFavorits(ProductModel item)
        {
            var dbQuery = new UpdateCommand(_productsTbl, new SearchFilterParam("product_id", item.product_id.ToString()), new BasicParam("favorit", "1"));
            return await LocalUpdateAsync(dbQuery);
        }
    }
}
