﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IRegisters
    {
        public int this[int i] { get; set; }
        public bool KernelMode { get; set; }
    }
}
