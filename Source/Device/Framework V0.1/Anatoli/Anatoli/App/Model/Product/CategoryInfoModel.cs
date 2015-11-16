using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Product
{
    public class CategoryInfoModel : BaseDataModel
    {
        public int catId;
        public int parentId;
        public int depth;
        public string name;
        public CategoryInfoModel()
        {

        }
        public CategoryInfoModel(int catId, int parentId, int depth, string name)
        {
            // TODO: Complete member initialization
            this.catId = catId;
            this.parentId = parentId;
            this.depth = depth;
            this.name = name;
        }
    }
}
