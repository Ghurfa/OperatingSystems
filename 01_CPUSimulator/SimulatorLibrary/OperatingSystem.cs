using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    class OperatingSystem : IOperatingSystem
    {
        private int Magic = 0x10700BEE;
        public Registers Registers { get; set; }

        /* 0x0000 - 0x00FF  Empty
         * 0x0100 - 0xDFFF  Memory
         * 0xE000 - 0xE0FF  Running processes MLFQ pointers
         * 0xE100 - 0xE1FF  Blocked processes table
         * 0xE200 - 0xF7FF  Process table
         *      0x00 - 0x7F Registers
         *      0x80 - 0x83 Next pointer
         *      0x84 - 0xFF Empty
         * 0xF800 - 0xFFFF  OS Code (Reserved)
         */
        public RAM RAM { get; set; }

        public int AllocateMemory(int size)
        {
            int FreeChainStart = Registers[SpecialRegisters.FreeChainStart];
            int address = FreeChainStart;
            
            int prevNext = 0;
            while (address != 0)
            {
                int blockSize = RAM[address];
                int next = RAM[address + 1];
                if (blockSize >= size + 2)
                {
                    RAM[address] = Magic;
                    RAM[address + 1] = size;

                    int newStart;
                    if(blockSize <= size + 4)
                    {
                        newStart = next;
                    }
                    else
                    {
                        newStart = address + size + 2;
                        RAM[newStart] = blockSize - size - 2;
                        RAM[newStart + 1] = next;
                    }

                    if (prevNext == 0)
                    {
                        Registers[SpecialRegisters.FreeChainStart] = newStart;
                    }
                    else
                    {
                        RAM[prevNext] = newStart;
                    }

                    return address;
                }
                else
                {
                    prevNext = address + 1;
                    address = next;
                }
            }
            return 0;
        }

        public void FreeMemory(int address)
        {
            int magic = RAM[address - 2];
            if (magic != Magic) throw new NotImplementedException();

            int size = RAM[address - 1];

            int freeBlock = Registers[SpecialRegisters.FreeChainStart];
            int prevBlock = 0;
            while(freeBlock != 0)
            {

            }
            //Coalesce   
        }

        public void RunScheduler()
        {
            /* 1. Calculate Quantum
             * 2. If blocking, remove from main queue and add to blocking queue
             * 3. If full quantum, move to lower queue
             * 4. Get the first non-blocking process
             * 5. Switch to that process
             */
        }

        public void SwitchProcess()
        {
            throw new NotImplementedException();
        }
    }
}
