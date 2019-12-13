using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PathfindingConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            PrintStartingScreen();

            while (! exit)
            {
                // PLAY GAME



                // Handle gracefull exit
                exit = CheckForEscPressed();
            }
        }

        static bool CheckForEscPressed()
        {
            if(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                return true;
            }
            return false;
        }

        static void PrintStartingScreen()
        {
            Console.WriteLine("*******************************************************");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("************* Press 'Escape' to exit game *************");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("*******************************************************");
            Thread.Sleep(500);
            Console.Write("3... ");
            Thread.Sleep(1000);
            Console.Write("2... ");
            Thread.Sleep(1000);
            Console.WriteLine("1...");
            Thread.Sleep(1000);
        }
    }
}
