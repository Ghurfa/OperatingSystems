using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    public class Registers
    {
        private int[] Regs;
        private Action AccessError;

        public int this[int index]
        {
            get
            {
                if (index > 15 && Regs[(int)SpecialRegisters.IsPrivileged] == 0)
                {
                    AccessError();
                    return 0;
                }
                else return Regs[index];
            }
            set
            {
                if (index > 15 && Regs[(int)SpecialRegisters.IsPrivileged] == 0)
                {
                    AccessError();
                }
                else Regs[index] = value;
            }
        }

        public int this[SpecialRegisters index]
        {
            get => this[(int)index];
            set => this[(int)index] = value;
        }

        public Registers(Action accessAction)
        {
            AccessError = accessAction;
            Regs = new int[32];
        }

        public int GetPrivileged(int index) => Regs[index];
        public int GetPrivileged(SpecialRegisters index) => Regs[(int)index];

        public void SetPrivileged(int index, int value) => Regs[index] = value;
        public void SetPrivileged(SpecialRegisters index, int value) => Regs[(int)index] = value;
    }
}
