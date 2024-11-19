using System.Xml.Linq;

namespace OrderingSystem
{
    class Program
    {
        static Dictionary<int, Dish> dishes = new Dictionary<int, Dish>();
        static Dictionary<int, Order> orders = new Dictionary<int, Order>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - Создать блюдо");
                Console.WriteLine("2 - Редактировать блюдо");
                Console.WriteLine("3 - Вывести информацию о блюде");
                Console.WriteLine("4 - Удалить блюдо из меню");
                Console.WriteLine("5 - Вывести меню");
                Console.WriteLine("6 - Создать заказ");
                Console.WriteLine("7 - Редактировать заказ");
                Console.WriteLine("8 - Вывести информацию о заказе");
                Console.WriteLine("9 - Закрыть заказ");
                Console.WriteLine("10 - Вывести чек");
                Console.WriteLine("11 - Подсчитать сумму закрытых заказов");
                Console.WriteLine("12 - Подсчитать сумму закрытых заказов официанта");
                Console.WriteLine("13 - Вывести статистику по количеству заказанных блюд");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        createDish();
                        break;
                    case "2":
                        editDish();
                        break;
                    case "3":
                        printDishInfo();
                        break;
                    case "4":
                        deleteDish();
                        break;
                    case "5":
                        string menu = "Меню:";
                        int countDishes;
                        int countCategories = 0;
                        if (dishes.Count == 0)
                        {
                            printMenu(in menu, out countDishes, ref countCategories, 0);
                        }
                        else
                        {
                            printMenu(in menu, out countDishes, ref countCategories);
                        }
                        Console.WriteLine($"Всего блюд: {countDishes}, Всего категорий: {countCategories}");
                        break;
                    case "6":
                        createOrder();
                        break;
                    case "7":
                        editOrder();
                        break;
                    case "8":
                        printOrderInfo();
                        break;
                    case "9":
                        closeOrder();
                        break;
                    case "10":
                        printReceipt();
                        break;
                    case "11":
                        countClosed();
                        break;
                    case "12":
                        countWaiterClosed();
                        break;
                    case "13":
                        dishStatistics();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

        }

