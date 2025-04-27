using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TrainWagons;

namespace HashTableTests
{
    public class TestValue : ICloneable
    {
        public string Value { get; set; }
        public TestValue(string value)
        {
            Value = value;
        }

        public object Clone()
        {
            return new TestValue(Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestValue other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    [TestClass]
    public class HashTableTests
    {
        private HashTable<int, TestValue> CreateTestHashTable(int size = 10)
        {
            return new HashTable<int, TestValue>(size);
        }

        [TestMethod]
        public void Add_NewKey_ShouldAddSuccessfully()
        {
            var hashTable = CreateTestHashTable();
            bool result = hashTable.Add(1, new TestValue("Value1"));

            Assert.IsTrue(result);
            TestValue value;
            Assert.IsTrue(hashTable.Find(1, out value));
            Assert.AreEqual("Value1", value.Value);
        }

        [TestMethod]
        public void Add_DuplicateKey_ShouldReturnFalse()
        {
            var hashTable = CreateTestHashTable();
            hashTable.Add(1, new TestValue("Value1"));
            bool result = hashTable.Add(1, new TestValue("Value2"));

            Assert.IsFalse(result);
            TestValue value;
            Assert.IsTrue(hashTable.Find(1, out value));
            Assert.AreEqual("Value1", value.Value);
        }

        [TestMethod]
        public void Add_Collision_ShouldAddToChain()
        {
            var hashTable = CreateTestHashTable(1); 
            bool result1 = hashTable.Add(1, new TestValue("Value1"));
            bool result2 = hashTable.Add(2, new TestValue("Value2"));
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            TestValue value1, value2;
            Assert.IsTrue(hashTable.Find(1, out value1));
            Assert.IsTrue(hashTable.Find(2, out value2));
            Assert.AreEqual("Value1", value1.Value);
            Assert.AreEqual("Value2", value2.Value);
        }

        [TestMethod]
        public void Find_ExistingKey_ShouldReturnValue()
        {
            var hashTable = CreateTestHashTable();
            hashTable.Add(1, new TestValue("Value1"));
            TestValue value;
            bool result = hashTable.Find(1, out value);
            Assert.IsTrue(result);
            Assert.AreEqual("Value1", value.Value);
        }

        [TestMethod]
        public void Find_NonExistingKey_ShouldReturnFalse()
        {
            var hashTable = CreateTestHashTable();
            hashTable.Add(1, new TestValue("Value1"));
            TestValue value;
            bool result = hashTable.Find(999, out value);
            Assert.IsFalse(result);
            Assert.IsNull(value); 
        }

        [TestMethod]
        public void Remove_ExistingKey_ShouldRemoveSuccessfully()
        {
            var hashTable = CreateTestHashTable();
            hashTable.Add(1, new TestValue("Value1"));
            bool result = hashTable.Remove(1);
            Assert.IsTrue(result);
            TestValue value;
            Assert.IsFalse(hashTable.Find(1, out value));
        }

        [TestMethod]
        public void Remove_NonExistingKey_ShouldReturnFalse()
        {
            var hashTable = CreateTestHashTable();
            bool result = hashTable.Remove(999);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_CollisionChain_ShouldRemoveCorrectElement()
        {
            var hashTable = CreateTestHashTable(1);
            hashTable.Add(1, new TestValue("Value1"));
            hashTable.Add(2, new TestValue("Value2"));
            bool result = hashTable.Remove(1);
            Assert.IsTrue(result);
            TestValue value;
            Assert.IsFalse(hashTable.Find(1, out value));
            Assert.IsTrue(hashTable.Find(2, out value));
            Assert.AreEqual("Value2", value.Value);
        }

        [TestMethod]
        public void Print_EmptyTable_ShouldPrintEmptyChains()
        {
            var hashTable = CreateTestHashTable(2);
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                hashTable.Print();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("0: null"));
                Assert.IsTrue(output.Contains("1: null"));
            }
        }

        [TestMethod]
        public void Print_NonEmptyTable_ShouldPrintChains()
        {
            var hashTable = CreateTestHashTable(2);
            hashTable.Add(1, new TestValue("Value1"));
            hashTable.Add(2, new TestValue("Value2"));
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                hashTable.Print();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Value1"));
                Assert.IsTrue(output.Contains("Value2"));
                Assert.IsTrue(output.Contains("null"));
            }
        }

        [TestMethod]
        public void DeepClone_ShouldCreateIndependentCopy()
        {
            var hashTable = CreateTestHashTable();
            hashTable.Add(1, new TestValue("Value1"));
            hashTable.Add(2, new TestValue("Value2"));
            var clone = hashTable.DeepClone();
            TestValue value1, value2;
            Assert.IsTrue(clone.Find(1, out value1));
            Assert.IsTrue(clone.Find(2, out value2));
            Assert.AreEqual("Value1", value1.Value);
            Assert.AreEqual("Value2", value2.Value);
            hashTable.Remove(1);
            Assert.IsTrue(clone.Find(1, out value1));
            Assert.IsFalse(hashTable.Find(1, out value1));
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements()
        {
            var hashTable = CreateTestHashTable();
            hashTable.Add(1, new TestValue("Value1"));
            hashTable.Add(2, new TestValue("Value2"));
            hashTable.Clear();
            TestValue value;
            Assert.IsFalse(hashTable.Find(1, out value));
            Assert.IsFalse(hashTable.Find(2, out value));
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                hashTable.Print();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("null"));
            }
        }
    }
}