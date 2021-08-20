using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _23_ConcurrentLinkedList
{
    class ConcurrentLinkedList<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private object HeadLock;

        private ConcurrentNode<T> Head;
        private int CountField;
        public int Count => CountField;

        public ConcurrentLinkedList()
        {
            HeadLock = new object();
        }

        public void InsertFirst(T value)
        {
            lock(HeadLock)
            {
                Head = new ConcurrentNode<T>(value, Head);
            }
            Interlocked.Increment(ref CountField);
        }

        public void RemoveFirst()
        {
            if (Head == null) throw new InvalidOperationException();
            lock (HeadLock)
            {
                Head = Head.Next;
            }
            Interlocked.Decrement(ref CountField);
        }

        public bool Contains(T value)
        {
            if (Head == null) return false;

            object nextLock = null;
            object currentLock = HeadLock;
            bool nextEntered = false;
            bool currentEntered = false;
            try
            {
                Monitor.Enter(HeadLock, ref currentEntered);
                ConcurrentNode<T> current = Head;
                while(current != null && !current.Value.Equals(value))
                {
                    nextLock = current.NextLock;
                    Monitor.Enter(nextLock, ref nextEntered);
                    Monitor.Exit(currentLock);
                    current = current.Next;
                    currentLock = nextLock;
                    currentEntered = true;
                    nextEntered = false;
                }
                return current != null;
            }
            finally
            {
                if (nextEntered) Monitor.Exit(nextLock);
                if (currentEntered) Monitor.Exit(currentLock);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Head == null) yield break;

            object nextLock = null;
            object currentLock = HeadLock;
            bool nextEntered = false;
            bool currentEntered = false;
            try
            {
                Monitor.Enter(HeadLock, ref currentEntered);
                ConcurrentNode<T> current = Head;
                while (current != null)
                {
                    yield return current.Value;
                    nextLock = current.NextLock;
                    Monitor.Enter(nextLock, ref nextEntered);
                    Monitor.Exit(currentLock);
                    current = current.Next;
                    currentLock = nextLock;
                    currentEntered = true;
                    nextEntered = false;
                }
            }
            finally
            {
                if (nextEntered) Monitor.Exit(nextLock);
                if (currentEntered) Monitor.Exit(currentLock);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
