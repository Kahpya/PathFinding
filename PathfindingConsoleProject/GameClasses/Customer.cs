using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfindingConsoleProject.DataStructures;
using PathfindingConsoleProject.Algorithms;

namespace PathfindingConsoleProject.GameClasses
{
    public class Customer
    {
        public string Name { get => name; }
        public GenericGraphNode CustomerLocation { get => customerLocation; }

        private string name;

        private GenericList<Item> shoppingList;
        private GenericList<Item> shoppingBasket;
        private GenericGraphNode customerLocation;

        private GenericList<GenericGraphNode> currentPath;
        
        public Customer(string name, GenericList<Item> items, GenericGraphNode startLocation)
        {
            this.name = name;
            this.shoppingList = items;
            this.shoppingBasket = new GenericList<Item>();
            this.customerLocation = startLocation;
        }

        public void MoveTowardsNextNode()
        {            
            // Temporary stuff without pathfinding:
            if (shoppingList.Count > 0)
            {
                Item nextItem = shoppingList[0];
                if (! this.customerLocation.Equals(nextItem.ItemLocation))
                {
                    if (this.currentPath == null)
                    {
                        this.currentPath = BreathFirstSeach.FindPathBetweenNodes(this.customerLocation, nextItem.ItemLocation);

                        Console.ForegroundColor = Program.TextColorCustomerCalculatedPath;
                        Console.Write($"{this.name} calculated shortest path from ");

                        Console.ForegroundColor = Program.TextColorCoordinates;
                        Console.Write($"[{ this.customerLocation.X}, { this.customerLocation.Y}]");

                        Console.ForegroundColor = Program.TextColorCustomerCalculatedPath;
                        Console.Write($" to {nextItem.ItemType.ToString()} at ");

                        Console.ForegroundColor = Program.TextColorCoordinates;
                        Console.Write($"[{this.currentPath[0].X}, {this.currentPath[0].Y}]");

                        Console.ForegroundColor = Program.TextColorCustomerCalculatedPath;
                        Console.Write(" with a distance of ");

                        Console.ForegroundColor = Program.TextColorCoordinates;
                        Console.WriteLine($"{this.currentPath.Count -1}");

                        this.currentPath.Remove(this.currentPath[this.currentPath.Count - 1]);
                    }
                    
                    GenericGraphNode nextStep = this.currentPath[this.currentPath.Count-1];
                    this.currentPath.Remove(nextStep);

                    PrintMovementToScreen(customerLocation, nextStep, nextItem.ItemLocation, nextItem.ItemType.ToString());
                    
                    this.customerLocation = nextStep;
                }
                else
                {
                    shoppingList.Remove(nextItem);
                    currentPath = null;
                    Console.ForegroundColor = Program.TextColorCustomerPickUpItem;
                    Console.WriteLine(this.name + " Picked up " + nextItem.ItemType.ToString() + " from an aisle.");
                }                
            }
            else
            {
                if (!this.customerLocation.Equals(PointOfPurchase.Instance.StoreLocation))
                {
                    if (this.currentPath == null)
                    {
                        this.currentPath = BreathFirstSeach.FindPathBetweenNodes(this.customerLocation, PointOfPurchase.Instance.StoreLocation);

                        Console.ForegroundColor = Program.TextColorCustomerCalculatedPath;
                        Console.Write($"{this.name} calculated shortest path from ");

                        Console.ForegroundColor = Program.TextColorCoordinates;
                        Console.Write($"[{ this.customerLocation.X}, { this.customerLocation.Y}]");

                        Console.ForegroundColor = Program.TextColorCustomerCalculatedPath;
                        Console.Write(" to Point of Sale at ");

                        Console.ForegroundColor = Program.TextColorCoordinates;
                        Console.Write($"[{this.currentPath[0].X}, {this.currentPath[0].Y}]");

                        Console.ForegroundColor = Program.TextColorCustomerCalculatedPath;
                        Console.Write(" with a distance of ");

                        Console.ForegroundColor = Program.TextColorCoordinates;
                        Console.WriteLine($"{this.currentPath.Count - 1}");

                        this.currentPath.Remove(this.currentPath[this.currentPath.Count - 1]);
                    }

                    GenericGraphNode nextStep = this.currentPath[this.currentPath.Count - 1];
                    this.currentPath.Remove(nextStep);

                    PrintMovementToScreen(customerLocation, nextStep, PointOfPurchase.Instance.StoreLocation, "Point of Sale");

                    this.customerLocation = nextStep;
                }
                else
                {
                    PointOfPurchase.Instance.SetCustomerInQueue(this);
                }
            }
        }

        private void PrintMovementToScreen(GenericGraphNode from, GenericGraphNode to, GenericGraphNode destination, string destinationName)
        {
            Console.ForegroundColor = Program.TextColorCustomerMoves;
            Console.Write($"{this.name} moved from ");

            Console.ForegroundColor = Program.TextColorCoordinates;
            Console.Write($"[{ from.X}, { from.Y}]");

            Console.ForegroundColor = Program.TextColorCustomerMoves;
            Console.Write(" to ");

            Console.ForegroundColor = Program.TextColorCoordinates;
            Console.Write($"[{to.X}, {to.Y}]");

            Console.ForegroundColor = Program.TextColorCustomerMoves;
            Console.Write($" towards {destinationName} at ");

            Console.ForegroundColor = Program.TextColorCoordinates;
            Console.WriteLine($"[{destination.X}, {destination.Y}]");
        }
    }
}
