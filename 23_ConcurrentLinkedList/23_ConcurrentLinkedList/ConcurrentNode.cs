using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23_ConcurrentLinkedList
{
    class ConcurrentNode<T>
    {
        public T Value { get; set; }
        public ConcurrentNode<T> Next { get; set; }
        public object NextLock { get; }

        public ConcurrentNode(T value, ConcurrentNode<T> next = null)
        {
            Value = value;
            Next = next;
            NextLock = new object();
        }
    }
}
