using SimulatorInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Computer
{
    public class CPU : ICPU
    {
        private IComputer Computer;
        public Dictionary<int, Action<object[]>> TrapTable { get; private set; }

        public int IP { get; set; }

        private int scheduleTickFlagInt;
        private bool ScheduleTickFlag => scheduleTickFlagInt != 0;

        public CPU(IComputer computer)
        {
            Computer = computer;
            scheduleTickFlagInt = 0;
        }

        public void StartScheduleTimer()
        {
            void ScheduleTimer(int intervalMillis)
            {
                while (true)
                {
                    Thread.Sleep(intervalMillis);
                    Interlocked.Exchange(ref scheduleTickFlagInt, 1);
                }
            }
            Thread scheduleTimerThr = new Thread(() => ScheduleTimer(1000));
            scheduleTimerThr.Start();
        }

        private IInstruction Fetch()
        {
            //Todo: verify ip

        }
    }
}
