using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer
{
    class Registers : IRegisters
    {
        private int[] Regs;
        private IComputer Computer;

        public int this[int i]
        {
            get => Regs[i];
            set => Regs[i] = value;
        }
        public bool KernelMode { get; set; }
        public Registers(IComputer comp)
        {
            Regs = new int[32];
            Computer = comp;
        }
    }
}
