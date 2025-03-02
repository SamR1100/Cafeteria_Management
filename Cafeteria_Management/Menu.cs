using DynamicArray;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria_Management;

public class MenuItem
{
    public string Name { get; set; }
    public double Price { get; set; }
    public MenuItem(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

public class MenuCategory
{
    public string CategoryName { get; set; }
    public DArray<MenuItem> MenuItems { get; set; }

    public MenuCategory(string categoryName)
    {
        CategoryName = categoryName;
        MenuItems = new DArray<MenuItem>();
    }

    public void AddItem(MenuItem item)
    {
        MenuItems.Add(item);
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < MenuItems.Count; i++)
        {
            var item = MenuItems.Get(i);
            if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                MenuItems.RemoveAt(i);
                Console.WriteLine($"{item.Name} has been removed from {CategoryName}.");
                return;
            }
        }
        Console.WriteLine($"Item {itemName} not found in {CategoryName}.");
    }

    public void DisplayMenu()
    {
        Console.WriteLine($"--- {CategoryName} Menu ---");
        for (int i = 0; i < MenuItems.Count; i++)
        {
            var item = MenuItems.Get(i);
            Console.WriteLine($"{i + 1}. {item.Name} - Rs.{item.Price}");
        }
        Console.WriteLine();
    }
}

public class Menu
{
    public MenuCategory FoodCategory { get; set; }
    public MenuCategory DrinkCategory { get; set; }

    public Menu()
    {
        FoodCategory = new MenuCategory("Food");
        DrinkCategory = new MenuCategory("Drinks");

        FoodCategory.AddItem(new MenuItem("Burger", 850));
        FoodCategory.AddItem(new MenuItem("Pizza", 1200));
        FoodCategory.AddItem(new MenuItem("Pasta", 450));
        FoodCategory.AddItem(new MenuItem("Fries", 250));
        FoodCategory.AddItem(new MenuItem("Sandwich", 120));

        DrinkCategory.AddItem(new MenuItem("Soda", 150));
        DrinkCategory.AddItem(new MenuItem("Juice", 120));
        DrinkCategory.AddItem(new MenuItem("Coffee", 70));
        DrinkCategory.AddItem(new MenuItem("Tea", 40));
        DrinkCategory.AddItem(new MenuItem("Milkshake", 290));
    }

}
