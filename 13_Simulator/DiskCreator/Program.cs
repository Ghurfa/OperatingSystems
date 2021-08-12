using System;
using System.IO;
using System.Reflection;

namespace DiskCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string osPath = @"..\..\..\..\OperatingSystem\bin\Debug\net5.0\OperatingSystem.dll";
            string diskPath = @"..\..\..\..\disk.vd";

            byte[] osAsm = File.ReadAllBytes(osPath);

            int headerLength = 1024;
            byte[] header = BitConverter.GetBytes(headerLength);

            using FileStream stream = new(diskPath, FileMode.OpenOrCreate);
            stream.Write(header, 0, header.Length);
            stream.Seek(headerLength, SeekOrigin.Begin);
            stream.Write(osAsm);
        }
    }
}
