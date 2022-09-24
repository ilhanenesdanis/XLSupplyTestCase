using System;
using System.Threading;

namespace SchuduleTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Trigger trigger = new Trigger();
            trigger.JobTrigger();
            Console.ReadKey();
        }
        
    }
}
