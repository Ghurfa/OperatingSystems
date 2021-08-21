using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24_FileSystem
{
    class FileSystem
    {
        public readonly int SectorSize;
        public readonly int DescriptorSize;
        public readonly int BlockHeaderSize;

        private byte[] Disk;
        private bool[] UsedBlocks;

        public FileSystem(byte[] disk)
        {
            Disk = disk;
            SectorSize = 1024;
            DescriptorSize = 128;
            BlockHeaderSize = sizeof(int);
            UsedBlocks = new bool[disk.Length / SectorSize];
            UsedBlocks[0] = true;
        }

        private bool FileNameMatches(int addr, string name)
        {
            int fileNameLen = Disk[addr];
            if (fileNameLen == name.Length)
            {
                for (int j = 0; j < fileNameLen; j++)
                {
                    char fileChar = (char)Disk[addr + 2 + j];
                    if (fileChar != name[j]) return false;
                }
                return true;
            }
            else return false;
        }

        public bool TryGetFileID(string name, out int id)
        {
            for (int i = 0; i < UsedBlocks.Length; i++)
            {
                int descAddr = i * DescriptorSize;
                if (FileNameMatches(descAddr, name))
                {
                    id = i;
                    return true;
                }
            }
            id = 0;
            return false;
        }

        public bool StoreFile(string name, Span<byte> data)
        {
            if (name.Length > 64) return false;

            int descriptorPos = 0; //Todo
            byte[] strBytes = name.ToCharArray().Select(x => Convert.ToByte(x)).ToArray();
            Disk[descriptorPos] = (byte)name.Length;
            strBytes.CopyTo(Disk, descriptorPos + 2);

            int fileSize = data.Length;
            BitConverter.GetBytes(fileSize).CopyTo(Disk, descriptorPos + 66);

            if (TryAllocateBlock(out int addr))
            {
                BitConverter.GetBytes(addr).CopyTo(Disk, descriptorPos + 66 + sizeof(int));
                return true;
            }
            else return false;
        }

        public IEnumerable<Memory<byte>> LoadFileBlock(string name)
        {
            if (!TryGetFileID(name, out int id)) throw new InvalidOperationException();

            int descAddr = DescriptorSize * id;
            int fileAddr = BitConverter.ToInt32(Disk, descAddr + 66 + sizeof(int));

            while (fileAddr != 0)
            {
                yield return new Memory<byte>(Disk, fileAddr + BlockHeaderSize, SectorSize - BlockHeaderSize);
                fileAddr = BitConverter.ToInt32(Disk, fileAddr);
            }
        }

        private bool TryAllocateBlock(out int addr)
        {
            for (int i = 0; i < UsedBlocks.Length; i++)
            {
                if (!UsedBlocks[i])
                {
                    addr = i * SectorSize;
                    return true;
                }
            }
            addr = 0;
            return false;
        }
    }
}
