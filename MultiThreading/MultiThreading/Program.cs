using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    class Program
    {
        [DllImport("kernel32.dll")]
        extern static int GetCurrentProcessorNumber();

        private int _instanceVar;
        private static int _staticVar;
        private static int mainThreadId;

        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MainThread";
            mainThreadId = Thread.CurrentThread.ManagedThreadId;

            /*
             * FOREGROUND THREADS
             */
            for (int i = 0; i < 10; i++)
            {
                DoWork(i); //executada pela thread principal

                var foregroundThread = new Thread(DoWork) { Name = "ForegroundThread-" + i };

                //estas threads são do tipo FOREGROUND.
                //a thread que as chama, caso precise encerrar, aguardaf o término dessas threads
                foregroundThread.Start(i);
            }
            Console.WriteLine("Waiting for threads finish...Press Any key to finish them");

            /*
             * BACKGROUND THREADS
             */
            for (int i = 0; i < 10; i++)
            {
                var backgroundThread = new Thread(DoWork) { Name = "BackgroundThread-" + i };
                backgroundThread.IsBackground = true;
                //a thread HOST, caso encerrada, não espera que elas terminem.
                backgroundThread.Start(i);
            }

            for (int i = 0; i < 20; i++)
            {
                //Sempre background threads
                //reaproveita threads

                ThreadPool.QueueUserWorkItem(DoWork, i);
            }

            for (int i = 0; i < 20; i++)
            {
               //tasks utilizam o ThreadPool

                Task.Factory.StartNew(DoWork, i);
            }

            Console.ReadKey(true);
        }

        /// <summary>
        /// Worker Thread
        /// </summary>
        /// <param name="obj"></param>
        private static void DoWork(object obj)
        {
            //try catch só é eficiente quando executado dentro da Thread, não na Thread em que foi startada.
            //não utilize finally pois nem sempre o bloco é executado, se a aplicação for encerrada abruptamente.
            try
            {
                var threadName = string.IsNullOrWhiteSpace(Thread.CurrentThread.Name)
                    ? Thread.CurrentThread.ManagedThreadId.ToString()
                    : Thread.CurrentThread.Name;

                var sw = Stopwatch.StartNew();
                Console.WriteLine(@"Thread {0}, processor: {1}, threadPoolThread: {2}, backgroundThread: {3}",
                    threadName,
                    GetCurrentProcessorNumber(),
                    Thread.CurrentThread.IsThreadPoolThread,
                    Thread.CurrentThread.IsBackground);

                //checa se a Thread é a principal
                if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
                    return;

                Console.ReadKey(true);
                sw.Stop();
                Console.WriteLine("Exiting... {0} after {1}", threadName, sw.Elapsed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
