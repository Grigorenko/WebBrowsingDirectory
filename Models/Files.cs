namespace WebBrowsingDirectory.Models
{
    public class Files
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string CurrentPath { get; set; }

        public Files()
        {
            Type = null;
            Name = null;
            Size = 0;
            CurrentPath = null;
        }

        public Files(string type, string name, long size, string path)
        {
            Type = type;
            Name = name;
            Size = size;
            CurrentPath = path;
        }
    }
}