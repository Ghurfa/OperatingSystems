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

        public IInstruction CurrentInstruction { get; private set; }

        public Registers Registers { get; private set; }
        public int IP { get; private set; }

        public RAM Ram { get; private set; }

        public CPU()
        {
            Registers = new Registers(AccessError);
            Ram = new RAM(short.MaxValue);
        }

        private void AccessError() { }

        public void Fetch()
        {
            CurrentInstruction = Code[IP++];
        }

        public void Execute()
        {
            switch (CurrentInstruction.Type)
            {
                case InstructionType.Set:
                    SetInstruction set = (SetInstruction)CurrentInstruction;
                    Registers[set.Register] = set.Value;
                    break;
                case InstructionType.Increment:
                    IncrementInstruction increment = (IncrementInstruction)CurrentInstruction;
                    Registers[increment.Register] += increment.Delta;
                    break;
                case InstructionType.Mov:
                    MovInstruction mov = (MovInstruction)CurrentInstruction;
                    Registers[mov.To] = Registers[mov.From];
                    break;
                case InstructionType.Add:
                    AddInstruction add = (AddInstruction)CurrentInstruction;
                    Registers[add.SumReg] = Registers[add.Addend1Reg] + Registers[add.Addend2Reg];
                    break;
                case InstructionType.Mul:
                    MulInstruction mul = (MulInstruction)CurrentInstruction;
                    Registers[mul.ProductReg] = Registers[mul.Factor1Reg] + Registers[mul.Factor2Reg];
                    break;
                case InstructionType.Jmp:
                    JmpInstruction jmp = (JmpInstruction)CurrentInstruction;
                    IP = Registers[SpecialRegisters.IPOffset] + jmp.Location;
                    break;
                case InstructionType.JmpAbs:
                    JmpAbsInstruction jmpAbs = (JmpAbsInstruction)CurrentInstruction;
                    IP = jmpAbs.Location;
                    break;
                case InstructionType.Store:
                    StoreInstruction store = (StoreInstruction)CurrentInstruction;
                    Ram[store.Address + Registers[SpecialRegisters.RAMStart]] = Registers[store.Register];
                    break;
                case InstructionType.StoreAbs:
                    StoreAbsInstruction storeAbs = (StoreAbsInstruction)CurrentInstruction;
                    Ram[storeAbs.Address] = Registers[storeAbs.Register];
                    break;
                case InstructionType.Load:
                    LoadInstruction load = (LoadInstruction)CurrentInstruction;
                    Registers[load.Register] = Ram[load.Address + Registers[SpecialRegisters.RAMStart]];
                    break;
                case InstructionType.LoadAbs:
                    LoadAbsInstruction loadAbs = (LoadAbsInstruction)CurrentInstruction;
                    Registers[loadAbs.Register] = Ram[loadAbs.Address];
                    break;
                case InstructionType.Syscall:
                    RunScheduler();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void RunScheduler()
        {
            /* 1. Calculate Quantum
             * 2. If blocking, remove from main queue and add to blocking queue
             */
            Registers.SetPrivileged(SpecialRegisters.SavedIP, IP);
            IP = 0;
        }
    }
}
