using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid
{
    class AndroidFileIO : IFileIO
    {
        public string GetDataLoction()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }
        public string GetInternetCacheLoction()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.InternetCache);
        }
        public string ReadAllText(string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            return System.IO.File.ReadAllText(filePath);
        }
        public bool WriteAllText(string content, string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            try
            {
                System.IO.File.WriteAllText(filePath, content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool WriteAllBytes(byte[] content, string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            try
            {
                System.IO.File.WriteAllBytes(filePath, content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public byte[] ReadAllBytes(string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            return System.IO.File.ReadAllBytes(filePath);
        }


        public void DeleteFile(string path, string fileName)
        {
            System.IO.File.Delete(System.IO.Path.Combine(path, fileName));
        }
    }
}