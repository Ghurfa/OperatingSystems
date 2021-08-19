using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IDisk
    {
        public void WriteDisk(int address, byte[] buffer, int start, int length);
        public void ReadDisk(int address, byte[] buffer, int start, int length);
    }
}
