using System;
using System.Collections.Generic;

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
                Console.WriteLine("4. Работа с обобщенной коллекцией (MyCollection)");
                Console.WriteLine("5. Выход");
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
                        MyCollectionMenu();
                        break;
                    case "5":
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

        static void MyCollectionMenu()
        {
            MyCollection<int, Wagon> collection = new MyCollection<int, Wagon>();
            Random rnd = new Random();
            while (true)
            {
                Console.WriteLine("\nОбобщенная коллекция (MyCollection):");
                Console.WriteLine("1. Создать пустую коллекцию");
                Console.WriteLine("2. Создать коллекцию с N случайными элементами");
                Console.WriteLine("3. Создать копию коллекции");
                Console.WriteLine("4. Добавить элемент");
                Console.WriteLine("5. Удалить элемент по ключу");
                Console.WriteLine("6. Найти элемент по ключу");
                Console.WriteLine("7. Показать все элементы (foreach)");
                Console.WriteLine("8. Показать ключи и значения");
                Console.WriteLine("9. Очистить коллекцию");
                Console.WriteLine("10. Назад");
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        collection = new MyCollection<int, Wagon>();
                        Console.WriteLine("Создана пустая коллекция");
                        break;
                    case "2":
                        Console.Write("Введите количество элементов: ");
                        if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
                        {
                            collection = new MyCollection<int, Wagon>(n);
                            Console.WriteLine($"Создана коллекция с {n} случайными элементами");
                        }
                        else
                        {
                            Console.WriteLine("Некорректное количество элементов");
                        }
                        break;
                    case "3":
                        var copy = new MyCollection<int, Wagon>(collection);
                        Console.WriteLine("Создана копия коллекции");
                        Console.WriteLine($"Количество элементов в копии: {copy.Count}");
                        Console.WriteLine("Элементы копии:");
                        foreach (var pair in copy)
                        {
                            Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
                        }
                        break;
                    case "4":
                        Wagon wagon = CreateRandomWagon(rnd);
                        try
                        {
                            collection.Add(wagon.Number, wagon);
                            Console.WriteLine($"Добавлен элемент: {wagon}");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine($"Элемент с ключом {wagon.Number} уже существует");
                        }
                        break;
                    case "5":
                        Console.Write("Введите ключ (номер вагона): ");
                        if (int.TryParse(Console.ReadLine(), out int key))
                        {
                            bool removed = collection.Remove(key);
                            Console.WriteLine(removed ? 
                                $"Элемент с ключом {key} удален" : 
                                $"Элемент с ключом {key} не найден");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ключ");
                        }
                        break;
                    case "6":
                        Console.Write("Введите ключ (номер вагона): ");
                        if (int.TryParse(Console.ReadLine(), out key))
                        {
                            if (collection.TryGetValue(key, out Wagon value))
                            {
                                Console.WriteLine($"Найден элемент: {value}");
                            }
                            else
                            {
                                Console.WriteLine($"Элемент с ключом {key} не найден");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ключ");
                        }
                        break;
                    case "7":
                        Console.WriteLine("\nВсе элементы коллекции (перебор с помощью foreach):");
                        if (collection.Count == 0)
                        {
                            Console.WriteLine("Коллекция пуста");
                        }
                        else
                        {
                            foreach (var pair in collection)
                            {
                                Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
                            }
                        }
                        break;
                    case "8":
                        Console.WriteLine("\nКлючи коллекции:");
                        foreach (var k in collection.Keys)
                        {
                            Console.WriteLine($"  {k}");
                        }
                        
                        Console.WriteLine("\nЗначения коллекции:");
                        foreach (var v in collection.Values)
                        {
                            Console.WriteLine($"  {v}");
                        }
                        break;
                    case "9":
                        collection.Clear();
                        Console.WriteLine("Коллекция очищена");
                        break;
                    case "10":
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
