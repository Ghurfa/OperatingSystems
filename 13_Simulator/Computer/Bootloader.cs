using System;
using System.IO;
using System.Reflection;

namespace Computer
{
    public class Bootloader
    {
        public static Bootloader Instance { get; } = new Bootloader();
        private Bootloader()
        {

        }

        public BootloaderResult Run(string diskPath)
        {
            if (!File.Exists(diskPath))
            {
                return BootloaderResult.NoDisk;
            }

            using var fs = File.Open(diskPath, FileMode.Open);
            try
            {
                byte[] header = new byte[1024];
                fs.Read(header, 0, 1024);

                long osAddr = BitConverter.ToInt64(header, 0);
                long osSize = BitConverter.ToInt64(header, 8);

                if(osAddr == 0) return BootloaderResult.NoOS;
                if (osSize > int.MaxValue) return BootloaderResult.BadDiskFormat;

                fs.Seek(osAddr, SeekOrigin.Begin);
                byte[] osData = new byte[osSize];
                fs.Read(osData, 0, (int)osSize);

                var asm = Assembly.Load(osData);

                throw new NotImplementedException();
                return BootloaderResult.Successful;
            }
            catch(EndOfStreamException)
            {
                return BootloaderResult.BadDiskFormat;
            }

        }
    }
}
