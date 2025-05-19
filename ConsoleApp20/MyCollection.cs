using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TrainWagons
{
    public class MyCollection<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>> where TValue : ICloneable
    {
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Next { get; set; }

            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Next = null;
            }
        }

        private Node[] table;
        private int size;
        private int count;
        private readonly Random random = new Random();
        
        public MyCollection()
        {
            size = 10;
            table = new Node[size];
            count = 0;
        }
        
        public MyCollection(int length)
        {
            size = Math.Max(length, 10);
            table = new Node[size];
            count = 0;
            
            for (int i = 0; i < length; i++)
            {
                int number = random.Next(1, 1000);
                int maxSpeed = random.Next(50, 200);
                
                Wagon wagon = null;
                int wagonType = random.Next(0, 3);
    
                switch (wagonType)
                {
                    case 0:
                        string[] purposes = { "Уголь", "Зерно", "Нефть", "Общее" };
                        wagon = new FreightWagon(number, maxSpeed, purposes[random.Next(purposes.Length)], random.Next(20, 100));
                        break;
                    case 1:
                        wagon = new PassengerWagon(number, maxSpeed, random.Next(10, 50), random.Next(20, 100));
                        break;
                    case 2:
                        wagon = new RestaurantWagon(number, maxSpeed, $"{random.Next(6, 12):D2}:00-{random.Next(18, 24):D2}:00");
                        break;
                }
    
                if (wagon != null)
                {
                    try
                    {
                        Add((TKey)(object)number, (TValue)(object)wagon);
                    }
                    catch (ArgumentException)
                    {
                        //если ключ уже существует пропускаем
                        i--; //повторяем попытку
                    }
                    catch (InvalidCastException)
                    {
                    }
                }
            }
        }
        
        public MyCollection(MyCollection<TKey, TValue> c)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c));

            size = c.size;
            table = new Node[size];
            count = 0;
            
            foreach (var pair in c)
            {
                Add(pair.Key, pair.Value);
            }
        }

        #region IDictionary<TKey, TValue> Implementation
        
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                int hash = GetHash(key);
                Node current = table[hash];

                while (current != null)
                {
                    if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                        return current.Value;
                    current = current.Next;
                }

                throw new KeyNotFoundException($"Ключ {key} не найден в коллекции");
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                int hash = GetHash(key);
                Node current = table[hash];
                
                if (current == null)
                {
                    table[hash] = new Node(key, value);
                    count++;
                    return;
                }
                
                Node prev = null;
                while (current != null)
                {
                    if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                    {
                        current.Value = value;
                        return;
                    }
                    prev = current;
                    current = current.Next;
                }
                
                prev.Next = new Node(key, value);
                count++;
            }
        }
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>(count);
                foreach (var pair in this)
                {
                    keys.Add(pair.Key);
                }
                return keys;
            }
        }
        
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>(count);
                foreach (var pair in this)
                {
                    values.Add(pair.Value);
                }
                return values;
            }
        }
        
        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int hash = GetHash(key);
            Node current = table[hash];
            
            if (current == null)
            {
                table[hash] = new Node(key, value);
                count++;
                return;
            }
            
            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                    throw new ArgumentException($"Элемент с ключом {key} уже существует");
                
                if (current.Next == null)
                    break;
                    
                current = current.Next;
            }
            
            current.Next = new Node(key, value);
            count++;
            
            if (count > size * 0.75)
            {
                Resize(size * 2);
            }
        }
        
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int hash = GetHash(key);
            Node current = table[hash];

            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                    return true;
                current = current.Next;
            }

            return false;
        }
        
        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int hash = GetHash(key);
            Node current = table[hash];
            Node prev = null;

            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                {
                    if (prev == null)
                    {
                        table[hash] = current.Next;
                    }
                    else
                    {
                        prev.Next = current.Next;
                    }
                    count--;
                    return true;
                }
                prev = current;
                current = current.Next;
            }

            return false;
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            int hash = GetHash(key);
            Node current = table[hash];

            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
            }

            value = default;
            return false;
        }

        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>> Implementation
        public int Count => count;
        
        public bool IsReadOnly => false;
        
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }
        
        public void Clear()
        {
            table = new Node[size];
            count = 0;
        }
        
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (TryGetValue(item.Key, out TValue value))
            {
                return EqualityComparer<TValue>.Default.Equals(value, item.Value);
            }
            return false;
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < count)
                throw new ArgumentException("Недостаточно места в массиве");

            int currentIndex = arrayIndex;
            foreach (var pair in this)
            {
                array[currentIndex++] = pair;
            }
        }
        
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
            {
                return Remove(item.Key);
            }
            return false;
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Implementation
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                Node current = table[i];
                while (current != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Helper Methods
        
        private int GetHash(TKey key)
        {
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode) % size;
        }
        
        private void Resize(int newSize)
        {
            Node[] oldTable = table;
            int oldSize = size;

            size = newSize;
            table = new Node[size];
            count = 0;
            
            for (int i = 0; i < oldSize; i++)
            {
                Node current = oldTable[i];
                while (current != null)
                {
                    Add(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        #endregion
    }
}
