using Microsoft.Owin.Hosting;
using System;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApplication.Start<Startup>("http://localhost:8080"))
            {
                Console.WriteLine("Server running on localhost:8080...");
                Console.ReadLine();
            }
        }
    }
}
