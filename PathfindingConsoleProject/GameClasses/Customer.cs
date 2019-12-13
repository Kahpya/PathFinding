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

        private string name;

        private GenericList<Item> shoppingList;
        private GenericList<Item> shoppingBasket;
        
        public Customer(string name, GenericList<Item> items)
        {
            this.name = name;
            this.shoppingList = items;
            this.shoppingBasket = new GenericList<Item>();
        }

        public void MoveTowardsNextNode()
        {
            // TODO: Create graphs stuff
            
            // Temporary stuff without pathfinding:
            if (shoppingList.Count > 0)
            {
                Item nextItem = shoppingList[0];
                shoppingList.Remove(nextItem);

                Console.WriteLine(this.name + " Picked up " + nextItem.ItemType.ToString() + "from an aisle.");
            }
            else
            {
                PointOfPurchase.Instance.SetCustomerInQueue(this);
            }
        }
    }
}
