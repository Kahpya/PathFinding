using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfindingConsoleProject.DataStructures;

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
                    
                }
                else
                {
                    shoppingList.Remove(nextItem);

                    Console.ForegroundColor = Program.TextColorCustomerPickUpItem;
                    Console.WriteLine(this.name + " Picked up " + nextItem.ItemType.ToString() + " from an aisle.");
                }                
            }
            else
            {
                PointOfPurchase.Instance.SetCustomerInQueue(this);
            }
        }
    }
}
