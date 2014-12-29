using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Semaphore;

namespace SemaphoreTest
{
    class Program
    {
        static QueuedSemaphore semaphore = new QueuedSemaphore(3, 3);

        static void Main(string[] args)
        {
            //for (int i = 0; i < 100; ++i)
            //{
            //    Thread thread = new Thread(PrintLine)
            //    {
            //        Name = i.ToString()
            //    };
            //    thread.Start();
            //    Thread.Sleep(10);
            //}


            //RunThreadsExitingFast();




            Console.ReadKey();
        }

        public static void RunThreadsExitingFast()
        {
            QueuedSemaphore semaphore = new QueuedSemaphore(5, 5);
            for (int i = 0; i < 100; ++i)
            {
                Thread thread = new Thread(PrintLine2)
                {
                    Name = i.ToString()
                };
                thread.Start(semaphore);
            }
        }

        private static void PrintLine2(object obj)
        {
            QueuedSemaphore semaphore = (QueuedSemaphore)obj;
            Console.WriteLine(string.Format("[{1}] Thread {0} is waiting", Thread.CurrentThread.Name, DateTime.Now));
            semaphore.WaitOne();
            Console.WriteLine(string.Format("[{1}] Thread {0} is working", Thread.CurrentThread.Name, DateTime.Now));
            semaphore.Release();
            Console.WriteLine(string.Format("[{1}] Thread {0} is done", Thread.CurrentThread.Name, DateTime.Now));
        }

        static void PrintLine()
        {
            semaphore.WaitOne();
            Console.WriteLine("Thread {0} start", Thread.CurrentThread.Name);
            Thread.Sleep(100);
            Console.WriteLine("Thread {0} end", Thread.CurrentThread.Name);
            semaphore.Release();
        }
    }
}
