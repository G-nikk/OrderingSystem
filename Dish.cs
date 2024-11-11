using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem
{
    public enum Category
    {
        Напитки = 1,
        Салаты = 2,
        Закуски = 3,
        Супы = 4,
        Горячее = 5,
        Десерты = 6
    }
    internal class Dish
    {
        public int id { get; set; }
        public string name { get; set; }
        public string composition { get; set; }
        public string weight { get; set; }
        public double price { get; set; }
        public Category category { get; set; }
        public int cookingTime { get; set; }
        public string[] type { get; set; }

        public Dish(int id, string name, string composition, string weight, double price, int categoryIndex, int cookingTime, string[] type)
        {
            this.id = id;
            this.name = name;
            this.composition = composition;
            this.weight = weight;
            this.price = price;
            this.category = (Category)categoryIndex;
            this.cookingTime = cookingTime;
            this.type = type;
        }
        public void printInfo()
        {
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Название: {name}");
            Console.WriteLine($"Состав: {composition}");
            Console.WriteLine($"Вес: {weight}");
            Console.WriteLine($"Цена: {price}");
            Console.WriteLine($"Категория:{category}");
            Console.WriteLine($"Время готовки:{cookingTime}");
            Console.WriteLine($"Тип: {string.Join(", ", type)}");
        }
        public void edit(string newName, string newComposition, string newWeight, double newPrice, int newCategoryIndex, int newCookingTime, string[] newType)
        {
            name = newName;
            composition = newComposition;
            weight = newWeight;
            price = newPrice;
            category = (Category)newCategoryIndex;
            cookingTime = newCookingTime;
            type = newType;
        }

    }
}
