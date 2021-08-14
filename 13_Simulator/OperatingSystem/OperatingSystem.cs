using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatingSystem
{
    public class OperatingSystem : IOperatingSystem
    {
        IComputer Computer;
        public OperatingSystem(IComputer computer)
        {
            Computer = computer;
        }

        public void EntryPoint()
        {
            throw new NotImplementedException();
        }
    }
}
