using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Computer
{
    public class Computer : IComputer
    {
        public ICPU CPU { get; private set; }
        public IRegisters Registers { get; private set; }
        public IRAM RAM { get; private set; }
        public IMemoryManager MemoryManager { get; private set; }
        public IOperatingSystem OperatingSystem { get; private set; }

        public Computer()
        {
            CPU = new CPU(this);
            Registers = new Registers(this);
            RAM = new RAM(this);
        }

        public void Run()
        {
            Bootloader.Instance.Run(@"..\..\..\..\disk.vd", this, out IOperatingSystem os);
            OperatingSystem = os;
            os.EntryPoint();
            CPU.StartScheduleTimer();
        }
    }
}
