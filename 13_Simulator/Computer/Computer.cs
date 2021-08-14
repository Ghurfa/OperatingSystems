using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer
{
    public class Computer : IComputer
    {
        public void Run()
        {
            Bootloader.Instance.Run(@"..\..\..\..\disk.vd", out IOperatingSystem os);
            os.EntryPoint();
        }
    }
}
