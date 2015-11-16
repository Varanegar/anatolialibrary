using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.Framework.Manager;
using Anatoli.Framework.DataAdapter;
using Anatoli.App.Model.Product;
namespace Anatoli.App.Manager
{
    public class CategoryManager : BaseManager<BaseDataAdapter<CategoryInfoModel>, CategoryInfoModel>
    {
        public static List<CategoryInfoModel> GetCategories(int catId)
        {
            var list = new List<CategoryInfoModel>();
            switch (catId)
            {
                case 0:
                    CategoryInfoModel[] l0 = { new CategoryInfoModel(1,0,2,"لبنیات"),
                                                 new CategoryInfoModel(2,0,2,"پروتئینی"),
                                                 new CategoryInfoModel(3,0,2,"خواربار"),
                                                 new CategoryInfoModel(4,0,2,"روغن"),
                                                 new CategoryInfoModel(5,0,2,"نوشیدنی")};
                    list = l0.ToList<CategoryInfoModel>();
                    break;
                case 1:
                    CategoryInfoModel[] l1 = {
                                                  new CategoryInfoModel(6,1,3,"پنیر"),
                                                 new CategoryInfoModel(7,1,3,"شیر"),
                                                 new CategoryInfoModel(8,1,3,"ماست"),
                                                 new CategoryInfoModel(9,1,3,"کره"),
                                                 new CategoryInfoModel(10,1,3,"دوغ") };
                    list = l1.ToList<CategoryInfoModel>();
                    break;
                case 2:
                    CategoryInfoModel[] l2 = {
                                                   new CategoryInfoModel(11,2,3,"گوشت"),
                                                 new CategoryInfoModel(12,2,3,"مرغ"),
                                                 new CategoryInfoModel(13,2,3,"تخم مرغ")};
                    list = l2.ToList<CategoryInfoModel>();
                    break;
                case 3:
                    CategoryInfoModel[] l3 = {
                                                 new CategoryInfoModel(14,3,3,"برنج"),
                                                 new CategoryInfoModel(15,3,3,"غلات"),
                                                 new CategoryInfoModel(16,3,3,"حبوبات"),
                                                 new CategoryInfoModel(17,3,3,"قند و شکر و نبات"),
                                                 new CategoryInfoModel(18,3,3,"لازانیا")};
                    list = l3.ToList<CategoryInfoModel>();
                    break;
                case 4:
                    CategoryInfoModel[] l4 = { 
                                                 new CategoryInfoModel(19,4,3,"روغن جامد"),
                                                 new CategoryInfoModel(20,4,3,"روغن مایع سرخ کردنی"),
                                                 new CategoryInfoModel(21,4,3,"روغن زیتون") };
                    list = l4.ToList<CategoryInfoModel>();
                    break;
                case 5:
                    CategoryInfoModel[] l5 = {
                                                 new CategoryInfoModel(22,5,3,"چای"),
                                                 new CategoryInfoModel(23,5,3,"دمنوش"),
                                                 new CategoryInfoModel(24,5,3,"قهوه"),
                                                 new CategoryInfoModel(25,5,3,"نسکافه"),
                                                 new CategoryInfoModel(26,5,3,"آبمیوه") };
                    list = l5.ToList<CategoryInfoModel>();
                    break;
                default:
                    return null;
            }
            return list;
        }

        public static CategoryInfoModel GetParentCategory(int p)
        {
            if (p == 0)
                return new CategoryInfoModel(0, -1, 1, "همه محصولات");

            return new CategoryInfoModel(0, -1, 1, "همه محصولات");
        }

        public static CategoryInfoModel GetCategoryInfo(int p)
        {
            switch (p)
            {
                case 1:
                    return new CategoryInfoModel(1, 0, 2, "لبنیات");
                case 2:
                    return new CategoryInfoModel(2, 0, 2, "پروتئینی");
                case 3:
                    return new CategoryInfoModel(3, 0, 2, "خواربار");
                case 4:
                    return new CategoryInfoModel(4, 0, 2, "روغن");
                case 5:
                    return new CategoryInfoModel(5, 0, 2, "نوشیدنی");
                default:
                    return new CategoryInfoModel(0, -1, 1, "همه محصولات");
            }
        }
    }
}
