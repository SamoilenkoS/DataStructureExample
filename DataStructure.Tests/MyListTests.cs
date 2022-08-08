using DataStructureLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Tests
{
    public class Tests
    {
        static private void Swap<T>(ref T x, ref T y)
        {
            var temp = x;
            x = y;
            y = temp;
        }

        public static IEnumerable<T> Sort<T>(T[] items, Func<T, T, int> comparer)
        {
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = i + 1; j < items.Length; j++)
                {
                    if (comparer(items[i], items[j]) > 0)
                    {
                        Swap(ref items[i], ref items[j]);
                    }
                }
            }

            foreach (var item in items)
            {
                yield return item;
            }
        }

        [Test]
        public void TT()
        {
            var array = new int[] { 1, 5, 4, 2, 3 };
            var sorted = Sort(array, (x, y) => x.CompareTo(y));

        }

        [Test]
        public void Test()
        {
            var items = new int[5];
            for (int i = 0; i < 5; i++)
            {
                items[i] = (i + 1) * 2;
            }

            items.Count(x => x % 2 == 0);
            var isRight = items.All(MoreThanTen);
            var response = items.Select(QQQ);
            response.Count();

            TT(() => 
            {
                Console.WriteLine();
                var items = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    items[i] = (i + 1) * 2;
                }
            });
        }

        private bool MoreThanTen(int arg)
        {
            return arg < 10;
        }

        public void TT(Action action)
        {
            //
            //
            action?.Invoke();
            //
            //
        }
        //count
        //foreach(var item in items)
        //{if(action.Invoke(item))
        //count++}
        //return count;
        private object QQQ(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }
    }
    public class ArrayListTests : MyListTests<MyArray<int>>
    {
        public override IMyList<int> CreateList(int[] sourceArray)
        {
            return new MyArray<int>(sourceArray);
        }
    }

    public class LinkedListTests : MyListTests<MyArray<int>>
    {
        public override IMyList<int> CreateList(int[] sourceArray)
        {
            return new MyArray<int>(sourceArray);
        }
    }

    public abstract class MyListTests<T> where T : IMyList<int>
    {
        public abstract IMyList<int> CreateList(int[] sourceArray);

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 10, new[] { 1, 2, 3, 4, 5, 10 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0, new[] { 1, 2, 3, 4, 5, 0 })]
        [TestCase(new[] { 1, 2 }, 0, new[] { 1, 2, 0 })]
        [TestCase(new[] { 1 }, 4, new[] { 1, 4 })]
        public void AddBack_WhenArrayFilled_ShouldAddNewElementToBack
            (int[] sourceArray, int valueToAdd, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddBack(valueToAdd);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 10, new[] { 1, 2, 3, 4, 5, 10, 10 })]
        public void AddBack_WhenArrayFilled_ShouldAddNewFewElementsToBack
            (int[] sourceArray, int valueToAdd, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddBack(valueToAdd);
            myArrayList.AddBack(valueToAdd);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 10, new[] { 10, 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0, new[] { 0, 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2}, -2, new[] { -2, 1, 2 })]
        [TestCase(new[] { 1 }, 77, new[] { 77, 1 })]
        [TestCase(new int[0] { }, 17, new[] { 17 })]
        public void AddFront_WhenArrayFilled_ShouldAddNewElementToFront
            (int[] sourceArray, int valueToAdd, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddFront(valueToAdd);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2 }, -2, new[] { -2, -2, 1, 2 })]
        public void AddFront_WhenArrayFilled_ShouldAddNewFewElementToFront
            (int[] sourceArray, int valueToAdd, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddFront(valueToAdd);
            myArrayList.AddFront(valueToAdd);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 65 }, 0, 65)]
        [TestCase(new[] { 5, 21 }, 1, 21)]
        [TestCase(new[] { 5, 3, 10, -90, 5, 0 }, 3, -90)]
        [TestCase(new[] { 5, 3, 10, -90, 5, 0 }, 0, 5)]
        [TestCase(new[] { 5, 3, 10, -90, 5, 0 }, 5, 0)]
        public void IndexerGet_WhenValidIndexAndArrayFilled_ShouldReturnValueByIndex
            (int[] sourceArray, int index, int expected)
        {
            var myList = CreateList(sourceArray);

            int actual = myList[index];

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0])]
        public void IndexerGet_WhenEmptyArray_ShouldThrowIndexOutOfRange
            (int[] sourceArray)
        {
            var myList = CreateList(sourceArray);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var item = myList[0];
            });
        }

        [TestCase(new[] { 1 }, 2)]
        [TestCase(new[] { 1 }, -1)]
        [TestCase(new[] { 1, 6, 3, 4, 1 }, 5)]
        [TestCase(new[] { 1, 6, 3, 4, 1 }, -10)]
        public void IndexerGet_WhenInvalidIndex_ShouldThrowIndexOutOfRange
            (int[] sourceArray, int index)
        {
            var myList = CreateList(sourceArray);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var item = myList[index];
            });
        }

        [TestCase(new[] { 65 }, 0, 77, 77)]
        [TestCase(new[] { 5, 21 }, 1, -2, -2)]
        [TestCase(new[] { 5, 3, 10, -90, 5, 0 }, 3, 5, 5)]
        [TestCase(new[] { 5, 3, 10, -90, 5, 0 }, 0, 11, 11)]
        [TestCase(new[] { 5, 3, 10, -90, 5, 0 }, 5, 9, 9)]
        public void IndexerSet_WhenValidIndexAndArrayFilled_ShouldSetValueByIndex
            (int[] sourceArray, int index, int setValue, int expected)
        {
            var myList = CreateList(sourceArray);

            myList[index] = setValue;
            int actual = myList[index];

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[0], 5)]
        public void IndexerSet_WhenEmptyArray_ShouldThrowIndexOutOfRange
            (int[] sourceArray, int value)
        {
            var myList = CreateList(sourceArray);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                value = myList[0];
            });
        }

        [TestCase(new[] { 1 }, 2, 6)]
        [TestCase(new[] { 1 }, -1, 8)]
        [TestCase(new[] { 1, 6, 3, 4, 1 }, 5, 15)]
        [TestCase(new[] { 1, 6, 3, 4, 1 }, -10, 88)]
        public void IndexerSet_WhenInvalidIndex_ShouldThrowIndexOutOfRange
            (int[] sourceArray, int index, int value)
        {
            var myList = CreateList(sourceArray);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                value = myList[index];
            });
        }

        [Test]
        public void ArrayConstructor_WhenNullPassed_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var myList = new MyArray<int>(null);
            });
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 2, 6, new[] { 1, 2, 6, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0, 9, new[] { 9, 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 1, 2 }, 1, -2, new[] { 1, -2, 2 })]
        [TestCase(new[] { 1 }, 1, 77, new[] { 1, 77 })]
        [TestCase(new int[0] { }, 0, 17, new[] { 17 })]
        public void AddByIndex_WhenArrayFilled_ShouldAddNewElementByIndex
            (int[] sourceArray, int index, int valueToAdd, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddByIndex(index, valueToAdd);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 5, new[] { 1, 2, 3, 4 })]
        [TestCase(new[] { 1, 2, 3}, 3, new[] { 1, 2 })]
        [TestCase(new[] { 1 }, 1, new int[0] { })]
        public void RemoveBack_WhenArrayFilled_ShouldReturnLastElement
            (int[] sourceArray,  int expected, int[] arrayWithoutRemovedElement)
        {
            var myArrayList = CreateList(sourceArray);

            int actual = myArrayList[myArrayList.Length - 1];
            myArrayList.RemoveBack();

            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(arrayWithoutRemovedElement, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 1, new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { -2, -7, 11 }, -2, new[] { -7, 11 })]
        [TestCase(new[] { 5 }, 5, new int[0] { })]
        public void RemoveFront_WhenArrayFilled_ShouldReturnFirstItem
            (int[] sourceArray, int expected, int[] arrayWithoutRemovedElement)
        {
            var myArrayList = CreateList(sourceArray);

            int actual = myArrayList[0];
            myArrayList.RemoveFront();

            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(arrayWithoutRemovedElement, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 2, 3, new[] { 1, 2, 4, 5 })]
        [TestCase(new[] { -2, -7, 11 }, 0, -2, new[] { -7, 11 })]
        [TestCase(new[] { -6 }, 0, -6, new int[0] { })]
        public void RemoveByIndex_WhenArrayFilled_ShouldReturnElementByIndex
            (int[] sourceArray, int index, int expected, int[] arrayWithoutRemovedElement)
        {
            var myArrayList = CreateList(sourceArray);

            int actual = myArrayList[index];
            myArrayList.RemoveByIndex(index);

            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(arrayWithoutRemovedElement, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 2, new[] { 4, 5}, new[] { 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 4, new[] { 2, 3, 4, 5 }, new[] { 1, })]
        [TestCase(new[] { -2, -7, 11 }, 1, new[] { 11 }, new[] { -2, -7 })]
        [TestCase(new[] { 5 }, 0, new int[0] { }, new[] { 5 })]
        public void RemoveNValuesBack_WhenArrayFilled_ShouldReturnNewArrayWithoutDeletedValuesFromBack
            (int[] sourceArray, int n, int[] removedValuesArray, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            CollectionAssert.AreEqual(removedValuesArray, myArrayList.RemoveNValuesBack(n));
            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 2, new[] { 1, 2 }, new[] { 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 4, new[] { 1, 2, 3, 4 }, new[] { 5 })]
        [TestCase(new[] { -2, -7, 11 }, 1, new[] { -2 }, new[] { -7, 11 })]
        public void RemoveNValuesFront_WhenArrayFilled_ShouldReturnNewArrayWithoudDeletedValuesFromFront
            (int[] sourceArray, int n, int[] removedValuesArray, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            CollectionAssert.AreEqual(removedValuesArray, myArrayList.RemoveNValuesFront(n));
            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 2, 3, new[] { 3, 4, 5 }, new[] { 1, 2 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 1, 4, new[] { 2, 3, 4, 5 }, new[] { 1 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 3, 2, new[] { 4, 5 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { -2, -7, 11}, 0, 3, new[] { -2, -7, 11 }, new int[0] { })]
        [TestCase(new[] { -2, -7, 11 }, 1, 1, new[] { -7 }, new[] { -2, 11 })]
        [TestCase(new[] { 1, 2, }, 0, 0, new int[0] { }, new[] { 1, 2, })]
        public void RemoveNValuesByIndex_WhenNValuesByIndexDeleted_ShouldReturnDeletedNumbers
            (int[] sourceArray, int index, int n, int[] removedValuesArray, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(removedValuesArray, myArrayList.RemoveNValuesByIndex(index, n));
            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 2, 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 1, 0)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 3, 2)]
        [TestCase(new[] { -2, -7, 11 }, 11, 2)]
        [TestCase(new[] { 1, -82347, 66 }, -82347, 1)]
        public void IndexOf_WhenArrayFilled_ShouldReturnIndexOfElement
            (int[] sourceArray, int element, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.IndexOf(element));
        }

        [TestCase(new[] { 1 }, new[] { 1 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 8 }, new[] { 8, 5, 4, 3, 2, 1 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 5, 4, 3, 2, 1 })]
        [TestCase(new[] { -2, -7, 11 }, new[] { 11, -7, -2})]
        [TestCase(new[] { 1, -82347, 66 }, new[] { 66, -82347, 1})]
        public void Reverse_WhenArrayFilled_ShouldReturnReversedArray
            (int[] sourceArray, int[] expected)
        {
            var myArrayList = CreateList(sourceArray);
            myArrayList.Reverse();

            CollectionAssert.AreEqual(expected, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 5)]
        [TestCase(new[] { -11, 0, -2, -33, -22 }, 0)]
        [TestCase(new[] { -3, -1, -88, -7, -11 }, -1)]
        [TestCase(new[] { -2, 11 }, 11)]
        [TestCase(new[] { 66 }, 66)]
        public void Max_WhenArrayFilled_ShouldReturnMaxElementOfArray
            (int[] sourceArray, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.Max());
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 1)]
        [TestCase(new[] { -11, 0, -2, -33, -22 }, -33)]
        [TestCase(new[] { 3, 0, 88, 7, 11 }, 0)]
        [TestCase(new[] { -2, 11 }, -2)]
        [TestCase(new[] { 66 }, 66)]
        public void Min_WhenArrayFilled_ShouldReturnMinElementOfArray
            (int[] sourceArray, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.Min());
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 4)]
        [TestCase(new[] { -11, 0, -2, -33, -22 }, 1)]
        [TestCase(new[] { 3, 0, 88, 7, 11 }, 2)]
        [TestCase(new[] { 555, 0, 88, 7, 11 }, 0)]
        [TestCase(new[] { -2, 11 }, 1)]
        [TestCase(new[] { 66 }, 0)]
        public void MaxIndex_WhenArrayFilled_ShouldReturnMaxElementsIndex
            (int[] sourceArray, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.MaxIndex());
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0)]
        [TestCase(new[] { -11, 0, -2, -33, -22 }, 3)]
        [TestCase(new[] { 3, 0, 88, 7, 11 }, 1)]
        [TestCase(new[] { -2, 11 }, 0)]
        [TestCase(new[] { 66 }, 0)]
        public void MinIndex_WhenArrayFilled_ShouldReturnMinElementsIndex
            (int[] sourceArray, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.MinIndex());
        }

        [TestCase(new[] { 1 }, new[] { 1 })]
        [TestCase(new[] { 5, 4, 22, 7, -2, 8 }, new[] { -2, 4, 5, 7, 8, 22 })]
        [TestCase(new[] { -61, -2, -983, 0, 11 }, new[] { -983, -61, -2, 0, 11 })]
        [TestCase(new[] { -2, -7}, new[] { -7, -2 })]
        public void SortAscending_WhenArrayFilled_ShouldReturnArrayInAscendingOrder
            (int[] sourceArray, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.Sort();

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1 }, new[] { 1 })]
        [TestCase(new[] { 5, 4, 22, 7, -2, 8 }, new[] { 22, 8, 7, 5, 4, -2 })]
        [TestCase(new[] { -61, -2, -983, 0, 11 }, new[] { 11, 0, -2, -61, -983 })]
        [TestCase(new[] { -2, -7 }, new[] { -2, -7 })]
        public void SortDescending_WhenArrayFilled_ShouldReturnArrayInDescendingOrder
            (int[] sourceArray, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.Sort(false);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 8 }, 5, new[] { 1, 2, 3, 4, 8 }, 4)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 1, new[] { 2, 3, 4, 5 }, 0)]
        [TestCase(new[] { -2, -7, 11 }, 11, new[] { -2, -7 }, 2)]
        [TestCase(new[] { 1, -82347, 66 }, 0, new[] { 1, -82347, 66 }, -1)]
        public void RemoveByValue_WhenValueRemoved_ShouldReturnIndexOfRemovedValue
            (int[] sourceArray, int value, int[] arrayWithoutValue, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.RemoveByValue(value));
            CollectionAssert.AreEqual(arrayWithoutValue, myArrayList);
        }

        [TestCase(new[] { 1, 5, 2, 5, 3, 4, 5, 8 }, 5, new[] { 1, 2, 3, 4, 8 }, 3)]
        [TestCase(new[] { 1, 2, 3, 1, 4, 5 }, 1, new[] { 2, 3, 4, 5 }, 2)]
        [TestCase(new[] { 11, -2, -7, 11, 11, 11 }, 11, new[] { -2, -7 }, 4)]
        [TestCase(new[] { 1, -82347, 66 }, 0, new[] { 1, -82347, 66 }, 0)]
        public void RemoveByValueAll_WhenValueRemoved_ShouldReturnCountOfRemovedElements
            (int[] sourceArray, int value, int[] arrayWithoutValue, int expected)
        {
            var myArrayList = CreateList(sourceArray);

            Assert.AreEqual(expected, myArrayList.RemoveByValueAll(value));
            CollectionAssert.AreEqual(arrayWithoutValue, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 3, 2, 2, 0, -5 }, new[] { 3, 2, 2, 0, -5, 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 0, 0, 0 }, new[] { 1, 3, 5}, new[] { 1, 3, 5, 0, 0, 0 })]
        [TestCase(new int[0], new[] { 7, 22 }, new[] { 7, 22 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new int[0], new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 99 }, new[] { 0 }, new[] { 0, 99 })]
        public void AddFrontIEnum_WhenFilled_ShouldReturnFullArrayWithNewItemsFront
            (int[] sourceArray, int[] items, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddFront(items);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 3, 2, 2, 0, -5 }, new[] { 1, 2, 3, 4, 5, 3, 2, 2, 0, -5 })]
        [TestCase(new[] { 0, 0, 0 }, new[] { 1, 3, 5 }, new[] { 0, 0, 0, 1, 3, 5 })]
        [TestCase(new int[0], new[] { 7, 22 }, new[] { 7, 22 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new int[0], new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 99 }, new[] { 0 }, new[] { 99, 0 })]
        public void AddBackIEnum_WhenFilled_ShouldReturnFullArrayWithNewItemsBack
            (int[] sourceArray, int[] items, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddBack(items);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 1, new[] { 3, 2, 2, 0, -5 }, new[] { 1, 3, 2, 2, 0, -5, 2, 3, 4, 5 })]
        [TestCase(new[] { 0, 0, 0 }, 0, new[] { 1, 3, 5 }, new[] { 1, 3, 5, 0, 0, 0 })]
        [TestCase(new[] { 0, 0, 0 }, 2, new[] { 1, 3, 5 }, new[] { 0, 0, 1, 3, 5, 0 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 3, new int[0], new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { 99 }, 0, new[] { 0 }, new[] { 0, 99 })]
        public void AddByIndexIEnum_WhenFilled_ShouldReturnArrayFullArrayWithAddedItemsByIndex
            (int[] sourceArray, int index, int[] items, int[] expectedArray)
        {
            var myArrayList = CreateList(sourceArray);

            myArrayList.AddByIndex(index, items);

            CollectionAssert.AreEqual(expectedArray, myArrayList);
        }
    }
}