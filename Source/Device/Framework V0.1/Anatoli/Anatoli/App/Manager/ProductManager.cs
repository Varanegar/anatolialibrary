using Anatoli.Anatoliclient;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
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
        protected override string GetDataTable()
        {
            return "products_price_view";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Products.ProductsView;
        }
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

        public async Task<ProductModel> GetByIdAsync(string id)
        {
            return await GetItemAsync(new Query.FilterParam("product_id", id));
        }
    }
}
