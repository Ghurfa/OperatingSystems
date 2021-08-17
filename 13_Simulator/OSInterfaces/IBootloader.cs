﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IBootloader
    {
        public BootloaderResult Run(string diskPath, IComputer computer, out IOperatingSystem os);
    }
}
