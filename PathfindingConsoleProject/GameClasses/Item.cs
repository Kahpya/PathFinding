using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfindingConsoleProject.DataStructures;

namespace PathfindingConsoleProject.GameClasses
{
    public enum ItemType { Milk, Bread, Coffee, ShowerHead, Beef, Tobacco }
    public struct Item
    {
        public ItemType ItemType { get => itemType; }
        public GenericGraphNode ItemLocation { get => itemLocation; }

        private ItemType itemType;
        private GenericGraphNode itemLocation;

        public Item(ItemType type)
        {
            int nodeIndex = Program.ItemLocationGridArray[(int)type];

            this.itemType = type;
            this.itemLocation = Program.StoreMapLayout[nodeIndex];
        }
    }
}
