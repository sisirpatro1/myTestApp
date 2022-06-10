using System;
using System.Threading;
using System.Diagnostics;
using System.Management;
namespace HighCPUUsage
{
    class Program
    {
        private const string QueryOrClassName = "SELECT LoadPercentage FROM Win32_Processor";

        static void Main(string[] args)
        {
            Debugger.NotifyOfCrossThreadDependency(); // for debugging
            int threadCount = Environment.ProcessorCount;
            Console.WriteLine("High CPU Usage application.Press ctrl / c for interrupt!");
            if (args.Length >= 1)
            {
                threadCount = Int32.Parse(args[0]);
            }
            for (int ii = 0; ii < threadCount; ii++)
            {
                Thread thread1 = new(new ThreadStart(ThreadFunction));
                thread1.Start();
            }
            Console.Write("Number of threads: {0}", threadCount);
            SelectQuery cpuQuery = new(QueryOrClassName);
            Console.Write("Cpu usage: ");
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            while (true)
            {
                ManagementObjectSearcher managementObjectSearcher = new(cpuQuery);
                ManagementObjectSearcher mgmtObjSrchr = managementObjectSearcher;
                foreach (ManagementObject cpuLoad in mgmtObjSrchr.Get())
                {
                    string cpuPercent = cpuLoad.GetPropertyValue("LoadPercentage").ToString();
                    Console.CursorLeft = x;
                    Console.CursorTop = y;
                    Console.Write("{0}%", cpuPercent);
                }
                Thread.Sleep(1000);

            }
            static void ThreadFunction()
            {
                while (true)
                {
                }
            }
        }
    }
}