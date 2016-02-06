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
using Android.Provider;

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


        public byte[] ReadAllBytes(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);
        }

        public static string GetPathToImage(Android.Net.Uri uri, Activity context)
        {
            string doc_id = "";
            using (var c1 = context.ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = context.ManagedQuery(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
    }
}