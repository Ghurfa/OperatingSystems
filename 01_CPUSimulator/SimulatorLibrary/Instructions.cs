using System;

namespace SimulatorLibrary
{
    public enum InstructionType
    {
        Set,
        Increment,
        Mov,
        Add,
        Mul,
        Jmp,
        JmpAbs,
        Store,
        StoreAbs,
        Load,
        LoadAbs,
        ReadIO,
    }

    public interface IInstruction
    {
        InstructionType Type { get; }
        static bool Privileged { get; }
    }

    public struct SetInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Register { get; }
        public int Value { get; }
        public SetInstruction(int register, int val)
        {
            Type = InstructionType.Set;
            Register = register;
            Value = val;
        }
    }

    public struct IncrementInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Register { get; }
        public int Delta { get; }
    }

    public struct MovInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int From { get; }
        public int To { get; }
    }

    public struct AddInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Addend1Reg { get; }
        public int Addend2Reg { get; }
        public int SumReg { get; }
    }

    public struct MulInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Factor1Reg { get; }
        public int Factor2Reg { get; }
        public int ProductReg { get; }
        public MulInstruction(int prodReg, int fact1Reg, int fact2Reg)
        {
            Type = InstructionType.Set;
            Factor1Reg = fact1Reg;
            Factor2Reg = fact2Reg;
            ProductReg = prodReg;
        }
    }

    public struct JmpInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Location { get; }
    }

    public struct JmpAbsInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Location { get; }
    }

    public struct StoreInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }

    public struct StoreAbsInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }
    public struct LoadInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }

    public struct LoadAbsInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }
    public struct ReadIOInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Location { get; }
    }

}
