using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderingSystem
{
    internal class Order
    {
        public int id { get; set; }
        public int tableId { get; set; }
        public Dish[] dishes { get; set; }
        public string comment { get; set; }
        public TimeSpan creationTime { get; set; }
        public int waiter { get; set; }
        public TimeSpan? closedTime { get; set; }
        public double? total { get; set; }
        
        public Order(int id, int tableId, Dish[] dishes, string comment, int waiter)
        {
            this.id = id; // Получение нового ID заказа
            this.tableId = tableId;
            this.dishes = dishes;
            this.comment = comment;
            this.creationTime = DateTime.Now.TimeOfDay; // Текущее время
            this.waiter = waiter;
            //this.total = CalculateTotal(); // Вычисление общей стоимости
        }

        // Метод для изменения заказа
        public void edit(Dish[] newDishes, string newComment)
        {
            this.dishes = newDishes;
            this.comment = newComment;
            this.total = CalculateTotal(); // Пересчет общей стоимости
        }

        // Метод для вывода информации о заказе
        public void printInfo()
        {
            Console.WriteLine($"Заказ {id}:");
            Console.WriteLine($"Номер стола: {tableId}");
            Console.WriteLine($"Время создания: {creationTime}");
            Console.WriteLine($"Официант: {waiter}");
            Console.WriteLine($"Комментарий: {comment}");
            Console.WriteLine($"Блюда:");
            foreach (Dish dish in dishes)
            {
                dish.printInfo();
                Console.WriteLine();
            }
            Console.WriteLine($"Итого: {total = CalculateTotal()}");
            if (closedTime != null)
            {
                Console.WriteLine($"Время закрытия: {closedTime}");
            }
        }

        // Метод для закрытия заказа
        public void CloseOrder()
        {
            if (closedTime == null)
            {
                this.closedTime = DateTime.Now.TimeOfDay;
                total = CalculateTotal();
                Console.WriteLine("Заказ успешно закрыт.");
            }
            else
            {
                Console.WriteLine("Заказ уже закрыт.");
            }
        }

        // Метод для вывода чека
        public void printReceipt()
        {
            if (closedTime != null)
            {
                var dishCounts = dishes.GroupBy(d => d.name)
                             .ToDictionary(g => g.Key, g => g.Count());

                Console.WriteLine($"Столик: {tableId}");
                Console.WriteLine($"Официант: {waiter}");
                Console.WriteLine($"Период обслуживания: с {creationTime} по {closedTime}");
                Console.WriteLine();

                var categoryTotals = new Dictionary<Category, double>();

                foreach (var category in Enum.GetValues(typeof(Category)).Cast<Category>())
                {
                    var dishesInCategory = dishes.Where(d => d.category == category).GroupBy(x => x.name).Select(x => new { Name = x.Key, Count = x.Count(), Price = x.First().price }).ToArray();

                    if (dishesInCategory.Length > 0)
                    {
                        Console.WriteLine($"{category}:");
                        double categorySum = 0;
                        foreach (var dish in dishesInCategory)
                        {
                            double itemTotal = dish.Count * dish.Price;
                            Console.WriteLine($"{dish.Name}\t\t{dish.Count}*{dish.Price}={itemTotal}");
                            categorySum += itemTotal;
                        }
                        Console.WriteLine($"\t\tПод-итог категории: {categorySum}");
                        categoryTotals[category] = categorySum;
                        Console.WriteLine();
                    }
                }

                double grandTotal = categoryTotals.Sum(x => x.Value);
                Console.WriteLine($"Итог счета: {grandTotal}");
                Console.WriteLine("*************************************************");
            }
            else
            {
                Console.WriteLine("Заказ не закрыт, чек не может быть выведен.");
            }
        }

        // Вспомогательный метод для вычисления общей стоимости
        public double CalculateTotal()
        {
            double totalCost = 0;
            foreach (Dish dish in dishes)
            {
                totalCost += dish.price;
            }
            return totalCost;
        }
    }

}
   
