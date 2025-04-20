using System;

namespace TrainWagons
{
    public class DoublyLinkedList<T> where T : ICloneable//в двунаправленном списке ссылки в каждом узле указывают на предыдущий и на последующий узел в списке
    {
        private Node<T> head;

        public DoublyLinkedList()
        {
            head = null;//изначально список пуст
        }

        public void AddToEnd(int k)//по варианту добавляет в конец количество элементов которые введет пользователь
        {
            Random rnd = new Random();
            for (int i = 0; i < k; i++)
            {
                int wagonType = rnd.Next(0, 3);//случайная генерация вагона 
                Wagon wagon;
                int number = rnd.Next(1, 1000);
                int minSpeed = rnd.Next(50, 200);

                switch (wagonType)//в зависимости от рандомного типа вагона создаются вагоны
                {
                    case 0:
                        string[] purposes = { "уоль", "зерно", "нефть", "общее" };
                        wagon = new FreightWagon(number, minSpeed, purposes[rnd.Next(purposes.Length)], rnd.Next(20, 100));
                        break;
                    case 1:
                        wagon = new PassengerWagon(number, minSpeed, rnd.Next(10, 50), rnd.Next(20, 100));
                        break;
                    default:
                        wagon = new RestaurantWagon(number, minSpeed, $"{rnd.Next(6, 12):D2}:00-{rnd.Next(18, 24):D2}:00");
                        break;
                }

                Node<T> newNode = new Node<T>((T)(object)wagon);//создается новый узел и приводится тип вагона
                Console.WriteLine($"Добавляем элемент: {wagon}");

                if (head == null)
                {
                    head = newNode;
                }
                else
                {
                    Node<T> current = head;//начинаем с начала
                    while (current.Next != null)//идем пока след эл списка не пуст
                    {
                        current = current.Next;//связываем последний с новым
                    }
                    current.Next = newNode;
                    newNode.Prev = current;
                }
            }
        }

        public void RemoveLastWithNumber(int number)
        {
            if (head == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Node<T> current = head;
            Node<T> lastMatch = null;

            while (current != null)
            {
                if (((Wagon)(object)current.Data).Number == number)//если номер совпадает сохраняем в ластмач
                {
                    lastMatch = current;
                }
                current = current.Next;
            }

            if (lastMatch == null)//если не найдет
            {
                Console.WriteLine($"Элемент с номером {number} не найден");
                return;
            }

            Console.WriteLine($"Удаляем последний элемент с номером {number}: {lastMatch.Data}");//если найдет

            if (lastMatch.Prev != null)
            {
                lastMatch.Prev.Next = lastMatch.Next;
            }
            else
            {
                head = lastMatch.Next;
            }

            if (lastMatch.Next != null)
            {
                lastMatch.Next.Prev = lastMatch.Prev;
            }
        }

        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Node<T> current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        public DoublyLinkedList<T> DeepClone()
        {
            DoublyLinkedList<T> clone = new DoublyLinkedList<T>();
            if (head == null)//если пуст выводим пустую копию
                return clone;

            Node<T> current = head;
            while (current != null)
            {
                T clonedData = (T)current.Data.Clone();
                Node<T> newNode = new Node<T>(clonedData);//проверяем с головы

                if (clone.head == null)
                {
                    clone.head = newNode;
                }
                else
                {
                    Node<T> cloneCurrent = clone.head;//вот отсюда с головы
                    while (cloneCurrent.Next != null)
                    {
                        cloneCurrent = cloneCurrent.Next;//проходимся по всем узлам
                    }
                    cloneCurrent.Next = newNode;
                    newNode.Prev = cloneCurrent;
                }
                current = current.Next;
            }

            return clone;
        }

        public void Clear()
        {
            head = null;//отвязываем все узлы списка
            GC.Collect();
            Console.WriteLine("Список удалён из памяти.");
        } 
    }
}