using Computer;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace _13_Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootloader bootloader = Bootloader.Instance;
            bootloader.Run(@"..\..\..\..\disk.vd", out SimulatorInterfaces.IOperatingSystem os);
        }
    }
}
