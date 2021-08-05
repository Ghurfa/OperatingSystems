using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    public enum SpecialRegisters
    {
        SyscallType = 15,
        IsPrivileged = 24,
        IPOffset = 25,
        RAMStart = 26,
        RAMEnd = 27,
        SP = 28,
        ProcessID = 29,
        ProcessCount = 30,
        SavedIP = 31,
    }
}
