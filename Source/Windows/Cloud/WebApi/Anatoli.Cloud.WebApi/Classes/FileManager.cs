using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Anatoli.Cloud.WebApi.Classes
{
    public class FileManager : IFileManager
    {
        #region Methods
        public async Task Save(System.Web.HttpPostedFileBase file, ObjectTypes objectTypes, string token)
        {
            try
            {
                await Task.Run(() =>
                {
                    var physicalPath = GetPath(token, objectTypes, file.FileName);

                    file.SaveAs(physicalPath);
                });
            }
            catch
            {
                //log error
            }
        }

        public string GetPath(string token, ObjectTypes objectTypes, string fileName)
        {
            try
            {
                fileName = Path.GetFileName(fileName);

                var folder = string.Format("{0}Content\\Images\\{1}\\{2}\\", AppDomain.CurrentDomain.BaseDirectory, objectTypes, token);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var physicalPath = Path.Combine(folder, fileName);

                return physicalPath;
            }
            catch (Exception ex)
            {
                //log error
                return string.Empty;
            }
        }

        public async Task Remove(string token, ObjectTypes objectTypes, string fileName)
        {
            try
            {
                await Task.Run(() =>
                {
                    var physicalPath = GetPath(token, objectTypes, fileName);

                    if (File.Exists(physicalPath))
                        File.Delete(physicalPath);
                });
            }
            catch
            {
                //log error
            }
        }

        public List<string> GetFileNames(string token, ObjectTypes objectTypes)
        {
            try
            {
                var physicalPath = GetPath(token, objectTypes, "");

                var model = new List<string>();

                Directory.GetFiles(physicalPath).ToList().ForEach(itm =>
                {
                    model.Add(itm.Substring(itm.LastIndexOf('\\') + 1));
                });

                return model;
            }
            catch (Exception ex)
            {
                //log error
                return new List<string>();
            }
        }
        #endregion
    }
}