using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructureLibrary
{
    public class MyArray<T> : IMyList<T> where T : IComparable<T>
    {
        private const int DefaultSize = 4;
        private const double Coef = 1.3;
        private int _count;
        private T[] _array;

        public int Capacity => _array.Length;
        public int Length => _count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                _array[index] = value;
            }
        }

        public MyArray() : this(DefaultSize)
        {
        }

        public MyArray(int size)
        {
            if (size < 0 )
            {
                throw new ArgumentException();
            }

            size = size > DefaultSize ? (int)(size * Coef) : DefaultSize;
            _array = new T[size];
        }

        public MyArray(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentException();
            }

            int size = array.Length > DefaultSize ? (int)(array.Length * Coef) : DefaultSize;
            _array = new T[size];

            for (int i = 0; i < array.Length; i++)
            {
                _array[i] = array[i];
            }

            _count = array.Length;
        }

        public T[] ToArray()
        {
            T[] result = new T[Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = _array[i];
            }

            return result;
        }

        public void AddBack(T itemToAdd)
        {
            ResizeArray();

            AddByIndex(_count, itemToAdd);
        }

        public void AddFront(T itemToAdd)
        {
            ResizeArray();

            AddByIndex(0, itemToAdd);
        }

        public void AddByIndex(int index, T itemToAdd)
        {
            ResizeArray();

            T[] newArray = new T[_array.Length];
            for (int i = 0; i < index; i++)
            {
                newArray[i] = _array[i];
            }

            for (int i = index + 1; i < newArray.Length; i++)
            {
                newArray[i] = _array[i - 1];
            }

            newArray[index] = itemToAdd;
            _array = newArray;
            _count++;
        }

        public T RemoveBack()
        {
            ResizeArray();

            return RemoveByIndex(_count);
        }

        public T RemoveFront()
        {
            ResizeArray();

            return RemoveByIndex(0);
        }

        public T RemoveByIndex(int index)
        {
            ResizeArray();

            T[] newArray = new T[_array.Length];
            T item = _array[index];

            for (int i = 0; i < index; i++)
            {
                newArray[i] = _array[i];
            }

            for (int i = index; i < _count; i++)
            {
                newArray[i] = _array[i + 1];
            }

            _array = newArray;
            _count--;

            return item;
        }

        public T[] RemoveNValuesBack(int n)
        {
            ResizeArray();

            return RemoveNValuesByIndex(_count - n, n);
        }
        
        public T[] RemoveNValuesFront(int n)
        {
            ResizeArray();

            return RemoveNValuesByIndex(0, n);
        }

        public T[] RemoveNValuesByIndex(int index, int n)
        {
            ResizeArray();

            int localCount = 0;
            T[] newArray = new T[_array.Length];
            T[] deletedArray = new T[n];

            do
            {
                if (n > 0)
                {
                    deletedArray[localCount] = _array[index];

                    for (int i = 0; i < index; i++)
                    {
                        newArray[i] = _array[i];
                    }

                    for (int i = index; i < _count; i++)
                    {
                        newArray[i] = _array[i + 1];
                    }

                    localCount++;
                    _count--;
                    _array = newArray;
                }

            } while (localCount < n);

            return deletedArray;
        }

        public int IndexOf(T element)
        {
            ResizeArray();

            int item = -1;
            for (int i = 0; i < _array.Length; i++)
            {
                if (element.CompareTo(_array[i]) == 0)
                {
                    item = i;
                    break;
                }
            }

            if (item == -1)
            {
                throw new IndexOutOfRangeException();
            }

            return item;
        }

        public void Reverse()
        {
            ResizeArray();

            int count = 1;
            T[] newArray = new T[_array.Length];

            for (int i = 0; i < _count; i++)
            {
                newArray[i] = _array[_count - count];
                count++;
            }

            _array = newArray;
        }

        public T Max()
        {
            return _array[MaxIndex()];
        }

        public T Min()
        {
            return _array[MinIndex()];
        }

        public int MaxIndex()
        {
            ResizeArray();

            int maxIndex = 0;

            for (int i = 1; i < _count; i++)
            {
                if (_array[i].CompareTo(_array[maxIndex]) == 1)
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public int MinIndex()
        {
            if (_array == null)
            {
                throw new ArgumentException();
            }

            ResizeArray();

            int minIndex = 0;

            for (int i = 1; i < _count; i++)
            {
                if (_array[i].CompareTo(_array[minIndex]) == -1)
                {
                    minIndex = i;
                }
            }

            return minIndex;
        }

        public void Sort(bool ascending = true)
        {
            ResizeArray();
            if (ascending == true)
            {
                for (int i = 1; i < _count; i++)
                {
                    T value = _array[i];
                    for (int j = i - 1; j >= 0;)
                    {
                        if (value.CompareTo(_array[j]) == -1)
                        {
                            _array[j + 1] = _array[j];
                            j--;
                            _array[j + 1] = value;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _count - 1; i++)
                {
                    int smalestIndex = i;
                    for (int j = i + 1; j < _count; j++)
                    {
                        if (_array[j].CompareTo(_array[smalestIndex]) == 1)
                        {
                            smalestIndex = j;
                        }
                    }

                    Swap(ref _array[smalestIndex], ref _array[i]);
                }
            }

        }

        public int RemoveByValue(T value)
        {
            ResizeArray();

            T[] newArray = new T[_array.Length];
            int removedValueIndex = -1;

            for (int i = 0; i < _count; i++)
            {
                if (value.CompareTo(_array[i]) == 0)
                {
                    removedValueIndex = i;

                    for (int j = 0; j < removedValueIndex; j++)
                    {
                        newArray[j] = _array[j];
                    }

                    for (int j = removedValueIndex; j < _count; j++)
                    {
                        newArray[j] = _array[j + 1];
                    }

                    _array = newArray;
                    _count--;
                    break;
                }
            }
            
            return removedValueIndex;
        }

        public int RemoveByValueAll(T value)
        {
            ResizeArray();

            T[] newArray = new T[_array.Length];
            int countOfRemovedNumbers = 0;
            int elementIndex;

            for (int i = 0; i < _count; i++)
            {
                if (value.CompareTo(_array[i]) == 0)
                {
                    elementIndex = i;

                    for (int j = 0; j < elementIndex; j++)
                    {
                        newArray[j] = _array[j];
                    }

                    for (int j = elementIndex; j < _count; j++)
                    {
                        newArray[j] = _array[j + 1];
                    }

                    _array = newArray;
                    i--;
                    countOfRemovedNumbers++;
                    _count--;
                }
                else if (_array[0].CompareTo(value) == 0)
                {
                    countOfRemovedNumbers++;
                    break;
                }
            }

            return countOfRemovedNumbers;

        }

        public void AddFront(IEnumerable<T> items)
        {
            ResizeArray();

            AddByIndex(0, items);
        }

        public void AddBack(IEnumerable<T> items)
        {
            ResizeArray();

            AddByIndex(_count, items);
        }

        public void AddByIndex(int index, IEnumerable<T> items)
        {
            int localCount = 0;

            foreach (T item in items)
            {
                localCount++;
            }

            T[] newArray = new T[_array.Length + localCount];
            int i = 0;

            for (int j = 0; j < index; j++)
            {
                newArray[j] = _array[j];
                i++;
            }

            foreach (T item in items)
            {
                newArray[i++] = item;
                _count++;
            }

            for (int j = i; j < _count; j++)
            {
                newArray[j] = _array[index];
                index++;
            }

            _array = newArray;
        }

        private void ResizeArray()
        {
            if (Capacity == Length)
            {
                T[] newArray = new T[(int)(_array.Length * Coef)];
                for (int i = 0; i < _array.Length; i++)
                {
                    newArray[i] = _array[i];
                }

                _array = newArray;
            }
        }

        private void Swap (ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}