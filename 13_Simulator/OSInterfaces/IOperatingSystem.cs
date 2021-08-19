using System;

namespace SimulatorInterfaces
{
    public interface IOperatingSystem
    {
        public IMemoryManager MemoryManager { get; }
        public IScheduler Scheduler { get; }
        public void EntryPoint();
    }
}
