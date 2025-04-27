using System;
using System.Collections.Generic;

namespace TrainWagons
{
    public class BinaryTree<TKey, TValue> where TValue : ICloneable where TKey : IComparable<TKey>
    {
        private class TreeNode
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Left = null;
                Right = null;
            }
        }

        private TreeNode root;
        private List<KeyValuePair<TKey, TValue>> elements;

        public BinaryTree()
        {
            root = null;
            elements = new List<KeyValuePair<TKey, TValue>>();
        }

        public void AddIdeal(TKey key, TValue value)
        {
            elements.Add(new KeyValuePair<TKey, TValue>(key, value));
            Console.WriteLine($"Добавлен элемент для сбалансированного дерева: {value}");
        }

        public void BuildIdealTree()
        {
            elements.Sort((x, y) => x.Key.CompareTo(y.Key));
            root = BuildIdealTreeRecursive(0, elements.Count - 1);
        }

        private TreeNode BuildIdealTreeRecursive(int start, int end)
        {
            if (start > end) return null;
            int mid = (start + end) / 2;
            var pair = elements[mid];
            TreeNode node = new TreeNode(pair.Key, pair.Value);
            node.Left = BuildIdealTreeRecursive(start, mid - 1);
            node.Right = BuildIdealTreeRecursive(mid + 1, end);
            return node;
        }

        public BinaryTree<TKey, TValue> ConvertToSearchTree()
        {
            BinaryTree<TKey, TValue> searchTree = new BinaryTree<TKey, TValue>();
            InorderTraversal(root, (key, value) => searchTree.AddSearch(key, value));
            return searchTree;
        }

        private void InorderTraversal(TreeNode node, Action<TKey, TValue> action)
        {
            if (node != null)
            {
                InorderTraversal(node.Left, action);
                action(node.Key, node.Value);
                InorderTraversal(node.Right, action);
            }
        }

        public void AddSearch(TKey key, TValue value)
        {
            root = AddSearchRecursive(root, key, value);
            Console.WriteLine($"Добавлен элемент в дерево поиска: {value}");
        }

        private TreeNode AddSearchRecursive(TreeNode node, TKey key, TValue value)
        {
            if (node == null)
                return new TreeNode(key, value);

            if (key.CompareTo(node.Key) < 0)
                node.Left = AddSearchRecursive(node.Left, key, value);
            else if (key.CompareTo(node.Key) > 0)
                node.Right = AddSearchRecursive(node.Right, key, value);

            return node;
        }

        public bool Remove(TKey key)
        {
            TreeNode parent = null;
            TreeNode current = root;

            while (current != null && !current.Key.Equals(key))
            {
                parent = current;
                if (key.CompareTo(current.Key) < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }

            if (current == null)
            {
                Console.WriteLine($"Элемент с ключом {key} не найден");
                return false;
            }

            if (current.Left == null || current.Right == null)
            {
                TreeNode newCurrent = current.Left == null ? current.Right : current.Left;
                if (parent == null)
                    root = newCurrent;
                else if (parent.Left == current)
                    parent.Left = newCurrent;
                else
                    parent.Right = newCurrent;
            }
            else
            {
                TreeNode minParent = current;
                TreeNode minNode = current.Right;
                while (minNode.Left != null)
                {
                    minParent = minNode;
                    minNode = minNode.Left;
                }
                current.Key = minNode.Key;
                current.Value = minNode.Value;
                if (minParent.Left == minNode)
                    minParent.Left = minNode.Right;
                else
                    minParent.Right = minNode.Right;
            }

            Console.WriteLine($"Удалён элемент с ключом {key}");
            return true;
        }

        public void PrintLevels()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пусто");
                return;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int level = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                Console.WriteLine($"Уровень {level}:");

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    Console.WriteLine($"  {node.Value}");

                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }
                level++;
            }
        }

        public BinaryTree<TKey, TValue> DeepClone()
        {
            BinaryTree<TKey, TValue> clone = new BinaryTree<TKey, TValue>();
            InorderTraversal(root, (key, value) => clone.AddSearch(key, (TValue)value.Clone()));
            return clone;
        }

        public void Clear()
        {
            root = null;
            elements.Clear();
            Console.WriteLine("Дерево очищено.");
        }

        public int CountElementsWithKey(TKey key)
        {
            return CountElementsWithKeyRecursive(root, key);
        }

        private int CountElementsWithKeyRecursive(TreeNode node, TKey key)
        {
            if (node == null) return 0;
            int count = node.Key.Equals(key) ? 1 : 0;
            count += CountElementsWithKeyRecursive(node.Left, key);
            count += CountElementsWithKeyRecursive(node.Right, key);
            return count;
        }
    }
}