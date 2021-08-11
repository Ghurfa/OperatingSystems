using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_BuddyAllocator
{
    class BuddyMemory
    {
        byte[] arr;

        public byte this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }

        public readonly int Size;
        public readonly int MaxLevel;
        public readonly int Unit;

        public BuddyMemory()
        {
            Unit = 2;
            MaxLevel = 6;
            Size = Unit << MaxLevel;
            arr = new byte[Size];
            arr[0] = (byte)(MaxLevel << 1);
        }

        public bool TryAllocate(int bytes, out int address)
        {
            bytes++; //Add header size

            byte newOrder = 0;
            int newSize = Unit;
            while(newSize < bytes)
            {
                newOrder++;
                newSize *= 2;
            }

            int currentAddr = 0;
            while (currentAddr < arr.Length)
            {
                int blockOrder = arr[currentAddr] >> 1;
                bool isOccupied = (arr[currentAddr] & 1) == 1;

                if(isOccupied)
                {
                    if(blockOrder <= newOrder)
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
                        arr[currentAddr] |= 1;
                        address = currentAddr + 1;
                        return true;
                    }
                    else
                    {
                        while(blockOrder > newOrder)
                        {
                            //Split
                            blockOrder--;
                            int buddyAddr = currentAddr ^ (Unit << blockOrder);
                            arr[buddyAddr] = (byte)(blockOrder << 1);
                        }

                        arr[currentAddr] = (byte)((blockOrder << 1) | 1);
                        address = currentAddr + 1;
                        return true;
                    }
                }
            }
            address = 0;
            return false;
        }

        public void Deallocate(int address)
        {
            address--;
            int order = arr[address] >> 1;

            while (order < MaxLevel)
            {
                int buddyAddr = address ^ (Unit << order);
                if ((arr[buddyAddr] & 1) == 0 && (arr[buddyAddr] >> 1) == order)
                {
                    order++;
                    if (buddyAddr < address)
                    {
                        address = buddyAddr;
                    }
                }
                else break;
            }
            arr[address] = (byte)(order << 1);
        }

        public byte[] GetCopy()
        {
            byte[] ret = new byte[Size];
            for(int i = 0; i < Size; i++)
            {
                ret[i] = arr[i];
            }
            return ret;
        }
    }
}
