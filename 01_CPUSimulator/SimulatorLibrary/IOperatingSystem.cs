using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    interface IOperatingSystem
    {
        enum Syscalls { }

        int AllocateMemory(int size);
        void FreeMemory(int address);
        void SwitchProcess();
        void RunScheduler();

    }
}
