using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface ICPU
    {
        public Dictionary<int, Action<object[]>> TrapTable { get; }
    }
}
