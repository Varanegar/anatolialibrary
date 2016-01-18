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


        //public async Task<ProductModel> GetByIdAsync(string id)
        //{
        //    var parameter = new Query.SearchFilterParam("product_id", id);
        //    return await GetItemAsync(new DBQuery("products",parameter));
        //}

        public static async Task<bool> RemoveFavorit(string pId)
        {
            var dbQuery = new UpdateCommand(_productsTbl, new EqFilterParam("product_id", pId.ToString()), new BasicParam("favorit", "0"));
            return await LocalUpdateAsync(dbQuery) > 0 ? true : false;
        }

        public static async Task<bool> AddToFavorits(string pId)
        {
            var dbQuery = new UpdateCommand(_productsTbl, new EqFilterParam("product_id", pId.ToString()), new BasicParam("favorit", "1"));
            return await LocalUpdateAsync(dbQuery) > 0 ? true : false;
        }
        public static async Task<List<string>> GetSuggests(string key, int no)
        {
            var dbQuery = new SelectQuery(_productsTbl, new SearchFilterParam("product_name", key));
            dbQuery.Limit = 10000;
            var listModel = await Task.Run(() => { return GetList(dbQuery, null); });
            if (listModel.Count > 0)
                return ShowSuggests(listModel, no);
            else
                return null;
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
                var result = await LocalUpdateAsync(command);
                return (result > 0) ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static string GetImageAddress(string productId, string imageId)
        {
            string imguri = String.Format("http://79.175.166.186/content/Images/635126C3-D648-4575-A27C-F96C595CDAC5/100x100/{0}/{1}.png", productId, imageId);
            return imguri;
        }
    }
}
