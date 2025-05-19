using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TrainWagons;

[TestClass]
public class DoublyLinkedListTests
{
    private DoublyLinkedList<Wagon> list;
    [TestInitialize]
    public void Setup()
    {
        list = new DoublyLinkedList<Wagon>();
    }
    [TestMethod]
    public void AddToEnd_AddsElementsToEnd()
    {
        int k = 1;
        var output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        var lines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(k, lines.Length);
        StringAssert.Contains(lines[0], "Список пуст");
        
    }

    [TestMethod]
    public void AddToEnd_EmptyList_SetsHead()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        var lines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(1, lines.Length);
        StringAssert.Contains(lines[0], "Список пуст");
    }

    [TestMethod]
    public void RemoveLastWithNumber_RemovesCorrectElement()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        list.RemoveLastWithNumber(1);
        StringAssert.Contains(output.ToString(), "Список пуст");
        output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        var lines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(1, lines.Length);
        StringAssert.Contains(lines[0], "Список пуст");
        
    }

    [TestMethod]
    public void RemoveLastWithNumber_NonExistentNumber_PrintsNotFound()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        list.RemoveLastWithNumber(9999);
        StringAssert.Contains(output.ToString(), "Список пуст");
        output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        var lines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(1, lines.Length);
    }

    [TestMethod]
    public void Print_EmptyList_PrintsEmptyMessage()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        StringAssert.Contains(output.ToString(), "Список пуст");
    }

    [TestMethod]
    public void Print_NonEmptyList_PrintsElements()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        var lines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual(1, lines.Length);
        StringAssert.Contains(lines[0], "Список пуст");
     
    }

    [TestMethod]
    public void DeepClone_ClonesList()
    {
        var clone = list.DeepClone();
        var originalOutput = new StringWriter();
        Console.SetOut(originalOutput);
        list.Print();
        var cloneOutput = new StringWriter();
        Console.SetOut(cloneOutput);
        clone.Print();
        Assert.AreEqual(originalOutput.ToString(), cloneOutput.ToString());
    }

    [TestMethod]
    public void DeepClone_EmptyList_ReturnsEmptyClone()
    {
        var clone = list.DeepClone();
        var output = new StringWriter();
        Console.SetOut(output);
        clone.Print();
        StringAssert.Contains(output.ToString(), "Список пуст");
    }

    [TestMethod]
    public void Clear_ClearsList()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        list.Clear();
        StringAssert.Contains(output.ToString(), "Список удалён из памяти.");
        output = new StringWriter();
        Console.SetOut(output);
        list.Print();
        StringAssert.Contains(output.ToString(), "Список пуст");
    }
}