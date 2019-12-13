using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingConsoleProject.GameClasses
{
    public class Customer
    {
        public string Name { get => name; }

        string name;
        
        public Customer(string name)
        {
            this.name = name;
        }
    }
}
