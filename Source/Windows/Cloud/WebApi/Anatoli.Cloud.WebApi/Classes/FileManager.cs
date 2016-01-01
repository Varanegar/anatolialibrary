using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Anatoli.Cloud.WebApi.Classes
{
    public class FileManager : IFileManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Methods
        public async Task Save(System.Web.HttpPostedFileBase file, string imagetype, string token, string imageName)
        {
            try
            {
                await Task.Run(() =>
                {
                    var physicalPath = GetPath(token, imagetype + "\\orginal", file.FileName);
                    file.SaveAs(physicalPath);

                    Image image100x100 = Scale(Image.FromFile(physicalPath), 100, 100);
                    image100x100.Save(GetPath(token, imagetype + "\\100x100", imageName + "-100x100.png"));

                    Image image320x320 = Scale(Image.FromFile(physicalPath), 320, 320);
                    image320x320.Save(GetPath(token, imagetype + "\\320x320", imageName + "-320x320.png"));
                });
            }
            catch(Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public string GetPath(string token, string imagetype, string fileName)
        {
            try
            {
                fileName = Path.GetFileName(fileName);

                var folder = string.Format("{0}Content\\Images\\{1}\\{2}\\", AppDomain.CurrentDomain.BaseDirectory, imagetype, token);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var physicalPath = Path.Combine(folder, fileName);

                return physicalPath;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;

                return string.Empty;
            }
        }

        public async Task Remove(string token, string imagetype, string fileName)
        {
            try
            {
                await Task.Run(() =>
                {
                    var physicalPath = GetPath(token, imagetype, fileName);

                    if (File.Exists(physicalPath))
                        File.Delete(physicalPath);
                });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;

            }

        }

        public List<string> GetFileNames(string token, string imagetype)
        {
            try
            {
                var physicalPath = GetPath(token, imagetype, "");

                var model = new List<string>();

                Directory.GetFiles(physicalPath).ToList().ForEach(itm =>
                {
                    model.Add(itm.Substring(itm.LastIndexOf('\\') + 1));
                });

                return model;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
                return new List<string>();
            }
        }

        public Image Scale(Image imgPhoto, int Width = 0, int Height = 0)
        {
            float sourceWidth = imgPhoto.Width,
                    sourceHeight = imgPhoto.Height,
                    destHeight = imgPhoto.Height,
                    destWidth = imgPhoto.Width;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else if (Width != 0)
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight);//,
            // PixelFormat.Format32bppPArgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.Default;//HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(0, 0, (int)destWidth, (int)destHeight),
                new Rectangle(0, 0, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
        #endregion
    }
}