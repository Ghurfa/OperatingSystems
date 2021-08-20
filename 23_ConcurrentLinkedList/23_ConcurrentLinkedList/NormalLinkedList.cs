using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
