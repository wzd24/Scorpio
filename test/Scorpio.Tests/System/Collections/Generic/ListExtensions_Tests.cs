﻿using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;
namespace System.Collections.Generic
{
    public class ListExtensions_Tests
    {
        static List<string> _sourceList = new List<string> { "Item1", "Item2", "Item3" };

        [Fact]
        public void FindIndex()
        {
            IList<string> list = new List<string>(_sourceList);
            list.FindIndex(s => s.StartsWith("I")).ShouldBe(0);
            list.FindIndex(s => s.StartsWith("A")).ShouldBe(-1);
            list.FindIndex(s => s.StartsWith("Item2")).ShouldBe(1);
        }

        [Fact]
        public void AddFirst()
        {
            IList<string> list = new List<string>(_sourceList);
            list.AddFirst("Item0");
            list[0].ShouldBe("Item0");
        }

        [Fact]
        public void AddLast()
        {
            IList<string> list = new List<string>(_sourceList);
            list.AddLast("Item0");
            list[3].ShouldBe("Item0");
        }

        [Theory]
        [ClassData(typeof(InsterBeforeData))]
        public void InsterBefore(Predicate<string> predicate, int index, string value)
        {
            IList<string> list = new List<string>(_sourceList);
            list.InsertBefore(predicate, value);
            list[index].ShouldBe(value);

        }

        [Theory]
        [ClassData(typeof(InsterAfterData))]
        public void InsterAfter(Predicate<string> predicate, int index, string value)
        {
            IList<string> list = new List<string>(_sourceList);
            list.InsertAfter(predicate, value);
            list[index].ShouldBe(value);

        }

        [Theory]
        [ClassData(typeof(ReplaceWhileValueData))]
        public void ReplaceWhileValue(Predicate<string> predicate, string value, Action<IList<string>> action)
        {
            IList<string> list = new List<string>(_sourceList);
            list.ReplaceWhile(predicate, value);
            action(list);
        }

        [Theory]
        [ClassData(typeof(ReplaceWhileFactoryData))]
        public void ReplaceWhileFactory(Predicate<string> predicate, Func<string, string> func, Action<IList<string>> action)
        {
            IList<string> list = new List<string>(_sourceList);
            list.ReplaceWhile(predicate, func);
            action(list);
        }

        [Theory]
        [ClassData(typeof(ReplaceOneSelectorData))]
        public void ReplaceOneSelector(Predicate<string> predicate, string value, Action<IList<string>> action)
        {
            IList<string> list = new List<string>(_sourceList);
            list.ReplaceOne(predicate, value);
            action(list);
        }

        [Theory]
        [ClassData(typeof(ReplaceOneItemData))]
        public void ReplaceOneItem(string old, string value, Action<IList<string>> action)
        {
            IList<string> list = new List<string>(_sourceList);
            list.ReplaceOne(old, value);
            action(list);
        }

        [Theory]
        [ClassData(typeof(ReplaceOneFactoryData))]
        public void ReplaceOneFactory(Predicate<string> predicate, Func<string, string> func, Action<IList<string>> action)
        {
            IList<string> list = new List<string>(_sourceList);
            list.ReplaceOne(predicate, func);
            action(list);
        }

        [Fact]
        public void GetOrAdd()
        {
            IList<string> list = new List<string>(_sourceList);
            list.Contains("Item0").ShouldBeFalse();
            list.GetOrAdd(i => i == "Item0", () => "Item0").ShouldBe("Item0");
            list.Contains("Item0").ShouldBeTrue();
        }

        class InsterAfterData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (Predicate<string>)(key => key == "Never"), 0, "Item0" };
                yield return new object[] { (Predicate<string>)(key => key == "Item1"), 1, "Item0" };
                yield return new object[] { (Predicate<string>)(key => key == "Item3"), 3, "Item0" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        class InsterBeforeData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (Predicate<string>)(key => key == "Never"), 3, "Item0" };
                yield return new object[] { (Predicate<string>)(key => key == "Item1"), 0, "Item0" };
                yield return new object[] { (Predicate<string>)(key => key == "Item3"), 2, "Item0" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        class ReplaceWhileValueData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Item")), "Item0", (Action<IList<string>>)(list => list.ShouldAllBe(f => f == "Item0")) };
                yield return new object[] { (Predicate<string>)(key => key == "Item1"), "Item0", (Action<IList<string>>)(list => list[0].ShouldBe("Item0")) };
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Temp")), "Item0", (Action<IList<string>>)(list => list.ShouldAllBe(f => f != "Item0")) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        class ReplaceWhileFactoryData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Item")), (Func<string, string>)(old => "Item0"), (Action<IList<string>>)(list => list.ShouldAllBe(f => f == "Item0")) };
                yield return new object[] { (Predicate<string>)(key => key == "Item1"), (Func<string, string>)(old => "Item0"), (Action<IList<string>>)(list => list[0].ShouldBe("Item0")) };
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Temp")), (Func<string, string>)(old => "Item0"), (Action<IList<string>>)(list => list.ShouldAllBe(f => f != "Item0")) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        class ReplaceOneSelectorData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Item")), "Item0", (Action<IList<string>>)(list => list.ShouldContain(f => f == "Item0", 1)) };
                yield return new object[] { (Predicate<string>)(key => key == "Item1"), "Item0", (Action<IList<string>>)(list => list[0].ShouldBe("Item0")) };
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Temp")), "Item0", (Action<IList<string>>)(list => list.ShouldAllBe(f => f != "Item0")) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        class ReplaceOneFactoryData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Item")), (Func<string, string>)(old => "Item0"), (Action<IList<string>>)(list => list.ShouldContain(f => f == "Item0", 1)) };
                yield return new object[] { (Predicate<string>)(key => key == "Item1"), (Func<string, string>)(old => "Item0"), (Action<IList<string>>)(list => list[0].ShouldBe("Item0")) };
                yield return new object[] { (Predicate<string>)(key => key.StartsWith("Temp")), (Func<string, string>)(old => "Item0"), (Action<IList<string>>)(list => list.ShouldAllBe(f => f != "Item0")) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        class ReplaceOneItemData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "Item1", "Item0", (Action<IList<string>>)(list => list[0].ShouldBe("Item0")) };
                yield return new object[] { "Item3", "Item0", (Action<IList<string>>)(list => list[2].ShouldBe("Item0")) };
                yield return new object[] { "Temp", "Item0", (Action<IList<string>>)(list => list.ShouldAllBe(f => f != "Item0")) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
