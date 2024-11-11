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
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Чек №{id}");
                Console.WriteLine($"Веремя открытия: {creationTime}");
                Console.WriteLine($"Время закрытия: {closedTime}");
                Console.WriteLine("-----------------------------------");
                foreach (Dish dish in dishes)
                {
                    Console.WriteLine($"{dish.name} x {dish.weight}г - {dish.price}");
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Итого: {total}");
                Console.WriteLine("-----------------------------------");
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
   
