using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using System.Threading;

namespace _03_SimulProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Missing isOdd arg");
                return;
            }

            bool even;
            if(int.TryParse(args[0], out int isOddNum))
            {
                even = isOddNum == 0;
            }
            else
            {
                Console.WriteLine("Could not parse isOdd");
                return;
            }

            string filePath = @"..\..\..\fight.txt";
            //FileStream strm = new FileStream(filePath, FileMode.OpenOrCreate);
            byte[] buffer = new byte[8];
            
            Mutex mutex;
            if(!Mutex.TryOpenExisting("fightMutex", out mutex))
            {
                mutex = new Mutex(true, "fightMutex");
            }
            if (even)
            {
                while (true)
                {
                    mutex.WaitOne();

                    using (FileStream strm = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        strm.Read(buffer, 0, 8);
                        ulong val = BitConverter.ToUInt64(buffer);
                        if (val % 2 == 1)
                        {
                            if (val == ulong.MaxValue) val = 0;
                            else val++;
                        }
                        BitConverter.GetBytes(val).CopyTo(buffer, 0);
                        strm.Write(buffer, 0, 8);
                    }

                    mutex.ReleaseMutex();
                }
            }
            else
            {
                while (true)
                {
                    mutex.WaitOne();

                    using (FileStream strm = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        strm.Read(buffer, 0, 8);
                        ulong val = BitConverter.ToUInt64(buffer);
                        if (val % 2 == 0)
                        {
                            val++;
                        }
                        BitConverter.GetBytes(val).CopyTo(buffer, 0);
                        strm.Write(buffer, 0, 8);
                    }
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
