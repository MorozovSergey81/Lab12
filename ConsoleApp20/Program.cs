using System;

namespace TrainWagons
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<Wagon> list = new DoublyLinkedList<Wagon>();

            while (true)
            {
                Console.WriteLine("\nДвунаправленный список");
                Console.WriteLine("1. Добавить K элементов в конец списка");
                Console.WriteLine("2. Удалить последний элемент с заданным номером");
                Console.WriteLine("3. Показать список");
                Console.WriteLine("4. Клонировать список");
                Console.WriteLine("5. Очистить список");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите количество элементов для добавления: ");
                        int k = int.Parse(Console.ReadLine());
                        list.AddToEnd(k);
                        break;

                    case "2":
                        Console.Write("Введите номер вагона для удаления: ");
                        int number = int.Parse(Console.ReadLine());
                        list.RemoveLastWithNumber(number);
                        break;

                    case "3":
                        Console.WriteLine("\nТекущий список:");
                        list.Print();
                        break;

                    case "4":
                        Console.WriteLine("\nКлонирование списка");
                        var clonedList = list.DeepClone();
                        Console.WriteLine("Клонированный список:");
                        clonedList.Print();
                        break;

                    case "5":
                        list.Clear();
                        Console.WriteLine("\nСписок после очистки:");
                        list.Print();
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }
    }
}