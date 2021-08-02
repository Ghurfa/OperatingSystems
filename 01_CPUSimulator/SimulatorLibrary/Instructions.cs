using System;

namespace SimulatorLibrary
{
    public enum InstructionType
    {
        Set,
        Mov,
        Add,
        Jmp,
        JmpAbs,
        SetMem,
        SetMemAbs,
        LoadMem,
        LoadMemAbs,
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

    public struct SetMemInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }

    public struct SetMemAbsInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }
    public struct LoadMemInstruction : IInstruction
    {
        public InstructionType Type { get; }
        public int Address { get; }
        public int Register { get; }
    }

    public struct LoadMemAbsInstruction : IInstruction
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
