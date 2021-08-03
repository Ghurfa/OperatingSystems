using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorLibrary
{
    public class CPU
    {
        public enum SpecialRegisters
        {
            IPOffset = 25,
            RAMStart = 26,
            RAMEnd = 27,
            SP = 28,
            ProcessID = 29,
            ProcessCount = 30,
            SavedIP = 31,
        }

        public IInstruction[] Code { get; private set; }

        public IInstruction CurrentInstruction { get; private set; }

        public int[] Registers { get; private set; }
        public int IP { get; private set; }
        public int IPOffset     { get => Registers[25]; set => Registers[25] = value; }
        public int RAMStart     { get => Registers[26]; set => Registers[26] = value; }
        public int RAMEnd       { get => Registers[27]; set => Registers[27] = value; }
        public int SP           { get => Registers[28]; set => Registers[28] = value; }
        public int ProcessID    { get => Registers[29]; set => Registers[29] = value; }
        public int ProcessCount { get => Registers[30]; set => Registers[30] = value; }
        public int SavedIP      { get => Registers[31]; set => Registers[31] = value; }

        public RAM Ram { get; private set; }

        public CPU()
        {
            Registers = new int[32];
            Ram = new RAM(short.MaxValue);
        }

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
                    IP = IPOffset + jmp.Location;
                    break;
                case InstructionType.JmpAbs:
                    JmpAbsInstruction jmpAbs = (JmpAbsInstruction)CurrentInstruction;
                    IP = jmpAbs.Location;
                    break;
                case InstructionType.Store:
                    StoreInstruction store = (StoreInstruction)CurrentInstruction;
                    Ram[store.Address + RAMStart] = Registers[store.Register];
                    break;
                case InstructionType.StoreAbs:
                    StoreAbsInstruction storeAbs = (StoreAbsInstruction)CurrentInstruction;
                    Ram[storeAbs.Address] = Registers[storeAbs.Register];
                    break;
                case InstructionType.Load:
                    LoadInstruction load = (LoadInstruction)CurrentInstruction;
                    Registers[load.Register] = Ram[load.Address + RAMStart];
                    break;
                case InstructionType.LoadAbs:
                    LoadAbsInstruction loadAbs = (LoadAbsInstruction)CurrentInstruction;
                    Registers[loadAbs.Register] = Ram[loadAbs.Address];
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void SwitchContexts()
        {

        }
    }
}
