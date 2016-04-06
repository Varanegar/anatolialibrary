using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace ClientApp
{
    public class ImageManagement
    {
        public static void UploadCenterPicture(HttpClient client, string servserURI)
        {
            var dataList = CenterPictures();
            UploadPictures(client, servserURI, dataList);
        }

        public static void UploadProductPicture(HttpClient client, string servserURI)
        {
            var dataList = ProductPictures();
            UploadPictures(client, servserURI, dataList);
        }
        public static void UploadProductGroupPicture(HttpClient client, string servserURI)
        {
            var dataList = ProductSiteGroupPictures();
            UploadPictures(client, servserURI, dataList); 
        }

        private static void UploadPictures(HttpClient client, string servserURI, List<ItemImageViewModel> dataList)
        {
            dataList.ForEach(item =>
                {
                    var requestContent = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(item.image);
                    imageContent.Headers.ContentType =
                        MediaTypeHeaderValue.Parse("image/jpeg");

                    requestContent.Add(imageContent, item.BaseDataId + "-" + item.ID, item.BaseDataId + "-" + item.ID + ".png");
                    requestContent.Headers.Add("OwnerKey", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240");
                    requestContent.Headers.Add("DataOwnerKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
                    requestContent.Headers.Add("DataOwnerCenterKey", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C");
                    var response = client.PostAsync(servserURI + "/api/imageManager/Save" + "?isDefault=" + item.IsDefault + "&imageId=" + item.UniqueId + "&imagetype=" + item.ImageType + "&token=" + item.BaseDataId, requestContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                    else
                    {
                        throw new Exception("Fail CallServerService URI :" );
                    }
                }
            );
        }

        public static List<ItemImageViewModel> CenterPictures()
        {
            List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<ItemImageViewModel>(@"select center.UniqueId as baseDataId, centerimageId as ID,  convert(uniqueidentifier, CenterImage.uniqueid) as uniqueid, CenterImage as image, CenterImageName as ImageName,
                                                    '9CED6F7E-D08E-40D7-94BF-A6950EE23915' as ImageType from CenterImage, Center
													where Center.CenterId = CenterImage.CenterId and CenterTypeId = 3");
                imageList = data.ToList();

            }

            return imageList;
        }

        public static List<ItemImageViewModel> ProductPictures()
        {
            List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<ItemImageViewModel>(@"select Product.UniqueId as baseDataId, ProductimageId as ID,  convert(uniqueidentifier, ProductImage.uniqueid) as uniqueid, ProductImage as image, ProductImageName as ImageName,
                                                    '635126C3-D648-4575-A27C-F96C595CDAC5' as ImageType from ProductImage, Product
													where Product.ProductId = ProductImage.ProductId ");
                imageList = data.ToList();

            }

            return imageList;
        }

        public static List<ItemImageViewModel> ProductSiteGroupPictures()
        {
            List<ItemImageViewModel> imageList = new List<ItemImageViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<ItemImageViewModel>(@"select ProductGroupTreeSite.UniqueId as baseDataId, ProductGroupTreeSiteimageId as ID,  convert(uniqueidentifier, ProductGroupTreeSiteImage.uniqueid) as uniqueid, ProductGroupTreeSiteImage as image, ProductGroupTreeSiteImageName as ImageName,
                                                    '149E61EF-C4DC-437D-8BC9-F6037C0A1ED1' as ImageType from ProductGroupTreeSiteImage, ProductGroupTreeSite
													where ProductGroupTreeSite.ProductGroupTreeSiteId = ProductGroupTreeSiteImage.ProductGroupTreeSiteId  ");
                imageList = data.ToList();

            }

            return imageList;
        }

    }
}
