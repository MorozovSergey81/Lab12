using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TrainWagons;
using System.IO;
using System.Text;

namespace BinaryTreeTests
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
    public class BinaryTreeTests
    {
        private BinaryTree<int, TestValue> CreateTestTree()
        {
            return new BinaryTree<int, TestValue>();
        }

        [TestMethod]
        public void AddIdeal_ShouldAddElements()
        {
            var tree = CreateTestTree();
            tree.AddIdeal(1, new TestValue("Value1"));
            tree.AddIdeal(2, new TestValue("Value2"));
            Assert.AreEqual(0, tree.CountElementsWithKey(1) + tree.CountElementsWithKey(2));
        }

        [TestMethod]
        public void BuildIdealTree_ShouldCreateBalancedTree()
        {
            var tree = CreateTestTree();
            tree.AddIdeal(1, new TestValue("Value1"));
            tree.AddIdeal(2, new TestValue("Value2"));
            tree.AddIdeal(3, new TestValue("Value3"));
            tree.BuildIdealTree();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.PrintLevels();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Уровень 0:"));
                Assert.IsTrue(output.Contains("Value2"));
                Assert.IsTrue(output.Contains("Уровень 1:"));
                Assert.IsTrue(output.Contains("Value1"));
                Assert.IsTrue(output.Contains("Value3"));
            }
        }

        [TestMethod]
        public void ConvertToSearchTree_ShouldCreateSearchTree()
        {
            var tree = CreateTestTree();
            tree.AddIdeal(1, new TestValue("Value1"));
            tree.AddIdeal(2, new TestValue("Value2"));
            tree.AddIdeal(3, new TestValue("Value3"));
            tree.BuildIdealTree();
            var searchTree = tree.ConvertToSearchTree();
            Assert.AreEqual(1, searchTree.CountElementsWithKey(1));
            Assert.AreEqual(1, searchTree.CountElementsWithKey(2));
            Assert.AreEqual(1, searchTree.CountElementsWithKey(3));
        }

        [TestMethod]
        public void AddSearch_ShouldAddElementsCorrectly()
        {
            var tree = CreateTestTree();
            tree.AddSearch(2, new TestValue("Value2"));
            tree.AddSearch(1, new TestValue("Value1"));
            tree.AddSearch(3, new TestValue("Value3"));
            Assert.AreEqual(1, tree.CountElementsWithKey(1));
            Assert.AreEqual(1, tree.CountElementsWithKey(2));
            Assert.AreEqual(1, tree.CountElementsWithKey(3));
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.PrintLevels();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Value2"));
                Assert.IsTrue(output.Contains("Value1"));
                Assert.IsTrue(output.Contains("Value3"));
            }
        }

        [TestMethod]
        public void Remove_ExistingKey_ShouldRemoveElement()
        {
            var tree = CreateTestTree();
            tree.AddSearch(2, new TestValue("Value2"));
            tree.AddSearch(1, new TestValue("Value1"));
            tree.AddSearch(3, new TestValue("Value3"));
            bool result = tree.Remove(2);
            Assert.IsTrue(result);
            Assert.AreEqual(0, tree.CountElementsWithKey(2));
            Assert.AreEqual(1, tree.CountElementsWithKey(1));
            Assert.AreEqual(1, tree.CountElementsWithKey(3));
        }

        [TestMethod]
        public void Remove_NonExistingKey_ShouldReturnFalse()
        {
            var tree = CreateTestTree();
            tree.AddSearch(1, new TestValue("Value1"));
            bool result = tree.Remove(999);
            Assert.IsFalse(result);
            Assert.AreEqual(1, tree.CountElementsWithKey(1));
        }

        [TestMethod]
        public void PrintLevels_EmptyTree_ShouldPrintEmptyMessage()
        {
            var tree = CreateTestTree();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.PrintLevels();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Дерево пусто"));
            }
        }

        [TestMethod]
        public void DeepClone_ShouldCreateIndependentCopy()
        {
            var tree = CreateTestTree();
            tree.AddSearch(1, new TestValue("Value1"));
            tree.AddSearch(2, new TestValue("Value2"));
            var clone = tree.DeepClone();
            Assert.AreEqual(1, clone.CountElementsWithKey(1));
            Assert.AreEqual(1, clone.CountElementsWithKey(2));
            tree.Remove(1);
            Assert.AreEqual(1, clone.CountElementsWithKey(1));
            Assert.AreEqual(0, tree.CountElementsWithKey(1));
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements()
        {
            var tree = CreateTestTree();
            tree.AddSearch(1, new TestValue("Value1"));
            tree.AddSearch(2, new TestValue("Value2"));
            tree.Clear();
            Assert.AreEqual(0, tree.CountElementsWithKey(1));
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.PrintLevels();
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Дерево пусто"));
            }
        }

        [TestMethod]
        public void CountElementsWithKey_MultipleOccurrences_ShouldCountCorrectly()
        {
            var tree = CreateTestTree();
            tree.AddSearch(1, new TestValue("Value1"));
            tree.AddSearch(1, new TestValue("Value1_duplicate"));
            tree.AddSearch(2, new TestValue("Value2"));
            int count = tree.CountElementsWithKey(1);
            Assert.AreEqual(1, count);
            Assert.AreEqual(1, tree.CountElementsWithKey(2));
        }

        [TestMethod]
        public void Remove_RootWithTwoChildren_ShouldRestructureCorrectly()
        {
            var tree = CreateTestTree();
            tree.AddSearch(2, new TestValue("Value2"));
            tree.AddSearch(1, new TestValue("Value1"));
            tree.AddSearch(3, new TestValue("Value3"));
            tree.AddSearch(4, new TestValue("Value4"));
            bool result = tree.Remove(2);
            Assert.IsTrue(result);
            Assert.AreEqual(0, tree.CountElementsWithKey(2));
            Assert.AreEqual(1, tree.CountElementsWithKey(3));
            Assert.AreEqual(1, tree.CountElementsWithKey(1));
            Assert.AreEqual(1, tree.CountElementsWithKey(4));
        }
    }
}