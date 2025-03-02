using Cafeteria_Management;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Cafeteria Management System");
        Console.WriteLine("===========================");

        Menu menu = new Menu();

        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Admin Login");
            Console.WriteLine("2. Customer Section");
            Console.WriteLine("3. Exit");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.Clear();
                AdminSection(ref menu);
            }
            else if (choice == 2)
            {
                Console.Clear();
                CustomerSection(menu);
            }
            else if (choice == 3)
            {
                Console.Clear();
                Console.WriteLine("Exiting the system. Goodbye!");
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice, try again.");
            }
        }
    }

    static void AdminSection(ref Menu menu)
    {
        string adminUsername = "admin";
        string adminPassword = "123";

        Console.WriteLine("--- Admin Login ---");
        Console.Write("\nUsername: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (username == adminUsername && password == adminPassword)
        {
            Console.WriteLine("Login successful!");
            menu = AdminMenu.DisplayMenu(menu);

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid credentials.");
        }
    }

    static void CustomerSection(Menu menu)
    {
        CustomerMenu.DisplayMenu(menu);
    }

}

