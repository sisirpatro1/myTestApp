using System;
using System.Threading;
using System.Diagnostics;
using System.Management;
namespace HighCPUUsage
{
    public class Worker
    {
        protected volatile bool shouldStop;
        protected Action action;
        public Worker(Action doSomething)
        {
            action = doSomething;
        }

        public void DoWork()
        {
            while (!shouldStop)
            {
                action();
            }
        }

        public void RequestStop()
        {
            shouldStop = true;
        }
    }
    class Program
    {
        static int ALLOCATIONS = 10000;
        static int ALLOCATION_SIZE = 16384;
        static int FACTORIAL_OF = 100;

        static void Main(string[] args)
        {
            ThreadTest();
        }
        //static int FACTORIAL_OF = 100;

        static void ThreadTest()
        {
            List<Thread> threads = new List<Thread>();
            List<Worker> workers = new List<Worker>();
            int n = Environment.ProcessorCount;

            for (int i = 0; i < n; i++)
            {
                Worker worker = new Worker(AllocationTest);
                Thread thread = new Thread(worker.DoWork);
                workers.Add(worker);
                threads.Add(thread);
            }

            threads.ForEach(t => t.Start());

            Console.WriteLine("Press ENTER key to stop...");
            Console.ReadLine();

            workers.ForEach(w => w.RequestStop());
            threads.ForEach(t => t.Join());

            Console.WriteLine("Done");
        }

        static void AllocationTest()
        {
            // Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
            object[] objects = new object[ALLOCATIONS];

            for (int i = 0; i < ALLOCATIONS; i++)
            {
                objects[i] = new byte[ALLOCATION_SIZE];
            }
        }
    }
}