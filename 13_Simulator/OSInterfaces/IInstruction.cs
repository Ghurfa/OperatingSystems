using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IInstruction
    {
        public InstructionType Type { get; }
        public int Value1 { get; }
        public int Value2 { get; }
        public int Value3 { get; }
    }
}
