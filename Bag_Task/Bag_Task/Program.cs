using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Net.Sockets;

namespace Bag_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Knapsack knapsack;
            int R=0, itemsAmount=0;

            Console.Write("Введiть вмiстимiсть рюкзака в кг: "); R = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введiть к-сть предметiв: "); itemsAmount = Convert.ToInt32(Console.ReadLine());

            List<Item> items = new List<Item>();
            ItemsController.InitItems(items, itemsAmount);

            knapsack = new GreedyKnapsack(R, items);
            knapsack.FillBag();                                              //  Наповнення рюкзака

            Console.WriteLine("\nПредмети в рюкзаку з вагою:");
            ItemsController.PrintItems(knapsack.ItemsInBag);
            
        }
    }

    public class Item                                                       //  Класс, описуючий предмет
    {
        public int Weight { get; set; }

        public Item(int weight)
        {
            this.Weight = weight;
        }

        public override string ToString()
        {
            return Weight.ToString();
        }
    }

    public static class ItemsController                                 // Класс для маніпуляцій над предметами
    {
        public static void InitItems(List<Item> items, int size)
        {
            Console.WriteLine("===================");
            for (int i = 0; i < size; i++)
            {
                Console.Write("Введiть вагу предмета " + (i + 1) + ": ");
                items.Add(new Item(Convert.ToInt32(Console.ReadLine())));
            }
            Console.WriteLine("===================");
        }
        public static List<Item> DeepCopy(List<Item> items)
        {
            List<Item> lel = new List<Item>();
            lel.AddRange(items);
            return lel;
        }

        public static void PrintItems(List<Item> items)
        {
            Console.WriteLine("=================");
            foreach (Item item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=================");
        }

        public static int GetSumWeights(List<Item> items)
        {
            int sum = 0;
            foreach (Item item in items)
            {
                sum += item.Weight;
            }
            return sum;
        }
    }

    public abstract class Knapsack
    {
        protected int BagCapacity { get; set; }
        public List<Item> Items { get; set; }
        public List<Item> ItemsInBag { get; set; }

        protected Knapsack(int bagCapacity, List<Item> items) 
        {
            BagCapacity = bagCapacity;
            Items = items;
            ItemsInBag = new List<Item>();
        }

        public abstract void FillBag();

    }

    public class GreedyKnapsack : Knapsack                                  //  Реалізація жадного алгоритму наповнення рюкзака
    {
        private List<Item> sortedItems;
        public GreedyKnapsack(int bagCapacity, List<Item> items) : base(bagCapacity, items)
        {
            sortedItems = new List<Item>();
        }

        public override void FillBag()
        {
            sortedItems = ItemsController.DeepCopy(Items);
            sortedItems.Sort((x, y) => x.Weight.CompareTo(y.Weight));
            Func(sortedItems.Count - 1);                                    //  Виклик рекурсивної функції
        }
        
        private void Func(int indexCheck)
        {
            if (indexCheck < 0) return;

            if ((BagCapacity-ItemsController.GetSumWeights(ItemsInBag))>=sortedItems[indexCheck].Weight)
            {
                ItemsInBag.Add(sortedItems[indexCheck]);
            }
            Func(--indexCheck);
        }

    }

}
