﻿using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class ImageAdapter : BaseAdapter
    {
        private static ImageAdapter instance = null;
        public static ImageAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageAdapter();
                }
                return instance;
            }
        }
        ImageAdapter() { }
        public  List<ItemImageViewModel> CenterPictures(DateTime lastUpload)
        {
            List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and CenterImage.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ItemImageViewModel>(DBQuery.Instance.GetCenterPicture() + where);
                imageList = data.ToList();

            }

            return imageList;
        }

        public  List<ItemImageViewModel> ProductPictures(DateTime lastUpload)
        {
            List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ProductImage.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ItemImageViewModel>(DBQuery.Instance.GetProductPicture() + where);
                imageList = data.ToList();

            }

            return imageList;
        }

        public  List<ItemImageViewModel> ProductSiteGroupPictures(DateTime lastUpload)
        {
            List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ProductGroupTreeSiteImage.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ItemImageViewModel>(DBQuery.Instance.GetProductSiteGroupPicture() + where);
                imageList = data.ToList();

            }

            return imageList;
        }
    }
}
