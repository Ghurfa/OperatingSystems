using System;
using System.Diagnostics;
using System.Threading;

namespace MultiThreadCounter
{
    class Counter
    {
        public int Num;
        private object LockObj;

        public Counter()
        {
            Num = 0;
            LockObj = new object();
        }

        public void Increment()
        {
            lock (LockObj)
            {
                Num++;
            }
        }
    }

    class MultiThreadCounter
    {
        public int Goal { get; private set; }
        public int Value { get; private set; }

        private object LockObj;
        private int[] LocalCount;

        public MultiThreadCounter(int count)
        {
            LocalCount = new int[count];
            LockObj = new object();
        }

        public void Increment(int id, int threshold)
        {
            LocalCount[id]++;
            if(LocalCount[id] >= threshold)
            {
                lock(LockObj)
                {
                    Value += LocalCount[id];
                }
                LocalCount[id] = 0;
            }
        }

        public void Flush(int id)
        {
            lock (LockObj)
            {
                Value += LocalCount[id];
            }
            LocalCount[id] = 0;
        }
    }

    class Program
    {
        static volatile bool Started = false;

        static void ThreadJob1(Counter counter, int goal)
        {
            while (!Started) ;
            while (counter.Num < goal)
            {
                counter.Increment();
            }
        }

        static void Counter1(int numThreads)
        {
            int perThreadSize = 50_000_000;
            int goal = numThreads * perThreadSize;
            Thread[] threads = new Thread[numThreads];
            Counter counter = new Counter();

            for (int i = 0; i < numThreads; i++)
            {
                threads[i] = new Thread(() => ThreadJob1(counter, goal));
                threads[i].Start();
            }
            Stopwatch timer = Stopwatch.StartNew();
            Started = true;
            for (int i = 0; i < numThreads; i++)
            {
                threads[i].Join();
            }
            timer.Stop();

            Console.WriteLine(numThreads + " threads:\t" + timer.ElapsedMilliseconds + "\tPer:\t" + timer.ElapsedMilliseconds/numThreads);
        }

        static void ThreadJob2(MultiThreadCounter counter, int id, int goal)
        {
            while (!Started) ;
            while (counter.Value < goal)
            {
                counter.Increment(id, 100);
            }
            counter.Flush(id);
        }

        static void Counter2(int numThreads)
        {
            int perThreadSize = 50_000_000;
            int goal = numThreads * perThreadSize;
            Thread[] threads = new Thread[numThreads];
            MultiThreadCounter counter = new MultiThreadCounter(numThreads);

            for (int i = 0; i < numThreads; i++)
            {
                int copy = i;
                threads[i] = new Thread(() => ThreadJob2(counter, copy, goal));
                threads[i].Start();
            }
            Stopwatch timer = Stopwatch.StartNew();
            Started = true;
            for (int i = 0; i < numThreads; i++)
            {
                threads[i].Join();
            }
            timer.Stop();

            Console.WriteLine(numThreads + " threads:\t" + timer.ElapsedMilliseconds + "\tPer:\t" + timer.ElapsedMilliseconds / numThreads);
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                Counter1(i);
                Console.Write("\t\t\t\t\t\t");
                Counter2(i);
            }
            Console.ReadLine();
        }
    }
}
