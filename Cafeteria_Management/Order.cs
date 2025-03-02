using DynamicArray;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria_Management
{
    public class Order
    {
        public DArray<MenuItem> Items { get; set; }
        public double TotalAmount { get; set; }

        public Order(DArray<MenuItem> items, double totalAmount)
        {
            Items = items;
            TotalAmount = totalAmount;
        }
    }
}
