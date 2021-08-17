using System;

namespace SimulatorInterfaces
{
    public interface IOperatingSystem
    {
        public IMemoryManager MemoryManager { get; }
        public void EntryPoint();
    }
}
