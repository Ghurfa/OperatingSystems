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
        IDisk Disk;

        public IMemoryManager MemoryManager { get; }

        public OperatingSystem(IComputer computer)
        {
            Computer = computer;
            MemoryManager = new BuddyMemoryManager(Computer, this);
        }

        public void EntryPoint()
        {
            //Setup Trap Table
            Computer.CPU.TrapTable.Add(1, (x) => ReadDisk(x));
            Computer.CPU.TrapTable.Add(2, (x) => StartProcess(x));

            MemoryManager.Initialize();
        }

        private void ReadDisk(object[] args)
        {

        }

        private void StartProcess(object[] args)
        {
            //Load code
            //Alloc memory
        }

        internal void OutOfMemory(object[] args)
        {
            //Kill x
        }
    }
}
