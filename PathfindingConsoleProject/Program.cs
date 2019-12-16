using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfindingConsoleProject.GameClasses;
using PathfindingConsoleProject.DataStructures;

namespace PathfindingConsoleProject
{
    class Program
    {
        public static ConsoleColor TextColorPointOfPurchase = ConsoleColor.Green;
        public static ConsoleColor TextColorCustomerPickUpItem = ConsoleColor.Blue;
        public static ConsoleColor TextColorCustomerMoves = ConsoleColor.Yellow;
        public static ConsoleColor TextColorCustomerEnterStore = ConsoleColor.Cyan;
        public static Random RandomNumberGenerator = new Random();
        private static double chanceToSpawnCustomer = 0.3;

        static void Main(string[] args)
        {
            bool exit = false;
            long msBetweenNewCustomers = 1000;
            long currentTimeGoal = 0;

            CustomerFactory customerFactory = new CustomerFactory(5);
            GenericList<Customer> customersInStore = new GenericList<Customer>();

            PointOfPurchase.Instance.SetCustomerInStoreReference(customersInStore);

            PrintStartingScreen();

            while (! exit)
            {
                // PLAY GAME

                // Check for and spawn new customers
                if (customerFactory.CanCreateNewCustomer && customersInStore.Count == 0 || customerFactory.CanCreateNewCustomer && RandomNumberGenerator.NextDouble() < chanceToSpawnCustomer)
                {
                    customersInStore.Add(customerFactory.Create());

                    currentTimeGoal = DateTimeOffset.Now.ToUnixTimeMilliseconds() + msBetweenNewCustomers;
                }

                // Handle customer business
                foreach (Customer customer in customersInStore)
                {
                    customer.MoveTowardsNextNode();
                }

                // Handle checkout point business
                PointOfPurchase.Instance.ParseNextCustomerInQueue();

                // Handle gracefull exit and step through to next iteration
                exit = CheckForKeyPressed();
                Console.WriteLine(string.Empty);
            }
        }

        static bool CheckForKeyPressed()
        {
            if(Console.ReadKey(true).Key == ConsoleKey.Escape)
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
            Console.WriteLine("*********** Press any other key to progress ***********");
            Console.WriteLine("*******************************************************");
            Console.WriteLine("*******************************************************");
            Thread.Sleep(500);
            Console.Write("3... ");
            Thread.Sleep(1000);
            Console.Write("2... ");
            Thread.Sleep(1000);
            Console.WriteLine("1...");
            Thread.Sleep(1000);
            Console.WriteLine(string.Empty);
        }
    }
}
