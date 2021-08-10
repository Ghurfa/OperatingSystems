using System;
using System.Collections.Generic;
using System.Text;

namespace _12_BuddyAllocator
{
    class Program
    {
        static void DrawMemory(byte[] memory, int minBlockSize, int y)
        {
            int i = 0;
            Console.SetCursorPosition(0, y);
            while (i < memory.Length)
            {
                int blockOrder = memory[i] >> 1;
                bool occupied = (memory[i] & 1) == 1;

                Console.ForegroundColor = occupied ? (ConsoleColor)(blockOrder + 1) : ConsoleColor.White;

                int orderWidth = minBlockSize << blockOrder;
                Console.Write('|');
                Console.Write(new string('-', orderWidth - 1));

                i += orderWidth;
            }
            Console.WriteLine();
        }

        static void DrawMemoryMemory(IList<byte[]> memMem)
        {
            for (int i = 0; i < memMem.Count; i++)
            {
                DrawMemory(memMem[i], 2, i);
            }
        }

        static void Main(string[] args)
        {
            BuddyMemory memory = new BuddyMemory();
            Console.SetWindowSize(memory.Size + 2, 40);

            List<byte[]> memoryMemory = new()
            {
                memory.GetCopy()
            };

            Console.CursorTop = 21;
            while (true)
            {
                int savePos = Console.CursorTop;
                DrawMemoryMemory(memoryMemory);

                Console.SetCursorPosition(0, savePos);
                Console.ForegroundColor = ConsoleColor.White;
                string line = Console.ReadLine();
                string[] parts = line.Split(' ');

                bool save = true;
                switch (parts[0].ToLower())
                {
                    case "alloc":
                        if (parts.Length > 0 && int.TryParse(parts[1], out int allocBytes))
                        {
                            if(memory.TryAllocate(allocBytes, out int address))
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(address);
                            }
                        }
                        else save = false;
                        break;
                    case "free":
                        if (parts.Length > 0 && int.TryParse(parts[1], out int freeAddr))
                        {
                            memory.Deallocate(freeAddr);
                        }
                        else save = false;
                        break;
                    default:
                        save = false;
                        continue;
                }

                if (save)
                {
                    memoryMemory.Add(memory.GetCopy());
                    if (memoryMemory.Count > 20)
                    {
                        memoryMemory.RemoveAt(0);
                    }
                }
            }
        }
    }
}
