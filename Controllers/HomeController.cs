using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using WebBrowsingDirectory.Models;

namespace WebBrowsingDirectory.Controllers
{
    public class HomeController : ApiController
    {
        DirectoryInfo dir;
        IEnumerable<FileInfo> files;
        IEnumerable<DirectoryInfo> directories;
        
        [HttpPost]
        public IEnumerable<Files> Post(object newPath)
        {
            string path = Convert.ToString(newPath);
            if (path == "root")
            {
                List<Files> filesAndDirectories = new List<Files>();
                DriveInfo[] di = DriveInfo.GetDrives();

                foreach (var i in di)
                {
                    filesAndDirectories.Add(new Files() { Type = "dir", Name = i.Name, CurrentPath = i.Name.TrimEnd('\\') });
                }

                return filesAndDirectories;
            }

            return Get(path);
        }

        private IEnumerable<Files> Get(string path)
        {
            if (path == "C:")
            {
                path += "\\";
            }
            dir = new DirectoryInfo(path);
            
            try
            {
                files = dir.EnumerateFiles();
                directories = dir.EnumerateDirectories();
            }
            catch (UnauthorizedAccessException e)
            {
            }

            List<Files> filesAndDirectories = new List<Files>();

            filesAndDirectories.Add(new Files { Type = "dir", Name = "..", CurrentPath = GetPrevPath(path) });
            if (directories != null)
            {
                foreach (var i in directories)
                {
                    filesAndDirectories.Add(new Files { Type = "dir", Name = i.Name, CurrentPath = i.FullName });
                }
            }

            if (files != null)
            {
                foreach (var i in files)
                {
                    filesAndDirectories.Add(new Files { Type = "file", Name = i.Name, Size = i.Length, CurrentPath = i.DirectoryName });
                }
            }

            return filesAndDirectories;
        }

        private string GetPrevPath(string currentPath)
        {
            int semi = currentPath.IndexOf(':');
            int lio = currentPath.LastIndexOf("\\");
            if (semi + 1 == lio && currentPath.Length == 3)
            {
                currentPath = currentPath.TrimEnd('\\');
            }
            int index = currentPath.LastIndexOf("\\");

            return index == -1 ? "root" : currentPath.Remove(index, currentPath.Length - index);
        }
    }
}
