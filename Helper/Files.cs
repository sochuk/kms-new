using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KMS.Helper
{
    public static class Files
    {
        public static string getRandomFile(string path, string searchPattern = null, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var rand = new Random();
            string output = string.Empty;
            string map_path = HttpContext.Current.Server.MapPath(path);
            string dir = string.Format("{0}", path.Replace(map_path, "").Replace("\\", "/").Replace("~", ""));

            if (Directory.Exists(map_path))
            {
                var files = Directory.GetFiles(map_path, searchPattern, searchOption);
                var uri = new System.Uri(files[rand.Next(files.Length)], UriKind.Absolute);
                var filename = Path.GetFileName(uri.LocalPath);
                output = CPanel.url + dir + filename;
            }            
            return output;
        }

        public static List<string> searchFile(string path, string searchPattern = null, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            List<string> output = new List<string>();
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path, searchPattern, searchOption);
                foreach(var item in files)
                {
                    output.Add(item);
                }
            }
            return output;
        }
    }
}