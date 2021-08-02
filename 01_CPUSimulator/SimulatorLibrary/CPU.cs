using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    public class CPU
    {
        public IInstruction[] Code { get; private set; }
        public int IP { get; private set; }
        public int IPOffset { get; private set; }

        public IInstruction CurrentInstruction { get; private set; }

        public int[] Registers { get; private set; }
        public int[] RAM { get; private set; }

        public CPU()
        {
            Registers = new int[32];
            RAM = new int[short.MaxValue];
        }

        public void Fetch()
        {
            CurrentInstruction = Code[IP++];
        }

        public void Execute()
        {
            switch(CurrentInstruction.Type)
            {
                case InstructionType.Set:
                    SetInstruction set = (SetInstruction)CurrentInstruction;
                    Registers[set.Register] = set.Value;
                    break;
                case InstructionType.Mov:
                    MovInstruction mov = (MovInstruction)CurrentInstruction;
                    Registers[mov.To] = Registers[mov.From];
                    break;
                case InstructionType.Jmp:
                    JmpInstruction jmp = (JmpInstruction)CurrentInstruction;
                    IP = IPOffset + jmp.Location;
                    break;
                case InstructionType.JmpAbs:
                    JmpAbsInstruction jmpAbs = (JmpAbsInstruction)CurrentInstruction;
                    IP = jmpAbs.Location;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
