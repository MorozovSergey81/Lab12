using System;

namespace TrainWagons
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nМеню лабы:");
                Console.WriteLine("1. Работа с двунаправленным списком");
                Console.WriteLine("2. Работа с хеш-таблицей");
                Console.WriteLine("3. Работа с бинарным деревом");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DoublyLinkedListMenu();
                        break;
                    case "2":
                        HashTableMenu();
                        break;
                    case "3":
                        BinaryTreeMenu();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        static void DoublyLinkedListMenu()
        {
            DoublyLinkedList<Wagon> list = new DoublyLinkedList<Wagon>();
            while (true)
            {
                Console.WriteLine("\nДвунаправленный список:");
                Console.WriteLine("1. Добавить K элементов в конец");
                Console.WriteLine("2. Удалить последний элемент с заданным номером");
                Console.WriteLine("3. Показать список");
                Console.WriteLine("4. Клонировать список");
                Console.WriteLine("5. Очистить список");
                Console.WriteLine("6. Назад");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите количество элементов: ");
                        int k = int.Parse(Console.ReadLine());
                        list.AddToEnd(k);
                        break;
                    case "2":
                        Console.Write("Введите номер вагона: ");
                        int number = int.Parse(Console.ReadLine());
                        list.RemoveLastWithNumber(number);
                        break;
                    case "3":
                        Console.WriteLine("\nСписок:");
                        list.Print();
                        break;
                    case "4":
                        var clone = list.DeepClone();
                        Console.WriteLine("\nКлонированный список:");
                        clone.Print();
                        break;
                    case "5":
                        list.Clear();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        static void HashTableMenu()
        {
            HashTable<int, Wagon> table = new HashTable<int, Wagon>(5);
            Random rnd = new Random();
            while (true)
            {
                Console.WriteLine("\nХеш-таблица:");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Найти элемент");
                Console.WriteLine("3. Удалить элемент");
                Console.WriteLine("4. Показать таблицу");
                Console.WriteLine("5. Добавить при переполнении");
                Console.WriteLine("6. Клонировать таблицу");
                Console.WriteLine("7. Очистить таблицу");
                Console.WriteLine("8. Назад");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Wagon wagon = CreateRandomWagon(rnd);
                        table.Add(wagon.Number, wagon);
                        break;
                    case "2":
                        Console.Write("Введите номер вагона: ");
                        int key = int.Parse(Console.ReadLine());
                        if (table.Find(key, out Wagon found))
                            Console.WriteLine($"Найден: {found}");
                        break;
                    case "3":
                        Console.Write("Введите номер вагона: ");
                        key = int.Parse(Console.ReadLine());
                        table.Remove(key);
                        break;
                    case "4":
                        Console.WriteLine("\nХеш-таблица:");
                        table.Print();
                        break;
                    case "5":
                        for (int i = 0; i < 10; i++)//добавляем больше элементов чем размер таблицы
                        {
                            wagon = CreateRandomWagon(rnd);
                            table.Add(wagon.Number, wagon);
                        }
                        break;
                    case "6":
                        var clone = table.DeepClone();
                        Console.WriteLine("\nКлонированная хеш-таблица:");
                        clone.Print();
                        break;
                    case "7":
                        table.Clear();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        static void BinaryTreeMenu()
        {
            BinaryTree<int, Wagon> tree = new BinaryTree<int, Wagon>();
            Random rnd = new Random();
            while (true)
            {
                Console.WriteLine("\nБинарное дерево:");
                Console.WriteLine("1. Добавить элемент в идеальное дерево");
                Console.WriteLine("2. Построить идеальное дерево");
                Console.WriteLine("3. Показать дерево по уровням");
                Console.WriteLine("4. Преобразовать в дерево поиска");
                Console.WriteLine("5. Удалить элемент из дерева поиска");
                Console.WriteLine("6. Найти количество элементов с заданным ключом");
                Console.WriteLine("7. Клонировать дерево");
                Console.WriteLine("8. Очистить дерево");
                Console.WriteLine("9. Назад");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Wagon wagon = CreateRandomWagon(rnd);
                        tree.AddIdeal(wagon.Number, wagon);
                        break;
                    case "2":
                        tree.BuildIdealTree();
                        Console.WriteLine("Идеальное дерево построено");
                        break;
                    case "3":
                        Console.WriteLine("\nДерево по уровням:");
                        tree.PrintLevels();
                        break;
                    case "4":
                        var searchTree = tree.ConvertToSearchTree();
                        Console.WriteLine("\nДерево поиска:");
                        searchTree.PrintLevels();
                        break;
                    case "5":
                        Console.Write("Введите номер вагона: ");
                        int key = int.Parse(Console.ReadLine());
                        tree.Remove(key);
                        break;
                    case "6":
                        Console.Write("Введите номер вагона: ");
                        key = int.Parse(Console.ReadLine());
                        int count = tree.CountElementsWithKey(key);
                        Console.WriteLine($"Количество элементов с ключом {key}: {count}");
                        break;
                    case "7":
                        var clone = tree.DeepClone();
                        Console.WriteLine("\nКлонированное дерево:");
                        clone.PrintLevels();
                        break;
                    case "8":
                        tree.Clear();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        } 
     
        static Wagon CreateRandomWagon(Random rnd)
        {
            int wagonType = rnd.Next(0, 3);
            int number = rnd.Next(1, 1000);
            int minSpeed = rnd.Next(50, 200);

            switch (wagonType)
            {
                case 0:
                    string[] purposes = { "Уголь", "Зерно", "Нефть", "Общее" };
                    return new FreightWagon(number, minSpeed, purposes[rnd.Next(purposes.Length)], rnd.Next(20, 100));
                case 1:
                    return new PassengerWagon(number, minSpeed, rnd.Next(10, 50), rnd.Next(20, 100));
                default:
                    return new RestaurantWagon(number, minSpeed, $"{rnd.Next(6, 12):D2}:00-{rnd.Next(18, 24):D2}:00");
            }
        }
    }
}