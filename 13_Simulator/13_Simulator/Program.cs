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
            Computer.Computer comp = new Computer.Computer();
            comp.Run();
        }
    }
}
