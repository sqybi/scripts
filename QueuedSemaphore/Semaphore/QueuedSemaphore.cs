using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphore
{
    public class QueuedSemaphore
    {
        private readonly object semaphoreLock = new object();
        private readonly Queue<AutoResetEvent> waitQueue = new Queue<AutoResetEvent>();
        private int semaphore;
        private readonly int maximumSemaphore;

        /// <summary>
        /// Constructor of QueuedSemaphore instance
        /// </summary>
        /// <param name="initialSemaphore">Initial semaphore value</param>
        /// <param name="maximumSemaphore">Maximum semaphore value</param>
        public QueuedSemaphore(int initialSemaphore, int maximumSemaphore)
        {
            if (semaphore > this.maximumSemaphore)
            {
                throw new ArgumentException("Initial semaphore count should less than or equal to its maximum count");
            }
            this.semaphore = initialSemaphore;
            this.maximumSemaphore = maximumSemaphore;
        }

        /// <summary>
        /// Wait for available semaphore, then wake up the thread
        /// </summary>
        public void WaitOne()
        {
            AutoResetEvent waitEvent = new AutoResetEvent(false);
            bool waitFlag = false;
            lock (semaphoreLock)
            {
                if (semaphore > 0)
                {
                    semaphore--;
                }
                else
                {
                    waitFlag = true;
                    waitQueue.Enqueue(waitEvent);
                }
            }
            if (waitFlag)
            {
                waitEvent.WaitOne();
            }
        }

        /// <summary>
        /// Release one semaphore and wake up the task at the beginning of the queue
        /// </summary>
        public void Release()
        {
            AutoResetEvent waitEvent = null;
            lock (semaphoreLock)
            {
                if (semaphore + 1 > maximumSemaphore)
                {
                    throw new SemaphoreFullException();
                }
                else
                {
                    if (waitQueue.Count > 0)
                    {
                        waitEvent = waitQueue.Dequeue();
                    }
                    else
                    {
                        semaphore++;
                    }
                }
            }
            if (waitEvent != null)
            {
                waitEvent.Set();
            }
        }
    }
}
