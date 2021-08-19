using SimulatorInterfaces;
using System;

namespace OperatingSystem
{
    public class BuddyMemoryManager : IMemoryManager
    {
        private IComputer Computer;
        private OperatingSystem OS;

        private readonly int Size;
        private readonly int MaxLevel;
        private readonly int Unit;

        public BuddyMemoryManager(IComputer computer, OperatingSystem os)
        {
            Computer = computer;
            OS = os;
            Unit = 16;
            Size = computer.RAM.Size; //I sure hope this is a power of 2
            MaxLevel = (int)Math.Log2(Size / Unit);
        }

        public void Initialize()
        {
            Computer.RAM[0] = MaxLevel << 1;
        }

        public int AllocateBlock(int pid, int blockSize)
        {
            blockSize++; //Add header size

            byte newOrder = 0;
            int newSize = Unit;
            while (newSize < blockSize)
            {
                newOrder++;
                newSize *= 2;
            }

            int currentAddr = 0;
            while (currentAddr < Computer.RAM.Size)
            {
                int blockOrder = Computer.RAM[currentAddr] >> 1;
                bool isOccupied = (Computer.RAM[currentAddr] & 1) == 1;

                if (isOccupied)
                {
                    if (blockOrder <= newOrder)
                    {
                        currentAddr += Unit << newOrder;
                    }
                    else
                    {
                        currentAddr += Unit << blockOrder;
                    }
                }
                else
                {
                    if (blockOrder < newOrder)
                    {
                        currentAddr += Unit << newOrder;
                    }
                    else if (blockOrder == newOrder)
                    {
                        Computer.RAM[currentAddr] |= 1;
                        return currentAddr + 1;
                    }
                    else
                    {
                        while (blockOrder > newOrder)
                        {
                            //Split
                            blockOrder--;
                            int buddyAddr = currentAddr ^ (Unit << blockOrder);
                            Computer.RAM[buddyAddr] = blockOrder << 1;
                        }

                        Computer.RAM[currentAddr] = (blockOrder << 1) | 1;
                        return currentAddr + 1;
                    }
                }
            }
            OS.OutOfMemory(null);
            return 0;
        }

        public void FreeBlock(int address)
        {
            address--;
            int order = Computer.RAM[address] >> 1;

            while (order < MaxLevel)
            {
                int buddyAddr = address ^ (Unit << order);
                if ((Computer.RAM[buddyAddr] & 1) == 0 && (Computer.RAM[buddyAddr] >> 1) == order)
                {
                    order++;
                    if (buddyAddr < address)
                    {
                        address = buddyAddr;
                    }
                }
                else break;
            }
            Computer.RAM[address] = (byte)(order << 1);
        }

        public void FreeProcessMemory(int pid)
        {
            throw new NotImplementedException();
        }
    }
}