        static void createDish()
        {
            Console.WriteLine("Введите количество блюд:");
            int dishCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < dishCount; i++)
            {
                Console.WriteLine($"Введите информацию о блюде {i + 1}:");
                Console.Write("ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Название: ");
                string name = Convert.ToString(Console.ReadLine());
                Console.Write("Состав: ");
                string composition = Convert.ToString(Console.ReadLine());
                Console.Write("Вес: ");
                string weight = Convert.ToString(Console.ReadLine());
                Console.Write("Цена: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Категория:");
                Console.WriteLine(" 1 - напитки, 2 - салаты, 3 - закуски, 4 - супы, 5 - горячее, 6 - десерты.");
                int categoryIndex = Convert.ToInt32(Console.ReadLine());
                Console.Write("Время готовки: ");
                int cookingTime = Convert.ToInt32(Console.ReadLine());
                Console.Write("Тип: ");
                string[] type = Console.ReadLine().Split(" ");

                Dish newDish = new Dish(id, name, composition, weight, price, categoryIndex, cookingTime, type);
                dishes.Add(newDish.id, newDish);
                Console.WriteLine("Блюдо успешно создано!");
            }
        }
        static void editDish()
        {
            Console.Write("Введите ID блюда: ");
            int idToEdit = Convert.ToInt32(Console.ReadLine());
            Dish dishToEdit = dishes[idToEdit];

            Console.Write("Новое название: ");
            string name = Convert.ToString(Console.ReadLine());
            Console.Write("Новый состав: ");
            string composition = Convert.ToString(Console.ReadLine());
            Console.Write("Новый вес: ");
            string weight = Convert.ToString(Console.ReadLine());
            Console.Write("Новая цена: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Новая категория:");
            Console.WriteLine(" 1 - напитки, 2 - салаты, 3 - закуски, 4 - супы, 5 - горячее, 6 - десерты.");
            int categoryIndex = Convert.ToInt32(Console.ReadLine());
            Console.Write("Новое время готовки: ");
            int cookingTime = Convert.ToInt32(Console.ReadLine());
            Console.Write("Новый тип: ");
            string[] type = Console.ReadLine().Split(" ");

            dishToEdit.edit(name, composition, weight, price, categoryIndex, cookingTime, type);
            dishes.Remove(idToEdit);
            dishes.Add(dishToEdit.id, dishToEdit);
            Console.WriteLine("Блюдо успешно отредактировано!");
        }
        static void printDishInfo()
        {
            Console.Write("Введите ID блюда: ");
            Dish dish = dishes[int.Parse(Console.ReadLine())];
            dish.printInfo();
        }

        static void deleteDish() {
            Console.Write("Введите ID блюда: ");
            dishes.Remove(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Блюдо успешно удалено из меню!");
        }

        static void printMenu(in string menu, out int count, ref int countCategories, params int[] n)
        {
            count = 0;
            if (n.Length > 0)
            {
                Console.Write("В меню нет блюд!");
                return;
            }
            Console.WriteLine(menu);
            var groupedDishes = dishes.Values.GroupBy(dish => dish.category);
            foreach (var group in groupedDishes)
            {
                countCategories++;
                Console.WriteLine($"{group.Key}:");
                foreach (var dish in group)
                {
                    count++;
                    Console.WriteLine($"ID: {dish.id} Название: {dish.name}");
                }
            }
        }

        static void createOrder()
        {
            Console.WriteLine("Введите количество заказов:");
            int orderCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < orderCount; i++)
            {
                Console.Write("Введите ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите ID стола: ");
                int tableId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите ID блюд через пробел: ");
                string input = Console.ReadLine();
                // Разделяем ввод на числа
                int[] dishIds = input.Split(' ').Select(int.Parse).ToArray();

                // Создаем массив блюд
                Dish[] selectedDishes = new Dish[dishIds.Length];

                // Извлекаем блюда из словаря и добавляем в массив
                for (int j = 0; j < dishIds.Length; j++)
                {
                    if (dishes.ContainsKey(dishIds[j]))
                    {
                        selectedDishes[j] = dishes[dishIds[j]];
                    }
                    else
                    {
                        Console.WriteLine($"Блюда с ID {dishIds[j]} не найдено.");
                    }
                }
                Console.Write("Введите комментарий: ");
                string comment = Console.ReadLine();
                Console.Write("Введите ID официанта: ");
                int waiter = Convert.ToInt32(Console.ReadLine());

                Order newOrder = new Order(id, tableId, selectedDishes, comment, waiter);
                orders.Add(id, newOrder);
                Console.WriteLine("Ваш заказ успешно создан!");
            }
        }

        static void editOrder()
        {
            Console.Write("Введите ID заказа: ");
            Order orderToEdit = orders[Convert.ToInt32(Console.ReadLine())];
            if (orderToEdit.closedTime == null)
            {
                Console.Write("Введите ID новых блюд через пробел: ");
                string input = Console.ReadLine();
                // Разделяем ввод на числа
                int[] dishIds = input.Split(' ').Select(int.Parse).ToArray();

                // Создаем массив блюд
                Dish[] selectedDishes = new Dish[dishIds.Length];

                // Извлекаем блюда из словаря и добавляем в массив
                for (int i = 0; i < dishIds.Length; i++)
                {
                    if (dishes.ContainsKey(dishIds[i]))
                    {
                        selectedDishes[i] = dishes[dishIds[i]];
                    }
                    else
                    {
                        Console.WriteLine($"Блюда с ID {dishIds[i]} не найдено.");
                    }
                }
                Console.Write("Введите новый комментарий: ");
                string comment = Console.ReadLine();
                orderToEdit.edit(selectedDishes, comment);
                Console.WriteLine("Ваш заказ успешно отредактирован!");
            }
            else
            {
                Console.WriteLine("Заказ уже закрыт, редактировать нельзя!");
            }
        }

        static void printOrderInfo()
        {
            Console.WriteLine("Введите ID заказа: ");
            Order orderToPrint = orders[Convert.ToInt32(Console.ReadLine())];
            orderToPrint.printInfo();
        }

        static void closeOrder()
        {
            Console.WriteLine("Введите ID заказа: ");
            Order orderToClose = orders[Convert.ToInt32(Console.ReadLine())];
            orderToClose.CloseOrder();
        }

        static void printReceipt()
        {
            Console.WriteLine("Введите ID заказа: ");
            Order order = orders[Convert.ToInt32(Console.ReadLine())];
            order.printReceipt();
        }

        static void countClosed()
        {
            double totalSum = 0;
            foreach(KeyValuePair<int, Order> order in orders)
            {
                if (order.Value.closedTime != null)
                {
                    totalSum += order.Value.CalculateTotal();
                }
            }
            Console.WriteLine($"Стоимость всех закрытых заказов: {totalSum}");
        }

        static void countWaiterClosed()
        {
            double totalSum = 0;
            Console.Write("Введите ID официанта: ");
            int waiter = Convert.ToInt32(Console.ReadLine());
            foreach (KeyValuePair<int, Order> order in orders)
            {
                if (order.Value.waiter == waiter && order.Value.closedTime != null)
                {
                    totalSum++;
                }
            }
            Console.WriteLine($"Количество закрытых заказов этого официанта: {totalSum}");
        }

        static void dishStatistics()
        {
            Dictionary<int, int> dishCounts = new Dictionary<int, int>();
            int totalDishes = 0; // Общее количество заказанных блюд
            foreach (var order in orders.Values)
            {
                if (order.closedTime != null)
                {
                    foreach (var dish in order.dishes)
                    {
                        totalDishes++; // Увеличиваем счетчик заказов
                        if (dishCounts.ContainsKey(dish.id))
                        {
                            dishCounts[dish.id]++;
                        }
                        else
                        {
                            dishCounts.Add(dish.id, 1);
                        }
                    }
                }
            }

            // Вывод статистики
            Console.WriteLine("Статистика заказов по блюдам:");
            foreach (var dishId in dishCounts.Keys)
            {
                int count = dishCounts[dishId];
                double percentage = (double)count / totalDishes * 100;
                Console.WriteLine($"Блюдо с ID: {dishId} заказано {count} раз ({percentage:F2}%)");
            }
        }
    }
}

