using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    class OperatingSystem
    {
        IInstruction[] Code = new IInstruction[]
        {
            new SetInstruction(32, 8),
            //new MulInstruction(32, 32, )
        };

        public void CopyCode(IInstruction[] buffer, int index) => Code.CopyTo(buffer, index);
        public int SwitchProcessInstruction { get; } = 0;
    }
}
