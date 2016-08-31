using System;
using System.Collections.Generic;
using System.Web.Http;
using WebBrowsingDirectory.Models;

namespace WebBrowsingDirectory.Controllers
{
    public class MetaController : ApiController
    {
        private int less = 0;
        private int middle = 0;
        private int more = 0;

        [HttpPost]
        public Sizing Post(object path)
        {
            if (path.ToString() == "root")
            {
                return new Sizing(0, 0, 0);
            }

            TraverseTree(path.ToString());

            return new Sizing(less,middle,more);
        }

        private void TraverseTree(string root)
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                if (currentDir == "C:")
                {
                    currentDir += "\\";
                }
                string[] subDirs;

                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    continue;
                }

                foreach (string file in files)
                {
                    try
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        Sizing(fi.Length);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        continue;
                    }
                }
                
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
        }

        private void Sizing(long size)
        {
            if (size < 10000000)
            {
                less++;
            }
            else
            {
                if (size >= 10000000 && size <= 50000000)
                {
                    middle++;
                }
                else
                {
                    if (size > 100000000)
                    {
                        more++;
                    }
                }
            }
        }
    }
}
