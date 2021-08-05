using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    public class RAM
    {
        /* 0x0000 - 0x00FF  Running processes MLFQ pointers
         * 0x0100 - 0x01FF  Blocked processes table
         * 0x0200 - 0x3FFF  Process table
         *      0x00 - 0x7F Registers
         *      0x80 - 0x83 Next pointer
         *      0x84 - 0xFF Empty
         */
        private int[] Data { get; set; }
        public int this[int i]
        {
            get => Data[i];
            set => Data[i] = value;
        }

        public RAM(int length)
        {
            Data = new int[length];
        }
    }
}
