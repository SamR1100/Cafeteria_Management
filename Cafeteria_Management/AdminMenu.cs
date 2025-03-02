using DynamicArray;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria_Management;

public static class AdminMenu
{
    public static Menu DisplayMenu(Menu menu)
    {
        double totalSales = 0;

        while (true)
        {
            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("1. Add Item to Menu");
            Console.WriteLine("2. Remove Item from Menu");
            Console.WriteLine("3. Update Menu");
            Console.WriteLine("4. View Menu");
            Console.WriteLine("5. View Order List");
            Console.WriteLine("6. Logout");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AddItemToMenu(menu);
                    break;
                case 2:
                    RemoveItemFromMenu(menu);
                    break;
                case 3:
                    UpdateMenu(menu);
                    break;
                case 4:
                    Console.Clear();
                    ViewOrderMenu(menu);
                    break;
                case 5:
                    Console.Clear();
                    ViewOrders();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Logging out...");
                    return menu;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private static void AddItemToMenu(Menu menu)
    {
        Console.WriteLine("Enter category (Food - F/Drink - D): ");
        string category = Console.ReadLine();

        if (category != "D" && category != "F" && category != "d" && category != "f")
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        Console.WriteLine("Enter item name: ");
        string name = Console.ReadLine();
        bool itemExists = false;
        string categoryName = "";

        if (category == "F" || category == "f")
        {
            categoryName = "Food";
            for (int i = 0; i < menu.FoodCategory.MenuItems.Count; i++)
            {
                var item = menu.FoodCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemExists = true;
                    break;
                }
            }
        }
        else
        {
            categoryName = "Drinks";
            for (int i = 0; i < menu.DrinkCategory.MenuItems.Count; i++)
            {
                var item = menu.DrinkCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    itemExists = true;
                    break;
                }
            }
        }

        if (itemExists)
        {
            Console.WriteLine($"The item '{name}' already exists in the {categoryName} menu.");
            return;
        }

        Console.WriteLine("Enter item price: ");
        string priceInput = Console.ReadLine();

        if (!double.TryParse(priceInput, out double price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        MenuItem newItem = new MenuItem(name, price);

        if (category == "F" || category == "f")
        {
            menu.FoodCategory.AddItem(newItem);
            Console.WriteLine("Item added to Food menu.");
        }
        else
        {
            menu.DrinkCategory.AddItem(newItem);
            Console.WriteLine("Item added to Drinks menu.");
        }
    }

    private static void RemoveItemFromMenu(Menu menu)
    {
        Console.WriteLine("\nEnter category (Food - F/Drink - D): ");
        string category = Console.ReadLine();

        if (category != "D" && category != "F" && category != "d" && category != "f")
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        Console.WriteLine("Enter item name to remove: ");
        string name = Console.ReadLine();

        if (category == "F" || category == "f")
        {
            menu.FoodCategory.RemoveItem(name);
        }
        else
        {
            menu.DrinkCategory.RemoveItem(name);
        }
    }

    private static void UpdateMenu(Menu menu)
    {
        Console.WriteLine("\nEnter category (Food - F/Drink - D): ");
        string category = Console.ReadLine();

        if (category != "D" && category != "F" && category != "d" && category != "f")
        {
            Console.WriteLine("Invalid category.");
            return;
        }

        Console.WriteLine("Enter item name to update: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter new price: ");
        string newPrice = Console.ReadLine();

        if (!double.TryParse(newPrice, out double price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }


        if (category == "F" || category == "f")
        {
            for (int i = 0; i < menu.FoodCategory.MenuItems.Count; i++)
            {
                var item = menu.FoodCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    item.Price = price;
                    Console.WriteLine($"The price of {item.Name} has been updated to Rs.{price}.");
                    return;
                }
            }
            Console.WriteLine($"Item {name} not found in Food menu.");
        }
        else
        {
            for (int i = 0; i < menu.DrinkCategory.MenuItems.Count; i++)
            {
                var item = menu.DrinkCategory.MenuItems.Get(i);
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    item.Price = price;
                    Console.WriteLine($"The price of {item.Name} has been updated to Rs.{price}.");
                    return;
                }
            }
            Console.WriteLine($"Item {name} not found in Drinks menu.");
        }
    }

    private static void ViewOrderMenu(Menu menu)
    {
        Console.WriteLine("--- Menu ---\n");

        menu.FoodCategory.DisplayMenu();
        menu.DrinkCategory.DisplayMenu();
    }
    private static void ViewOrders()
    {
        Console.WriteLine("--- Customer Orders ---");

        if (CustomerMenu.allOrders.Count == 0)
        {
            Console.WriteLine("No orders placed yet.");
            return;
        }

        for (int i = 0; i < CustomerMenu.allOrders.Count; i++)
        {
            var order = CustomerMenu.allOrders.Get(i);
            Console.WriteLine($"\nOrder {i + 1}:");
            DArray<CustomerMenu.MenuItemWithQuantity> groupedItems = new DArray<CustomerMenu.MenuItemWithQuantity>();

            for (int j = 0; j < order.Items.Count; j++)
            {
                var item = order.Items.Get(j);
                bool itemExists = false;

                for (int k = 0; k < groupedItems.Count; k++)
                {
                    if (groupedItems.Get(k).Item.Name == item.Name)
                    {
                        groupedItems.Get(k).Quantity++;
                        itemExists = true;
                        break;
                    }
                }
                if (!itemExists)
                {
                    groupedItems.Add(new CustomerMenu.MenuItemWithQuantity
                    {
                        Item = item,
                        Quantity = 1
                    });
                }
            }

            int index = 1;
            for (int m = 0; m < groupedItems.Count; m++)
            {
                var groupedItem = groupedItems.Get(m);
                Console.WriteLine($"{index}. {groupedItem.Quantity}x {groupedItem.Item.Name} - Rs.{groupedItem.Item.Price * groupedItem.Quantity}");
                index++;
            }
            Console.WriteLine($"Total Amount : Rs.{order.TotalAmount}");
        }
    }
}


