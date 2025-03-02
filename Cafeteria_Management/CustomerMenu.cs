using DynamicArray;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria_Management
{
    public static class CustomerMenu
    {
        public static DArray<Order> allOrders = new DArray<Order>();

        public static void DisplayMenu(Menu menu)
        {
            DArray<MenuItem> order = new DArray<MenuItem>();
            double totalAmount = 0;

            while (true)
            {
                Console.WriteLine("--- Customer Section ---");
                Console.WriteLine("\n1. View Menu");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. View Order");
                Console.WriteLine("4. Delete Item from Order");
                Console.WriteLine("5. View Bill");
                Console.WriteLine("6. Exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        ViewOrderMenu(menu);
                        break;
                    case 2:
                        Console.Clear();
                        PlaceOrder(menu, order, ref totalAmount);
                        break;
                    case 3:
                        ViewOrder(order);
                        break;
                    case 4:
                        Console.Clear();
                        DeleteItemFromOrder(order, ref totalAmount);
                        break;
                    case 5:
                        ViewBill(order, totalAmount);
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Thank you for visiting!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        private static void ViewOrderMenu(Menu menu)
        {
            Console.WriteLine("--- Menu ---\n");

            menu.FoodCategory.DisplayMenu();
            menu.DrinkCategory.DisplayMenu();
        }

        private static void PlaceOrder(Menu menu, DArray<MenuItem> order, ref double totalAmount)
        {
            while (true)
            {
                ViewOrderMenu(menu);

                Console.WriteLine("Enter category (Food - F/Drink - D): ");
                string category = Console.ReadLine();
                MenuCategory selectedCategory;

                if (category != "D" && category != "F" && category != "d" && category != "f")
                {
                    Console.WriteLine("Invalid category.");
                    return;
                }
                else if (category == "F" || category == "f")
                {
                    selectedCategory = menu.FoodCategory;
                }
                else
                {
                    selectedCategory = menu.DrinkCategory;
                }

                Console.WriteLine("Enter the number of the item you want to order:");
                int itemNumber = Convert.ToInt32(Console.ReadLine()) - 1;

                if (itemNumber >= 0 && itemNumber < selectedCategory.MenuItems.Count)
                {
                    var item = selectedCategory.MenuItems.Get(itemNumber);

                    Console.WriteLine($"Enter the quantity of {item.Name} you want to order:");
                    int quantity;

                    if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        return;
                    }

                    for (int i = 0; i < quantity; i++)
                    {
                        order.Add(new MenuItem(item.Name, item.Price));
                        totalAmount += item.Price;
                    }

                    Console.WriteLine($"{quantity}x {item.Name} have been added to your order.");
                }
                else
                {
                    Console.WriteLine("Invalid item number.");
                    return;
                }

                Console.WriteLine("\nWould you like to order something else? (Y/N): ");
                string continueOrder = Console.ReadLine().ToUpper();
                Console.Clear();

                if (continueOrder != "Y")
                {
                    allOrders.Add(new Order(order, totalAmount));
                    Console.WriteLine("Your order has been placed successfully!\n");
                    return;
                }
            }
        }

        public class MenuItemWithQuantity
        {
            public MenuItem Item { get; set; }
            public int Quantity { get; set; }
        }

        static public DArray<MenuItemWithQuantity> GroupItemsByName(DArray<MenuItem> order)
        {
            DArray<MenuItemWithQuantity> groupedItems = new DArray<MenuItemWithQuantity>();

            for (int i = 0; i < order.Count; i++)
            {
                var item = order.Get(i);
                bool itemExists = false;
                for (int j = 0; j < groupedItems.Count; j++)
                {
                    if (groupedItems.Get(j).Item.Name == item.Name)
                    {
                        groupedItems.Get(j).Quantity++;
                        itemExists = true;
                        break;
                    }
                }

                if (!itemExists)
                {
                    groupedItems.Add(new MenuItemWithQuantity
                    {
                        Item = item,
                        Quantity = 1
                    });
                }
            }
            return groupedItems;
        }

        private static void ViewOrder(DArray<MenuItem> order)
        {
            Console.Clear();
            Console.WriteLine("--- Your Order ---");
            if (order.Count == 0)
            {
                Console.WriteLine("No items in your order.");
            }
            else
            {
                DArray<MenuItemWithQuantity> groupedItems = GroupItemsByName(order);

                int index = 1;
                for (int i = 0; i < groupedItems.Count; i++)
                {
                    var groupedItem = groupedItems.Get(i);
                    Console.WriteLine($"{i + 1}. {groupedItem.Quantity}x {groupedItem.Item.Name} - Rs.{groupedItem.Item.Price * groupedItem.Quantity}");
                }
            }
            Console.WriteLine();
        }

        private static void DeleteItemFromOrder(DArray<MenuItem> order, ref double totalAmount)
        {
            ViewOrder(order);

            Console.WriteLine("Enter the number of the item you want to delete from the order:");
            int itemNumber = Convert.ToInt32(Console.ReadLine()) - 1;

            if (itemNumber >= 0)
            {
                DArray<MenuItemWithQuantity> groupedItems = GroupItemsByName(order);

                if (itemNumber < groupedItems.Count)
                {
                    var itemToDelete = groupedItems.Get(itemNumber);
                    Console.WriteLine($"You are about to delete {itemToDelete.Item.Name}!");

                    Console.WriteLine($"How many of '{itemToDelete.Item.Name}' would you like to remove? (Max: {itemToDelete.Quantity}):");
                    int quantityToRemove = Convert.ToInt32(Console.ReadLine());

                    if (quantityToRemove > 0 && quantityToRemove <= itemToDelete.Quantity)
                    {
                        int quantityRemoved = 0;

                        for (int i = 0; i < order.Count && quantityRemoved < quantityToRemove; i++)
                        {
                            var currentItem = order.Get(i);
                            if (currentItem.Name == itemToDelete.Item.Name)
                            {
                                order.RemoveAt(i);
                                totalAmount -= currentItem.Price;
                                quantityRemoved++;

                                i--;
                            }
                        }

                        Console.WriteLine($"{quantityToRemove} of '{itemToDelete.Item.Name}' have been removed from your order.");
                    }
                    else
                    {
                        Console.WriteLine($"Invalid quantity. You can only remove between 1 and {itemToDelete.Quantity} of {itemToDelete.Item.Name}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid item number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid item number.");
            }
        }



        private static void ViewBill(DArray<MenuItem> order, double totalAmount)
        {
            ViewOrder(order);

            Console.WriteLine($"Total amount: Rs.{totalAmount}\n");
        }
    }
}
