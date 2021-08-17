using System;
using System.Threading;
using System.Threading.Tasks;

namespace BadThreads
{
    class Program
    {
        private static int Number = 0;
        static void Increment()
        {
            while (true)
            {
                Number++;
            }
        }
        static void Decrement()
        {
            while (true)
            {
                Number--;
            }
        }

        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Increment));
            Thread t2 = new Thread(new ThreadStart(Decrement));

            t1.Start();
            t2.Start();

            while (true)
            {
                Console.WriteLine(Number);
            }
        }
    }
}
