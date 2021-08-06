using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    public class RAM
    {
        private int[] Data { get; set; }
        public int this[int i]
        {
            get => Data[i];
            set => Data[i] = value;
        }

        public RAM(int length)
        {
            Data = new int[length];
        }
    }
}
