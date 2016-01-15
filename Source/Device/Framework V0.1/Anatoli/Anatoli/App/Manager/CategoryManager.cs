using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.Manager;
using Anatoli.Framework.DataAdapter;
using Anatoli.App.Model.Product;
using Anatoli.Framework.AnatoliBase;
namespace Anatoli.App.Manager
{
    public class CategoryManager : BaseManager<BaseDataAdapter<CategoryInfoModel>, CategoryInfoModel>
    {
        public static GroupLeftRightModel GetLeftRight(string catId)
        {
            using (var connection = AnatoliClient.GetInstance().DbClient.GetConnection())
            {
                SQLite.SQLiteCommand query;
                if (catId == null)
                    query = connection.CreateCommand("SELECT min(cat_left) as left , max(cat_right) as right FROM categories");
                else
                    query = connection.CreateCommand(String.Format("SELECT cat_left as left, cat_right as right FROM categories WHERE cat_id ='{0}'", catId));
                var lr = query.ExecuteQuery<GroupLeftRightModel>();
                return lr.First();
            }
        }
        public static List<CategoryInfoModel> GetFirstLevel()
        {
            var query = new SelectQuery("categories", new EqFilterParam("cat_depth", "1"));
            query.Unlimited = true;
            var list = GetList(query, null);
            return list;
        }
        public static async Task<List<CategoryInfoModel>> GetCategoriesAsync(string catId)
        {
            var query = new SelectQuery("categories", new EqFilterParam("cat_parent", catId));
            query.Unlimited = true;
            var list = await GetListAsync(query, null);
            return list;
        }

        public static List<CategoryInfoModel> GetCategories(string catId)
        {
            var query = new SelectQuery("categories", new EqFilterParam("cat_parent", catId));
            query.Unlimited = true;
            var list = GetList(query, null);
            return list;
        }
        public static async Task<CategoryInfoModel> GetParentCategory(string p)
        {
            var current = await GetItemAsync(new SelectQuery("categories", new EqFilterParam("cat_id", p)));
            var parent = await GetItemAsync(new SelectQuery("categories", new EqFilterParam("cat_id", current.cat_parent)));
            return parent;
        }

        public static async Task<CategoryInfoModel> GetCategoryInfo(string p)
        {
            var c = await GetItemAsync(new SelectQuery("categories", new EqFilterParam("cat_id", p)));
            return c;
        }

    }
}
