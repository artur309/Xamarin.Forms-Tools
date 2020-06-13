using System;
using System.Threading.Tasks;

namespace Password
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run().Wait();
        }
        private async Task Run()
        {


            Console.WriteLine("\nfim programa");
            Console.ReadKey();
        }
    }
}
