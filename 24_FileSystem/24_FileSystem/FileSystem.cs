using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_FileSystem
{
    class FileSystem
    {
        private byte[] Disk;

        public readonly int SectorSize;

        public FileSystem(byte[] disk)
        {
            SectorSize = 1024;
            Disk = new byte[short.MaxValue];
        }

        public int GetFileID(string name)
        {
            for(int i = 0; i < 1024; i++)
            {

            }
            throw new NotImplementedException();
        }

        public void StoreFile(string name, Span<byte> data)
        {

        }
    }
}
