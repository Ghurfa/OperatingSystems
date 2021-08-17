using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer
{
    public class CPU : ICPU
    {
        private IComputer Computer;
        public Dictionary<int, Action<object[]>> TrapTable { get; private set; }

        public CPU(IComputer computer)
        {
            Computer = computer;
        }
    }
}
