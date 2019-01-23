using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace LeetCode.DataModel
{
    class MyHeapTest
    {
        [Test]
        public void METHOD()
        {
            var heap = new MyHeap();
            heap.Push(10);
            heap.Push(3);

        }
    }

    public class MyHeap
    {
        private int _size;
        private int _capacity = 10;
        private int[] _items;
        public MyHeap()
        {
            _size = 0;
            _capacity = 10;
            _items = new int[_capacity];
        }

        public int Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            var result = _items[0];

            _items[0] = _items[_size - 1];
            _size--;
            HeapifyDown();
            return result;
        }


        public void Push(int value)
        {
            EnsureExtraCapacity();
            _items[_size] = value;
            _size++;
            HeapifyUp();
        }

        public int Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            return _items[0];
        }

        private void HeapifyUp()
        {
            int index = _size - 1;
            while (HasParent(index) && GetParent(index) > _items[index])
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
                int smallerChildIndex = GetLeftChildIndex(index);

                if (HasRightChild(index) && GetRightChild(index) < GetLeftChild(index))
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (_items[index] < _items[smallerChildIndex])
                {
                    break;
                }

                Swap(smallerChildIndex, index);
                index = smallerChildIndex;
            }
        }



        private int GetRightChild(int index)
        {
            return _items[(index * 2) + 2];
        }

        private bool HasRightChild(int index)
        {
            return (index * 2) + 2 < _size;
        }
        private int GetRightChildIndex(int index)
        {
            return (index * 2) + 2;
        }

        private int GetLeftChild(int index)
        {
            return _items[(index * 2) + 1];
        }

        private int GetLeftChildIndex(int index)
        {
            return (index * 2) + 1;
        }

        private bool HasLeftChild(int index)
        {
            return (index * 2) + 1 < _size;
        }

        private bool HasParent(int index)
        {
            return (index - 1) / 2 >= 0;
        }


        private int GetParent(int index)
        {
            return _items[(index - 1) / 2];
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }


        public void EnsureExtraCapacity()
        {
            if (_size == _capacity)
            {
                var temp = new int[_capacity * 2];
                var i = 0;
                foreach (var item in _items)
                {
                    temp[i++] = item;
                }
                _items = temp;
                _capacity *= 2;
            }
        }

        public void Swap(int i, int j)
        {
            var temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }
    }
}
