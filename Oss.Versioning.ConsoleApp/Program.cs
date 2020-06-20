using System;
using System.Drawing;
using System.Threading.Tasks;
using Oss.Versioning.Domain;

namespace Oss.Versioning.ConsoleApp
{
    public class ConsoleWrite : INotify
    {
        public void Write(string notification)
        {
            Console.WriteLine(notification);
        }

        public void WriteHeader(string header)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(header);
            Console.ForegroundColor = originalColor;
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
            // using an aysnc method so I got to use something like this
            MainAsync().GetAwaiter().GetResult();
            Console.WriteLine();
        }

        private static async Task MainAsync()
        {
            ConsoleWrite notifier = new ConsoleWrite();

            try
            {
                Console.WriteLine("Getting QA Machine Configuration");
                QAMachineCollector collector = new QAMachineCollector(notifier);
                var task = await collector.Collect();
            }
            catch (Exception ex)
            {
                notifier.WriteError($"something went wrong! {ex.Message}");
            }

        }
    }
}
