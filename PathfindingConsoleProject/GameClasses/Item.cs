using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingConsoleProject.GameClasses
{
    public enum ItemType { Milk, Bread, Coffee, ShowerHead, Beef, Tobacco }
    public class Item
    {
        public ItemType ItemType { get => itemType; }

        private ItemType itemType;

        public Item(ItemType type)
        {
            this.itemType = type;
        }
    }
}
