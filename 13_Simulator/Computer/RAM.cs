using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer
{
    public class RAM : IRAM
    {
        private IComputer Computer;
        private int[] Ram;
        public int this[int index]
        {
            get => Ram[index];
            set => Ram[index] = value;
        }

        public int Size => short.MaxValue;

        public RAM (IComputer computer)
        {
            Computer = computer;
            Ram = new int[short.MaxValue];
        }
    }
}
