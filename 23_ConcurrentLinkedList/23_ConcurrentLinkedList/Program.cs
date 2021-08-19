using System;
using System.Collections;
using System.Threading;

namespace _23_ConcurrentLinkedList
{
    class Node
    {
        public Node Next;
        public Node(Node next)
        {
            Next = next;
        }
        public Node()
        {
            Next = null;
        }
    }

    class NormalLinkedList
    {
        public int Count { get; private set; }
        private Node Head;

        public NormalLinkedList()
        {
            Head = null;
            Count = 0;
        }

        public void Insert()
        {
            Head = new Node(Head);
            Count++;
        }

        public int TraverseCount()
        {
            Node current = Head;
            int count = 0;
            while (current != null)
            {
                current = current.Next;
                count++;
            }
            return count;
        }
    }

    class ConcurrentLinkedList
    {
        private object LockObj;

        private Node Head;

        public ConcurrentLinkedList()
        {
            LockObj = new object();
        }

        public void Insert()
        {
            Head = new Node(Head);
        }

        public int TraverseCount()
        {
            lock (LockObj)
            {
                Node current = Head;
                int count = 0;
                while (current != null)
                {
                    current = current.Next;
                    count++;
                }
                return count;
            }
        }
    }

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

        static void Main(string[] args)
        {
            NormalInsertTest();
        }
    }
}
