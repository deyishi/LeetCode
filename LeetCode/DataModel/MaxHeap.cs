using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.DataModel
{
    public class MaxHeapTest
    {
        [Test]
        public void Test()
        {
            var n = new MaxHeap<char>();
            n.Push('a');
            n.Push('c');
            var t = n.Pop();
        }
    }

    public class MaxHeap<T> where T : IComparable<T>
    {
        private int size;
        private int capacity;
        private T[] items;

        public MaxHeap()
        {
            size = 0;
            capacity = 10;
            items = new T[capacity];
        }

        public T Pop()
        {
            if (size == 0)
            {
                throw new InvalidOperationException();
            }

            T result = items[0];

            items[0] = items[size - 1];
            size--;
            HeapifyDown();
            return result;
        }

        public void Push(T value)
        {
            EnsureExtraCapacity();
            items[size] = value;
            size++;
            HeapifyUp();
        }

        public T Peek()
        {
            if (size == 0)
            {
                throw new InvalidOperationException();
            }

            return items[0];
        }

        public void Remove(T item)
        {
            if (size == 0)
            {
                throw new InvalidOperationException();
            }

            var list = items.ToList();
            list.Remove(item);
            items = list.ToArray();
            size--;
            HeapifyUp();
            HeapifyDown();
        }

        private void HeapifyUp()
        {
            int index = size - 1;
            while (HasParent(index) && GetParent(index).CompareTo(items[index]) < 0)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        private void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int biggerChild = GetLeftChildIndex(index);

                if (HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) > 0)
                {
                    biggerChild = GetRightChildIndex(index);
                }

                if (items[index].CompareTo(items[biggerChild]) > 0)
                {
                    break;
                }

                Swap(biggerChild, index);
                index = biggerChild;
            }

        }

        private T GetRightChild(int index)
        {
            return items[(index * 2) + 2];
        }

        private bool HasRightChild(int index)
        {
            return (index * 2) + 2 < size;
        }

        private int GetRightChildIndex(int index)
        {
            return (index * 2) + 2;
        }

        private T GetLeftChild(int index)
        {
            return items[(index * 2) + 1];
        }

        private int GetLeftChildIndex(int index)
        {
            return (index * 2) + 1;
        }

        private bool HasLeftChild(int index)
        {
            return (index * 2) + 1 < size;
        }

        private bool HasParent(int index)
        {
            return (index - 1) / 2 >= 0;
        }


        private T GetParent(int index)
        {
            return items[(index - 1) / 2];
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }


        public void EnsureExtraCapacity()
        {
            if (size == capacity)
            {
                var temp = new T[capacity * 2];
                var i = 0;
                foreach (var item in items)
                {
                    temp[i++] = item;
                }

                items = temp;
                capacity *= 2;
            }
        }

        public bool Any()
        {
            return size > 0;
        }

        public void Swap(int i, int j)
        {
            var temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
}
