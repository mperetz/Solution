using System;
using System.Drawing;
using Oss.Versioning.Domain;

namespace Oss.Versioning.ConsoleApp
{
    public class ConsoleWrite : INotify
    {
        public void Write(string notification)
        {
            Console.WriteLine(notification);
        }

        public void WriteError(string error)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = originalColor;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWrite notifier = new ConsoleWrite();
            Console.WriteLine("Getting QA Machine Configuration");
            QAMachineCollector collector = new QAMachineCollector(notifier);
            collector.Collect();            
        }
    }
}
