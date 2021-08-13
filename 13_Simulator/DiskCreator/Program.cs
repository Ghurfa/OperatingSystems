using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DiskCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string osPath = @"..\..\..\..\OperatingSystem\bin\Debug\net5.0\OperatingSystem.dll";
            string diskPath = @"..\..\..\..\disk.vd";
            string filesFolderPath = @"..\..\..\..\VirtualFiles\";

            byte[] osAsm = File.ReadAllBytes(osPath);
            string[] fileNames = Directory.GetFiles(filesFolderPath);
            var sortedFiles = fileNames.OrderBy(x => x).ToArray();

            int blockSize = 1024;
            long headerLength = 1024;
            long osLength = osAsm.Length + blockSize - osAsm.Length % blockSize;
            long[] header = new long[headerLength / sizeof(long)];

            header[0] = headerLength; //OS location
            header[1] = osLength;
            header[2] = sortedFiles.Length;
            //header[3] empty

            (long Location, string Name)[] fileInfos = new (long, string)[sortedFiles.Length];

            long byteAddr = headerLength + osAsm.Length;
            byteAddr += blockSize - (byteAddr % blockSize);
            for (int i = 0; i < sortedFiles.Length; i++)
            {
                FileInfo info = new FileInfo(sortedFiles[i]);
                fileInfos[i].Location = byteAddr;
                fileInfos[i].Name = Path.GetFileNameWithoutExtension(sortedFiles[i]);

                long fileSize = 1 + fileInfos[i].Name.Length * sizeof(char) + info.Length;
                header[4 + 2 * i] = byteAddr;
                header[5 + 2 * i] = fileSize;
                byteAddr += fileSize;
                byteAddr += blockSize - (byteAddr % blockSize);
            }

            ReadOnlySpan<byte> headerSpan = MemoryMarshal.Cast<long, byte>(header.AsSpan());

            using var vDiskStream = new FileStream(diskPath, FileMode.Create);
            vDiskStream.Write(headerSpan.ToArray(), 0, headerSpan.Length);
            vDiskStream.Seek(headerLength, SeekOrigin.Begin);
            vDiskStream.Write(osAsm);

            for (int i = 0; i < sortedFiles.Length; i++)
            {
                var nameAsSpan = MemoryMarshal.Cast<char, byte>(fileInfos[i].Name.AsSpan()).ToArray();
                vDiskStream.Seek((int)fileInfos[i].Location, SeekOrigin.Begin);
                vDiskStream.WriteByte((byte)(fileInfos[i].Name.Length * sizeof(char)));
                vDiskStream.Write(nameAsSpan, 0, fileInfos[i].Name.Length * sizeof(char));

                using var vFileStream = new FileStream(sortedFiles[i], FileMode.Open);
                vFileStream.CopyTo(vDiskStream);
            }
        }
    }
}
