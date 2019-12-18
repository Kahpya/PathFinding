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
        // Length of this array must match the ItemType enum length
        // Values must match an index in the storeMap (ie between 0 & mapWidth * mapHeight)
        public static int[] ItemLocationGridArray = new int[]
        {
            5, // 5 max
            6, // 11 max
            14, // 17 max
            21, // 23 max
            23, // 29 max
            32 // 35 max
        };

        public static GenericGraph StoreMapLayout;
        public static ConsoleColor TextColorPointOfPurchase = ConsoleColor.Green;
        public static ConsoleColor TextColorCustomerPickUpItem = ConsoleColor.Blue;
        public static ConsoleColor TextColorCustomerMoves = ConsoleColor.Yellow;
        public static ConsoleColor TextColorCoordinates = ConsoleColor.White;
        public static ConsoleColor TextColorCustomerEnterStore = ConsoleColor.Cyan;
        public static ConsoleColor TextColorCustomerCalculatedPath = ConsoleColor.Magenta;
        public static Random RandomNumberGenerator = new Random();
        private static double chanceToSpawnCustomer = 0.3;
        private static int mapWidth = 6;
        private static int mapHeight = 6;

        

        static void Main(string[] args)
        {
            bool exit = false;
            long msBetweenNewCustomers = 1000;
            long currentTimeGoal = 0;

            CustomerFactory customerFactory = new CustomerFactory(3);
            GenericList<Customer> customersInStore = new GenericList<Customer>();
            StoreMapLayout = CreateGraphMap();

            PointOfPurchase.Instance.SetCustomerInStoreReference(customersInStore);

            // Set point of purchase in lower right corner of shop
            PointOfPurchase.Instance.SetNodeReference(StoreMapLayout[StoreMapLayout.Count - 1]);

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

        static GenericGraph CreateGraphMap()
        {
            GenericGraph map = new GenericGraph();

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    int thisIndex = (i * mapWidth) + j;

                    if (i == 0 && j == 0)
                    {
                        GenericGraphNode node = map.Add(); // Add upper left corner
                        node.X = j;
                        node.Y = i;
                    }
                    else if (i == 0 && j > 0)
                    {
                        GenericGraphNode node = map.Add(map[j - 1]); // add first row
                        node.X = j;
                        node.Y = i;
                    }
                    else
                    {
                        int upperIndex = ((i - 1) * mapWidth) + j;

                        if (j == 0)
                        {
                            GenericGraphNode node = map.Add(map[upperIndex]);
                            node.X = j;
                            node.Y = i;
                        }
                        else if (j > 0)
                        {
                            GenericGraphNode[] targetNodes = new GenericGraphNode[2]
                            {
                                map[upperIndex],
                                map[thisIndex -1]
                            };

                            GenericGraphNode node = map.Add(targetNodes);
                            node.X = j;
                            node.Y = i;
                        }
                    }
                }
            }

            return map;
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
