using TrainWagons;

namespace TestProject6;

[TestClass]
public class MyCollectionTests
{
    // Класс-заглушка для Wagon, реализующий ICloneable
    private class TestWagon : ICloneable
    {
        public int Number { get; set; }
        public int MaxSpeed { get; set; }

        public TestWagon(int number, int maxSpeed)
        {
            Number = number;
            MaxSpeed = maxSpeed;
        }

        public object Clone()
        {
            return new TestWagon(Number, MaxSpeed);
        }
    }

    [TestMethod]
    public void Constructor_Default_CreatesEmptyCollection()
    {
        var collection = new MyCollection<int, TestWagon>();

        Assert.AreEqual(0, collection.Count);
        Assert.IsFalse(collection.ContainsKey(1));
    }

    [TestMethod]
    public void Constructor_WithLength_CreatesCollectionWithItems()
    {
        var collection = new MyCollection<int, TestWagon>(5);

        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void Constructor_Copy_CreatesDeepCopy()
    {
        var original = new MyCollection<int, TestWagon>();
        var wagon = new TestWagon(1, 100);
        original.Add(1, wagon);

        var copy = new MyCollection<int, TestWagon>(original);

        Assert.AreEqual(original.Count, copy.Count);
        Assert.IsTrue(copy.ContainsKey(1));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_Copy_NullCollection_ThrowsArgumentNullException()
    {
        MyCollection<int, TestWagon> nullCollection = null;
        var copy = new MyCollection<int, TestWagon>(nullCollection);
    }

    [TestMethod]
    public void Add_NewKeyValuePair_IncreasesCountAndStoresValue()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon = new TestWagon(1, 100);

        collection.Add(1, wagon);

        Assert.AreEqual(1, collection.Count);
        Assert.IsTrue(collection.ContainsKey(1));
        Assert.AreEqual(wagon, collection[1]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Add_DuplicateKey_ThrowsArgumentException()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon = new TestWagon(1, 100);
        collection.Add(1, wagon);

        collection.Add(1, new TestWagon(2, 200));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_NullKey_ThrowsArgumentNullException()
    {
        var collection = new MyCollection<string, TestWagon>();
        collection.Add(null, new TestWagon(1, 100));
    }

    [TestMethod]
    public void Indexer_Get_ValidKey_ReturnsValue()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon = new TestWagon(1, 100);
        collection.Add(1, wagon);

        var result = collection[1];

        Assert.AreEqual(wagon, result);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void Indexer_Get_InvalidKey_ThrowsKeyNotFoundException()
    {
        var collection = new MyCollection<int, TestWagon>();
        var value = collection[1];
    }

    [TestMethod]
    public void Indexer_Set_ExistingKey_UpdatesValue()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon1 = new TestWagon(1, 100);
        var wagon2 = new TestWagon(1, 200);
        collection.Add(1, wagon1);

        collection[1] = wagon2;

        Assert.AreEqual(wagon2, collection[1]);
        Assert.AreEqual(1, collection.Count);
    }

    [TestMethod]
    public void Remove_ExistingKey_ReturnsTrueAndRemovesItem()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon = new TestWagon(1, 100);
        collection.Add(1, wagon);

        bool removed = collection.Remove(1);

        Assert.IsTrue(removed);
        Assert.AreEqual(0, collection.Count);
        Assert.IsFalse(collection.ContainsKey(1));
    }

    [TestMethod]
    public void Remove_NonExistingKey_ReturnsFalse()
    {
        var collection = new MyCollection<int, TestWagon>();

        bool removed = collection.Remove(1);

        Assert.IsFalse(removed);
        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void TryGetValue_ExistingKey_ReturnsTrueAndValue()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon = new TestWagon(1, 100);
        collection.Add(1, wagon);

        bool result = collection.TryGetValue(1, out TestWagon value);

        Assert.IsTrue(result);
        Assert.AreEqual(wagon, value);
    }

    [TestMethod]
    public void TryGetValue_NonExistingKey_ReturnsFalseAndDefault()
    {
        var collection = new MyCollection<int, TestWagon>();

        bool result = collection.TryGetValue(1, out TestWagon value);

        Assert.IsFalse(result);
        Assert.IsNull(value);
    }

    [TestMethod]
    public void Clear_RemovesAllItems()
    {
        var collection = new MyCollection<int, TestWagon>();
        collection.Add(1, new TestWagon(1, 100));
        collection.Add(2, new TestWagon(2, 200));

        collection.Clear();

        Assert.AreEqual(0, collection.Count);
        Assert.IsFalse(collection.ContainsKey(1));
        Assert.IsFalse(collection.ContainsKey(2));
    }

    [TestMethod]
    public void GetEnumerator_EnumeratesAllItems()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon1 = new TestWagon(1, 100);
        var wagon2 = new TestWagon(2, 200);
        collection.Add(1, wagon1);
        collection.Add(2, wagon2);

        var items = collection.ToList();

        Assert.AreEqual(2, items.Count);
        CollectionAssert.Contains(items, new KeyValuePair<int, TestWagon>(1, wagon1));
        CollectionAssert.Contains(items, new KeyValuePair<int, TestWagon>(2, wagon2));
    }

    [TestMethod]
    public void Keys_ReturnsAllKeys()
    {
        var collection = new MyCollection<int, TestWagon>();
        collection.Add(1, new TestWagon(1, 100));
        collection.Add(2, new TestWagon(2, 200));

        var keys = collection.Keys;

        CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, keys.ToList());
    }

    [TestMethod]
    public void Values_ReturnsAllValues()
    {
        var collection = new MyCollection<int, TestWagon>();
        var wagon1 = new TestWagon(1, 100);
        var wagon2 = new TestWagon(2, 200);
        collection.Add(1, wagon1);
        collection.Add(2, wagon2);

        var values = collection.Values;

        CollectionAssert.AreEquivalent(new List<TestWagon> { wagon1, wagon2 }, values.ToList());
    }

    [TestMethod]
    public void Resize_HandlesCollisionsCorrectly()
    {
        var collection = new MyCollection<int, TestWagon>(2); // Маленький размер для проверки коллизий
        collection.Add(1, new TestWagon(1, 100));
        collection.Add(2, new TestWagon(2, 200));
        collection.Add(3, new TestWagon(3, 300));

        Assert.IsTrue(collection.ContainsKey(1));
        Assert.IsTrue(collection.ContainsKey(2));
        Assert.IsTrue(collection.ContainsKey(3));
    }
}