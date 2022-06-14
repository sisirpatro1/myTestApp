using System;
using System.Threading;
using System.Diagnostics;
using System.Management;
namespace HighCPUUsage
{
    
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            for(int i = 0; i < 10000; i++)
            {
                Console.WriteLine("Counting as {0}", i);
                Thread.Sleep(100);
               
                while (i%2 == 0)
                {
                    Console.WriteLine("{0} is an even number.", i);
                    Thread.Sleep(1000);
                    i++;
                }
            }
        }
        
    }
}