using System;
using System.Threading;

namespace BadSwapper
{
    struct SwapItem
    {
        long A;
        long B;
        public SwapItem(long A, long B)
        {
            this.A = A;
            this.B = B;
        }
        public static bool operator ==(SwapItem a, SwapItem b)
        {
            return a.A == b.A && a.B == b.B;
        }
        public static bool operator !=(SwapItem a, SwapItem b)
        {
            return !(a == b);
        }
    }

    class Program
    {
        static SwapItem ItemA = new SwapItem(1, 2);
        static SwapItem ItemB = new SwapItem(3, 4);
        static void Swap()
        {
            while (true)
            {
                var temp = ItemA;
                ItemA = ItemB;
                ItemB = temp;
            }
        }

        static void Main(string[] args)
        {
            Thread swapThread = new Thread(Swap);

            SwapItem originalA = ItemA;
            SwapItem originalB = ItemB;
            swapThread.Start();

            while(true)
            {
                if ( (ItemA != originalA && ItemA != originalB)||
                    (ItemB != originalA && ItemB != originalB))
                {
                    ;
                }
            }
        }
    }
}
