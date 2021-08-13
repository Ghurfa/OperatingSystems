﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer
{
    public enum BootloaderResult
    {
        Successful,
        NoDisk,
        NoOS,
        InvalidOS,
        BadDiskFormat,
    }
}