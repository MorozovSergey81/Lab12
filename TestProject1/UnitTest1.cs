using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using TrainWagons;

// Мок-классы для тестирования
public class MockWagon : Wagon, ICloneable
{
    public int Number { get; set; }
    public int MinSpeed { get; set; }

    public MockWagon(int number, int minSpeed) : base(number, minSpeed)
    {
        Number = number;
        MinSpeed = minSpeed;
    }

    public object Clone()
    {
        return new MockWagon(Number, MinSpeed);
    }

    public override string ToString()
    {
        return $"MockWagon: Number={Number}, MinSpeed={MinSpeed}";
    }

    // Минимальная реализация абстрактных методов
    public override void Show() => Console.WriteLine(ToString());
    public override void Init() { }
    public override void RandomInit() { }
    public override void ShallowCopy() { }
    public override void VirtualShow() => Console.WriteLine(ToString());
}

[TestClass]
public class DoublyLinkedListTests
{
    private DoublyLinkedList<MockWagon> list;

    [TestInitialize]
    public void Setup()
    {
        list = new DoublyLinkedList<MockWagon>();
    }

    [TestMethod]
    public void AddToEnd_AddsElementsToEnd()
    {
        // Arrange
        int k = 3;
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        list.AddToEnd(k);

        // Assert
        StringAssert.Contains(output.ToString(), "Добавляем элемент:");
        // Проверяем, что добавлено k элементов (проверим косвенно через Print)
        var printOutput = new StringWriter();
        Console.SetOut(printOutput);
        list.Print();
        var lines = printOutput.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(k, lines.Length);
    }

    [TestMethod]
    public void AddToEnd_EmptyList_SetsHead()
    {
        // Arrange
        int k = 1;
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        list.AddToEnd(k);

        // Assert
        StringAssert.Contains(output.ToString(), "Добавляем элемент:");
        var printOutput = new StringWriter();
        Console.SetOut(printOutput);
        list.Print();
        var lines = printOutput.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(1, lines.Length); // Должен быть ровно 1 элемент
    }

    [TestMethod]
    public void RemoveLastWithNumber_RemovesCorrectElement()
    {
        // Arrange
        list.AddToEnd(3); // Добавляем 3 элемента
        var output = new StringWriter();
        Console.SetOut(output);

        // Найдём номер первого элемента для удаления
        var printOutput = new StringWriter();
        Console.SetOut(printOutput);
        list.Print();
        var firstLine = printOutput.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)[0];
        int number = int.Parse(firstLine.Split('=')[1].Split(',')[0]); // Извлекаем Number из строки

        // Act
        Console.SetOut(output);
        list.RemoveLastWithNumber(number);

        // Assert
        StringAssert.Contains(output.ToString(), $"Удаляем последний элемент с номером {number}:");
        printOutput = new StringWriter();
        Console.SetOut(printOutput);
        list.Print();
        var lines = printOutput.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(2, lines.Length); // Должно остаться 2 элемента
    }

    [TestMethod]
    public void RemoveLastWithNumber_NonExistentNumber_PrintsNotFound()
    {
        // Arrange
        list.AddToEnd(2); // Добавляем 2 элемента
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        list.RemoveLastWithNumber(9999); // Номер, которого нет

        // Assert
        StringAssert.Contains(output.ToString(), "Элемент с номером 9999 не найден");
    }
        [TestMethod]
    public void Print_EmptyList_PrintsEmptyMessage()
    {
        // Arrange
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        list.Print();

        // Assert
        StringAssert.Contains(output.ToString(), "Список пуст");
    }

    [TestMethod]
    public void Print_NonEmptyList_PrintsElements()
    {
        // Arrange
        list.AddToEnd(2); // Добавляем 2 элемента
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        list.Print();

        // Assert
        var lines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(2, lines.Length); // Должно быть 2 строки
    }

    [TestMethod]
    public void DeepClone_ClonesList()
    {
        // Arrange
        list.AddToEnd(2); // Добавляем 2 элемента

        // Act
        var clone = list.DeepClone();

        // Assert
        var originalOutput = new StringWriter();
        Console.SetOut(originalOutput);
        list.Print();

        var cloneOutput = new StringWriter();
        Console.SetOut(cloneOutput);
        clone.Print();

        Assert.AreEqual(originalOutput.ToString(), cloneOutput.ToString()); // Содержимое должно совпадать
    }

    [TestMethod]
    public void DeepClone_EmptyList_ReturnsEmptyClone()
    {
        // Arrange & Act
        var clone = list.DeepClone();

        // Assert
        var output = new StringWriter();
        Console.SetOut(output);
        clone.Print();
        StringAssert.Contains(output.ToString(), "Список пуст");
    }

    [TestMethod]
    public void Clear_ClearsList()
    {
        // Arrange
        list.AddToEnd(3); // Добавляем 3 элемента
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        list.Clear();

        // Assert
        StringAssert.Contains(output.ToString(), "Список удалён из памяти.");
        output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        StringAssert.Contains(output.ToString(), "Список пуст");
    }
}