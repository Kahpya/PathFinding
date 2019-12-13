using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfindingConsoleProject.DataStructures;
using PathfindingConsoleProject.GameClasses;

namespace PathfindingConsoleProject.GameClasses
{
    public class PointOfPurchase
    {
        public static PointOfPurchase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PointOfPurchase();
                }
                return instance;
            }
        }

        private static PointOfPurchase instance;
        private GenericList<Customer> customersInStore;

        private GenericList<Customer> customersInQueue;

        private PointOfPurchase()
        {
            customersInQueue = new GenericList<Customer>();
        }

        public void SetCustomerInStoreReference(GenericList<Customer> customersInStore)
        {
            this.customersInStore = customersInStore;
        }

        public void SetCustomerInQueue(Customer customer)
        {
            this.customersInStore.Remove(customer);
            this.customersInQueue.Add(customer);
        }

        public void ParseNextCustomerInQueue()
        {
            if (this.customersInQueue.Count < 0)
            {
                Customer customer = this.customersInQueue[0];
                this.customersInQueue.Remove(customer);

                Console.WriteLine(customer.Name + " paid and left the store.");
            }
        }
    }
}
