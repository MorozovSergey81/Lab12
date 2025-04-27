using System;
using System.Collections.Generic;

namespace TrainWagons
{//в хеш таблице с цепочками разрешаются коллизии ведь могут быть два элемента с одинаковыми ключами а все элементы с одинаковым индексом хранятся в цепочке
    public class HashTable<TKey, TValue> where TValue : ICloneable//сопостовляет ключи со значениями обобщения помогают работать с любыми типами пар
    {
        private class HashNode
        {
            public TKey Key { get; set; }//храним ключ
            public TValue Value { get; set; }//значение
            public HashNode Next { get; set; }//следующий узел в цепочке

            public HashNode(TKey key, TValue value)//новый узел с заданным ключом и значением
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private HashNode[] table;//массив для хранения узлов хеш-таблицы
        private int size;//размер хеш-таблицы 

        public HashTable(int size = 10)//конструктор
        {
            this.size = size;//размер хеш-таблицы 
            table = new HashNode[size];//инициализируем массив хеш-таблицы создаем где все пустое
        }

        private int GetIndex(TKey key) => Math.Abs(key.GetHashCode()) % size;//вычисление индекса по ключу 

        public bool Add(TKey key, TValue value)//добавляем новый узел в хеш-таблицу
        {
            int index = GetIndex(key);
            HashNode newNode = new HashNode(key, value);//создаем новый узел с заданным ключом и значением

            if (table[index] == null)
            {
                table[index] = newNode;
                Console.WriteLine($"Добавлен элемент: {value}");
                return true;
            }

            HashNode current = table[index];
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    Console.WriteLine($"Элемент с ключом {key} уже существует");
                    return false;
                }
                if (current.Next == null) break;
                current = current.Next;
            }
            current.Next = newNode;
            Console.WriteLine($"Добавлен элемент в цепочку: {value}");
            return true;
        }

        public bool Find(TKey key, out TValue value)//поиск элемента по ключу
        {
            int index = GetIndex(key);//вычисляем индекс
            HashNode current = table[index];//начало цепочки
            while (current != null)//проходит по цепочке до конца
            {
                if (current.Key.Equals(key))//проверяет совпадение ключа с искомым
                {
                    value = current.Value;
                    Console.WriteLine($"Найден элемент: {value}");
                    return true;
                }
                current = current.Next;//если не совпадают то переход к следующему
            }
            value = default;
            Console.WriteLine($"Элемент с ключом {key} не найден");
            return false;
        }

        public bool Remove(TKey key)//удаление элемента по ключу
        {
            int index = GetIndex(key);//вычисляем индекс
            HashNode current = table[index];
            HashNode prev = null;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (prev == null)
                        table[index] = current.Next;
                    else
                        prev.Next = current.Next;
                    Console.WriteLine($"Удалён элемент с ключом {key}: {current.Value}");
                    return true;
                }
                prev = current;
                current = current.Next;
            }
            Console.WriteLine($"Элемент с ключом {key} не найден");
            return false;
        }

        public void Print()//выводит содержимое хеш таблицы
        {
            for (int i = 0; i < size; i++)//проходит по цепочке узлов
            {
                Console.Write($"{i}: ");
                HashNode current = table[i];
                while (current != null)
                {
                    Console.Write($"{current.Value} -> ");
                    current = current.Next;
                }
                Console.WriteLine("null");
            }
        }

        public HashTable<TKey, TValue> DeepClone()
        {
            HashTable<TKey, TValue> clone = new HashTable<TKey, TValue>(size);
            for (int i = 0; i < size; i++)
            {
                HashNode current = table[i];
                while (current != null)
                {
                    clone.Add(current.Key, (TValue)current.Value.Clone());
                    current = current.Next;
                }
            }
            return clone;
        }

        public void Clear()//очищает методом замены на новую пустую таблицу
        {
            table = new HashNode[size];
            Console.WriteLine("Хеш-таблица очищена.");
        }
    }
}