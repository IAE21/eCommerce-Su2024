using IMM.Models;
using IMM.Services;

namespace eCommerce
{
    internal class Program
    {
        static int mainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("1 - Inventory Management");
            Console.WriteLine("2 - Shop");
            Console.WriteLine("3 - Exit\n");
            Console.Write("Enter option number to proceed: ");
            int.TryParse(Console.ReadLine() ?? "0", out int choice);
            return choice;
        }

        static int inventoryMenu()
        {
            Console.WriteLine("Inventory Management System:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("1 - Create New Item");
            Console.WriteLine("2 - Display Current Items");
            Console.WriteLine("3 - Edit Current Inventory");
            Console.WriteLine("4 - Delete Item");
            Console.WriteLine("5 - Return to Main Menu\n");
            int.TryParse(Console.ReadLine() ?? "0", out int choice);
            return choice;
        }

        static int editItemMenu()
        {
            Console.WriteLine("\n1 - Edit Name");
            Console.WriteLine("2 - Edit Description");
            Console.WriteLine("3 - Edit Price");
            Console.WriteLine("4 - Edit Stock");
            Console.WriteLine("5 - Done\n");
            int.TryParse(Console.ReadLine() ?? "0", out int choice);
            return choice;
        }

        static int shopMenu()
        {
            Console.WriteLine("Shop:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("1 - Display Available Items");
            Console.WriteLine("2 - Display Items in Cart");
            Console.WriteLine("3 - Add Item to Cart");
            Console.WriteLine("4 - Remove Item from Cart");
            Console.WriteLine("5 - Checkout");
            Console.WriteLine("6 - Return to Main Menu\n");
            int.TryParse(Console.ReadLine() ?? "0", out int choice);
            return choice;
        }

        static void Main(string[] args)
        {
            int option = 0;
            var inventorySvc = InventoryServiceProxy.Current;
            var cartSvc = CartServiceProxy.Current;

            while (option != 3)
            {
                option = mainMenu();

                if (option == 1)
                {
                    int invoption = 0;
                    while (invoption != 5)
                    {
                        invoption = inventoryMenu();

                        if (invoption == 1)
                        {
                            Item item = new Item();
                            Console.Write("Enter Item Name: ");
                            string name = Console.ReadLine() ?? string.Empty;
                            if (name == string.Empty)
                            {
                                while (name == string.Empty)
                                {
                                    Console.WriteLine("Please enter a valid Item Name.\n");
                                    Console.Write("Enter Item Name: ");
                                    name = Console.ReadLine() ?? string.Empty;
                                }
                            }
                            item.Name = name;
                            Console.WriteLine("Enter Item Description:");
                            string desc = Console.ReadLine() ?? string.Empty;
                            if (desc == string.Empty)
                            {
                                while (desc == string.Empty)
                                {
                                    Console.WriteLine("Please enter a valid Item Description.\n");
                                    Console.WriteLine("Enter Item Description: ");
                                    desc = Console.ReadLine() ?? string.Empty;
                                }
                            }
                            item.Description = desc;
                            Console.Write("Enter Item Price: ");
                            decimal price;
                            decimal.TryParse(Console.ReadLine() ?? "0", out price);
                            item.Price = price;
                            Console.Write("Enter Number of Item in Stock: ");
                            int stock;
                            int.TryParse(Console.ReadLine() ?? "0", out stock);
                            item.Stock = stock;

                            inventorySvc.AddOrUpdate(item);
                            Console.WriteLine("\nItem successfully added to inventory.\n");
                        }
                        else if (invoption == 2)
                        {
                            if (inventorySvc.Items.Count == 0)
                                Console.WriteLine("\nNo items currently in inventory.\n");
                            else
                            {
                                Console.WriteLine("Current Inventory:");
                                foreach (Item i in inventorySvc.Items)
                                {
                                    Console.WriteLine("----------------------------");
                                    Console.WriteLine(i);
                                }
                                Console.WriteLine("----------------------------");
                            }
                        }
                        else if (invoption == 3)
                        {
                            if (inventorySvc.Items.Count == 0)
                                Console.WriteLine("\nError: Inventory empty.\n");
                            else
                            {
                                int editoption = 0;
                                Console.Write("Enter ID of Item to edit: ");
                                int.TryParse(Console.ReadLine() ?? "1", out int id);

                                var editItem = InventoryServiceProxy.Current.Items.FirstOrDefault(i => i.Id == id);

                                if (editItem != null)
                                {
                                    while (editoption != 5)
                                    {
                                        Console.WriteLine("----------------------------");
                                        Console.WriteLine(editItem);
                                        Console.WriteLine("----------------------------");

                                        editoption = editItemMenu();
                                        if (editoption == 1)
                                        {
                                            Console.WriteLine("Enter Item Name: ");
                                            string name = Console.ReadLine() ?? string.Empty;
                                            if (name == string.Empty)
                                            {
                                                while (name == string.Empty)
                                                {
                                                    Console.WriteLine("Please enter a valid Item Name.\n");
                                                    Console.Write("Enter Item Name: ");
                                                    name = Console.ReadLine() ?? string.Empty;
                                                }
                                            }
                                            editItem.Name = name;
                                            Console.WriteLine("\nItem Name successfully updated.\n");
                                        }
                                        else if (editoption == 2)
                                        {
                                            Console.WriteLine("Enter Item Description:");
                                            string desc = Console.ReadLine() ?? string.Empty;
                                            if (desc == string.Empty)
                                            {
                                                while (desc == string.Empty)
                                                {
                                                    Console.WriteLine("Please enter a valid Item Description.\n");
                                                    Console.WriteLine("Enter Item Description: ");
                                                    desc = Console.ReadLine() ?? string.Empty;
                                                }
                                            }
                                            editItem.Description = desc;
                                            Console.WriteLine("\nItem Description successfully updated.\n");
                                        }
                                        else if (editoption == 3)
                                        {
                                            Console.Write("Enter Item Price: ");
                                            decimal price;
                                            decimal.TryParse(Console.ReadLine() ?? "0", out price);
                                            editItem.Price = price;
                                            Console.WriteLine("\nItem Price successfully updated.\n");
                                        }
                                        else if (editoption == 4)
                                        {
                                            Console.Write("Enter Number of Item in Stock: ");
                                            int stock;
                                            int.TryParse(Console.ReadLine() ?? "0", out stock);
                                            editItem.Stock = stock;
                                            Console.WriteLine("\nItem Stock successfully updated.\n");
                                        }
                                        else if (editoption == 5)
                                        {

                                        }
                                        else
                                            Console.WriteLine("\nInvalid option.\n");
                                    }
                                }
                                else
                                    Console.WriteLine("\nItem not found in inventory.\n");
                            }
                        }
                        else if (invoption == 4)
                        {
                            if (inventorySvc.Items.Count == 0)
                                Console.WriteLine("\nError: Inventory empty.\n");
                            else
                            {
                                Console.WriteLine("Enter ID of Item to delete:");
                                int.TryParse(Console.ReadLine() ?? "1", out int id);

                                var deleteItem = inventorySvc.Items.FirstOrDefault(i => i.Id == id) ?? null;

                                if (deleteItem != null)
                                {
                                    inventorySvc.Delete(id);
                                    Console.WriteLine("\nItem successfully deleted.\n");
                                }
                                else
                                    Console.WriteLine("\nItem not found in inventory.\n");
                            }
                        }
                        else if (invoption == 5)
                        {

                        }
                        else
                            Console.WriteLine("\nInvalid option.\n");
                    }
                }
                else if (option == 2)
                {
                    int shoption = 0;
                    while (shoption != 6)
                    {
                        shoption = shopMenu();

                        if (shoption == 1)
                        {
                            if (inventorySvc.Items.Count == 0)
                                Console.WriteLine("\nNo Items available for purchase.\n");
                            else
                            {
                                foreach (Item i in inventorySvc.Items)
                                {
                                    if (i.Stock > 0)
                                    {
                                        Console.WriteLine("----------------------------");
                                        Console.WriteLine(i);
                                    }
                                }
                                Console.WriteLine("----------------------------");
                            }
                        }
                        else if (shoption == 2)
                        {
                            if (cartSvc.Cart.Contents.Count == 0)
                                Console.WriteLine("\nCart currently empty.\n");
                            else
                            {
                                foreach (Item i in cartSvc.Cart.Contents)
                                {
                                    Console.WriteLine("----------------------------");
                                    Console.WriteLine($"ID: {i.Id}");
                                    Console.WriteLine($"Name: {i.Name}");
                                    Console.WriteLine($"Description: {i.Description}");
                                    Console.WriteLine($"Price: ${i.Price}");
                                    Console.WriteLine($"Number in Cart: {i.Stock}");
                                }
                                Console.WriteLine("----------------------------");
                            }
                        }
                        else if (shoption == 3)
                        {
                            if (inventorySvc.Items.Count == 0)
                                Console.WriteLine("\nShop currently empty.\n");
                            else
                            {
                                Console.Write("Enter ID of Item to add to Cart: ");
                                int.TryParse(Console.ReadLine() ?? "1", out int id);

                                var invItem = inventorySvc.Items.FirstOrDefault(i => i.Id == id) ?? null;
                                if (invItem != null)
                                {
                                    Console.Write("Enter number of Items to add to Cart: ");
                                    int.TryParse(Console.ReadLine() ?? "1", out int num);
                                    if (num == 0)
                                    {
                                        while (num == 0)
                                        {
                                            Console.WriteLine("Please enter a valid number of Items to add to Cart.");
                                            Console.Write("Enter number of Items to add to Cart: ");
                                            int.TryParse(Console.ReadLine() ?? "1", out num);
                                        }
                                    }

                                    if (num > invItem.Stock)
                                        Console.WriteLine($"\nNot enough in stock to add {num} Items to Cart.\n");
                                    else
                                    {
                                        Item addItem = new Item()
                                        {
                                            Id = invItem.Id,
                                            Name = invItem.Name,
                                            Description = invItem.Description,
                                            Price = invItem.Price,
                                            Stock = num,
                                        };
                                        cartSvc.AddToCart(1, addItem);
                                        Console.WriteLine("\nItem successfully added to cart.\n");
                                    }
                                }
                                else
                                    Console.WriteLine("\nItem not found in inventory.\n");
                            }
                        }
                        else if (shoption == 4)
                        {
                            if (cartSvc.Cart.Contents.Count == 0)
                                Console.WriteLine("\nError: Cart empty.\n");
                            else
                            {
                                Console.Write("Enter ID of Item to remove from Cart: ");
                                int.TryParse(Console.ReadLine() ?? "1", out int id);

                                var deleteItem = cartSvc.Cart.Contents.FirstOrDefault(i => i.Id == id);

                                if (deleteItem != null)
                                {
                                    cartSvc.DeleteItem(1, deleteItem.Id);
                                    Console.WriteLine("\nItem successfully removed from Cart.\n");
                                }
                                else
                                    Console.WriteLine("\nItem not found in Cart.\n");
                            }
                        }
                        else if (shoption == 5)
                        {
                            if (cartSvc.Cart.Contents.Count == 0)
                                Console.WriteLine("\nError: Cart empty.\n");
                            else
                            {
                                decimal total = 0;
                                Console.WriteLine("Checkout:");
                                Console.WriteLine("---------------------------------");
                                int padding = cartSvc.Cart.Contents.Max(s => s.Name.Length) + 10;
                                foreach (Item i in cartSvc.Cart.Contents)
                                {
                                    string name = i.Name.ToUpper();
                                    Console.WriteLine($"{name.PadRight(padding)}{i.Price} X {i.Stock}");
                                    total += (decimal)(i.Price * i.Stock);
                                }
                                Console.WriteLine($"SUBTOTAL         {total.ToString()}");
                                Console.WriteLine($"TAX 7%           {(decimal.ToDouble(total) * 0.07).ToString("0.00")}");
                                Console.WriteLine($"TOTAL            {(decimal.ToDouble(total) * 1.07).ToString("0.00")}");
                                Console.WriteLine("---------------------------------");
                                return;
                            }
                        }
                        else if (shoption == 6)
                        {

                        }
                        else
                        {
                            Console.WriteLine("\nInvalid option.\n");
                        }
                    }
                }
                else if (option == 3)
                {

                }
                else
                {
                    Console.WriteLine("\nInvalid option.\n");
                }
            }

        }
    }
}
