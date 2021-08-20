using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace _23_ConcurrentLinkedList
{
    class Program
    {
        static void NormalThreadJob(NormalLinkedList ll, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ll.Insert();
            }
        }

        static void NormalInsertTest()
        {
            int threadCount = 10;
            int perThreadInsert = 1_000_000;

            NormalLinkedList ll = new();
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(() => NormalThreadJob(ll, perThreadInsert));
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("Instructed count:\t" + threadCount * perThreadInsert);
            Console.WriteLine("Stored count:\t\t" + ll.Count);
            Console.WriteLine("Actual count:\t\t" + ll.TraverseCount());
        }

        static void ThreadJob(ConcurrentLinkedList<int> ll, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ll.InsertFirst(i);
            }
        }

        static void InsertTest()
        {
            int threadCount = 10;
            int perThreadInsert = 1_000_000;

            ConcurrentLinkedList<int> ll = new();
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(() => ThreadJob(ll, perThreadInsert));
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("Instructed count:\t" + threadCount * perThreadInsert);
            Console.WriteLine("Stored count:\t\t" + ll.Count);
            Console.WriteLine("Actual count:\t\t" + ll.Count());
        }

        static void Main(string[] args)
        {
            while (true)
            {
                InsertTest();
            }
        }
    }
}
