using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IComputer
    {
        public ICPU CPU { get; }
        public IRegisters Registers { get; }
        public IRAM RAM { get; }
        public IOperatingSystem OperatingSystem { get; }
    }
}
