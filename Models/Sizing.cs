namespace WebBrowsingDirectory.Models
{
    public class Sizing
    {
        public int CountFilesLess { get; set; }
        public int CountFilesMiddle { get; set; }
        public int CountFilesMore { get; set; }

        public Sizing()
        {
            CountFilesLess = 0;
            CountFilesMiddle = 0;
            CountFilesMore = 0;
        }
        public Sizing(int less, int middle, int more)
        {
            CountFilesLess = less;
            CountFilesMiddle = middle;
            CountFilesMore = more;
        }
    }
}