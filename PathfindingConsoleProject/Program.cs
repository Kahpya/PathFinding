﻿using System;
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
            7,
            23,
            49,
            50,
            75,
            91
        };

        public static GenericGraph StoreMapLayout;
        public static ConsoleColor TextColorPointOfPurchase = ConsoleColor.Green;
        public static ConsoleColor TextColorCustomerPickUpItem = ConsoleColor.Blue;
        public static ConsoleColor TextColorCustomerMoves = ConsoleColor.Yellow;
        public static ConsoleColor TextColorCustomerEnterStore = ConsoleColor.Cyan;
        public static Random RandomNumberGenerator = new Random();
        private static double chanceToSpawnCustomer = 0.3;
        private static int mapWidth = 10;
        private static int mapHeight = 10;

        

        static void Main(string[] args)
        {
            bool exit = false;
            long msBetweenNewCustomers = 1000;
            long currentTimeGoal = 0;

            CustomerFactory customerFactory = new CustomerFactory(5);
            GenericList<Customer> customersInStore = new GenericList<Customer>();
            StoreMapLayout = CreateGraphMap();

            PointOfPurchase.Instance.SetCustomerInStoreReference(customersInStore);

            // Set point of purchase in lower right corner of shop
            PointOfPurchase.Instance.SetNodeReference(StoreMapLayout[StoreMapLayout.Count - 1]);

            GenericList<GenericGraphNode> n = SearchStuff(StoreMapLayout[4], StoreMapLayout[7]);

            // PRINT LIGE LISTEN UD :) o.O
            for (int i = 0; i < n.Count; i++)
            {
                Console.WriteLine($"Moving from node at {n[i].x}, {n[i].y}");
            }

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
                        node.x = j;
                        node.y = i;
                    }
                    else if (i == 0 && j > 0)
                    {
                        GenericGraphNode node = map.Add(map[j - 1]); // add first row
                        node.x = j;
                        node.y = i;
                    }
                    else
                    {
                        int upperIndex = ((i - 1) * mapWidth) + j;

                        if (j == 0)
                        {
                            GenericGraphNode node = map.Add(map[upperIndex]);
                            node.x = j;
                            node.y = i;
                        }
                        else if (j > 0)
                        {
                            GenericGraphNode[] targetNodes = new GenericGraphNode[2]
                            {
                                map[upperIndex],
                                map[thisIndex -1]
                            };

                            GenericGraphNode node = map.Add(targetNodes);
                            node.x = j;
                            node.y = i;
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


        static GenericList<GenericGraphNode> SearchStuff (GenericGraphNode source, GenericGraphNode goal)
        {
            // Tilføj start noden til søgeliste
            GenericList<GenericGraphNode> searchList = new GenericList<GenericGraphNode>();
            searchList.Add(source);

            // Hjælpelister til at holde styr på, hvad der er besøgt allerede
            // Bruges også til at beregne, hvilke noder som er sammenhængende til korteste path
            GenericList<GenericGraphNode> cameFromKey = new GenericList<GenericGraphNode>();
            GenericList<GenericGraphNode> cameFromValue = new GenericList<GenericGraphNode>();

            // Listen over, hvilken path der skal returneres
            GenericList<GenericGraphNode> pathList = new GenericList<GenericGraphNode>();
            int iterations = 0;
            while (searchList.Count > 0)
            {
                GenericGraphNode current = searchList[0];
                searchList.Remove(current);

                foreach (GenericGraphEdge edge in current.Edges)
                {
                    iterations += 1;

                    // Find næste node fra hver edge, som ikke er den samme som Current
                    GenericGraphNode nextNode = (current.Equals(edge.FromNode)) ? edge.ToNode : edge.FromNode;

                    if (current.Equals(goal))
                    {
                        pathList.Add(current);
                        while(cameFromKey.IndexOf(pathList.Last()) > -1)
                        {
                            int keyIndex = cameFromKey.IndexOf(pathList.Last());
                            pathList.Add(cameFromValue[keyIndex]);
                        }
                        Console.WriteLine("ITERATIONS: " + iterations);
                        return pathList;
                    }

                    // VIRKER MÅSKE IKKE HELT?
                    // HVORFOR I ALVERDEN ER DER SÅ MANGE FANDENS ITERATIONER?!?!
                    if (! cameFromValue.Contains(nextNode))
                    {
                        searchList.Add(nextNode);
                        cameFromKey.Add(nextNode);
                        cameFromValue.Add(current);
                    }
                }
            }

            return null;
        }
    }
}
