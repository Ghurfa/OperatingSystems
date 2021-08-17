using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IMemoryManager
    {
        public void Initialize();
        public int AllocateBlock(int pid, int blockSize);
        public void FreeBlock(int address);
        public void FreeProcessMemory(int pid);
    }
}
