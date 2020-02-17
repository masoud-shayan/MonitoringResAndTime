using System;
using System.Diagnostics;
using static System.Diagnostics.Process;


namespace MonitoringLib
{
    public class Recorder

    {
        static Stopwatch timer = new Stopwatch();
        static long bytesPhysicalBefore = 0;
        static long bytesVirtualBefore = 0;

        
        public static void Start()
        {
            // force two garbage collections to release memory that is
            // no longer referenced but has not been released yet
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
            bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
            
            timer.Restart();

        }


        public static void Stop()
        {
            
            timer.Stop();
            
           long  bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
            long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;

            Console.WriteLine($"physical byte used  : {(bytesPhysicalAfter -bytesPhysicalBefore):N0}");
            Console.WriteLine($"virtual byte used  : {(bytesVirtualAfter -bytesVirtualBefore):N0}");
            Console.WriteLine($"span time elapsed  : {timer.Elapsed}");
            Console.WriteLine($"span time elapsed in milliseconds  : {timer.ElapsedMilliseconds:N0}");
            
            
        }
    }
}