using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PathfindingConsoleProject.DataStructures;

namespace PathfindingConsoleProject.GameClasses
{
    public class CustomerFactory
    {
        public bool CanCreateNewCustomer
        {
            get => availableNames.Count > 0;
        }

        GenericList<string> availableNames;
        int maxNumberOfItemsInShoppingList;

        public CustomerFactory(int maxNumberOfItemsInShoppingList)
        {
            this.maxNumberOfItemsInShoppingList = maxNumberOfItemsInShoppingList +1;
            this.availableNames = new GenericList<string>();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("Assets/first-names.txt"))
                {
                    // Read the stream to a string, and write the string to the console.

                    string line = string.Empty;

                    while((line = sr.ReadLine()) != null)
                    {
                        availableNames.Add(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public Customer Create()
        {
            if (! CanCreateNewCustomer)
            {
                return null;
            }

            int randomIndex = Program.RandomNumberGenerator.Next(0, availableNames.Count);
            int randomItemLength = Program.RandomNumberGenerator.Next(1, maxNumberOfItemsInShoppingList + 1); // +1 to allow inclusive max

            string name = availableNames[randomIndex];
            GenericList<Item> shoppingBasket = new GenericList<Item>();

            for (int i = 0; i < randomItemLength; i++)
            {
                // Find random item from enum length
                ItemType randomItemType = (ItemType)Program.RandomNumberGenerator.Next(0, Enum.GetNames(typeof(ItemType)).Length);
                shoppingBasket.Add(new Item(randomItemType));
            }

            availableNames.Remove(name);

            Console.ForegroundColor = Program.TextColorCustomerEnterStore;
            Console.WriteLine(name + " has entered the store.");

            // Create new customer who is starting at the same position as the kasseapparat
            return new Customer(name, shoppingBasket, PointOfPurchase.Instance.StoreLocation);
        }
    }
}
