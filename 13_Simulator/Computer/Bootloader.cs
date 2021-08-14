using SimulatorInterfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Computer
{
    public class Bootloader : IBootloader
    {
        public static Bootloader Instance { get; } = new Bootloader();
        private Bootloader() { }

        public BootloaderResult Run(string diskPath, out IOperatingSystem os)
        {
            if (!File.Exists(diskPath))
            {
                os = null;
                return BootloaderResult.NoDisk;
            }

            using var fs = File.Open(diskPath, FileMode.Open);
            try
            {
                byte[] header = new byte[1024];
                fs.Read(header, 0, 1024);

                long osAddr = BitConverter.ToInt64(header, 0);
                long osSize = BitConverter.ToInt64(header, 8);

                if (osAddr == 0)
                {
                    os = null;
                    return BootloaderResult.NoOS;
                }
                if (osSize > int.MaxValue)
                {
                    os = null;
                    return BootloaderResult.BadDiskFormat;
                }

                fs.Seek(osAddr, SeekOrigin.Begin);
                byte[] osData = new byte[osSize];
                fs.Read(osData, 0, (int)osSize);

                var asm = Assembly.Load(osData);

                Type[] osTypes;
                try
                {
                    osTypes = asm.GetTypes();
                }
                catch(ReflectionTypeLoadException)
                {
                    os = null;
                    return BootloaderResult.InvalidOS;
                }

                foreach (Type type in osTypes)
                {
                    if (typeof(IOperatingSystem).IsAssignableFrom(type))
                    {
                        os = Activator.CreateInstance(type) as IOperatingSystem;
                        //PropertyInfo propInfo = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);
                        //if (propInfo == null) break;

                        //MethodInfo instanceGetter = propInfo.GetMethod;
                        //os = (IOperatingSystem)instanceGetter.Invoke(instanceGetter, new object[0]);
                        //if (os == null) return BootloaderResult.InvalidOS;

                        return BootloaderResult.Successful;
                    }
                }
                os = null;
                return BootloaderResult.InvalidOS;
            }
            catch (EndOfStreamException)
            {
                os = null;
                return BootloaderResult.BadDiskFormat;
            }

        }
    }
}
