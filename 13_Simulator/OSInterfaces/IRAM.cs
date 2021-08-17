using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IRAM
    {
        public int Size { get; }
        public int this[int index] { get; set; }
    }
}
