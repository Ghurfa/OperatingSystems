using System;

namespace _24_FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] disk = new byte[short.MaxValue];
            FileSystem fs = new FileSystem(disk);

            byte[] file = {1, 5, 8, 3, 123};
            fs.StoreFile("file1", file);
        }
    }
}
