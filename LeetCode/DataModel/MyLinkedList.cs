using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    /// <summary>
    /// Your MyLinkedList object will be instantiated and called as such:
    /// MyLinkedList obj = new MyLinkedList();
    /// int param_1 = obj.Get(index);
    /// obj.AddAtHead(val);
    /// obj.AddAtTail(val);
    /// obj.AddAtIndex(index, val);
    /// obj.DeleteAtIndex(index);
    /// </summary>
    public class MyLinkedList
    {
        private ListNode _head;
        private int _size;
        /** Initialize your data structure here. */
        public MyLinkedList()
        {
        }

        /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
        public int Get(int index)
        {
            if (index + 1 < _size)
            {
                return -1;
            }

            var count = index;
            var temp = _head;
            while (count >= 0)
            {
                temp = temp.next;
                count--;
            }

            return temp.val;
        }

        /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
        public void AddAtHead(int val)
        {
            if (_head == null)
            {
                _head = new ListNode(val);
            }
            else
            {
                var node = new ListNode(val) { next = _head };
                _head = node;
            }
            _size++;
        }

        /** Append a node of value val to the last element of the linked list. */
        public void AddAtTail(int val)
        {
            if (_head == null)
            {
                _head = new ListNode(val);
            }
            else
            {
                var temp = _head;
                while (temp.next != null)
                {
                    temp = temp.next;
                }

                temp.next = new ListNode(val);
            }

            _size++;
        }

        /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
        public void AddAtIndex(int index, int val)
        {
            if (index + 1  < _size)
            {
                Console.WriteLine("Given index is greater than the length, the node will not be inserted");
            }
            else
            {
                var count = index;
                var temp = _head;
                while (count > 0)
                {
                    temp = temp.next;
                    count--;
                }

                temp.next = new ListNode(val);
                _size++;
            }
        }

        /** Delete the index-th node in the linked list, if the index is valid. */
        public void DeleteAtIndex(int index)
        {
            if (index + 1 < _size)
            {
                Console.WriteLine("Given index is greater than the length, the node will not be deleted");
            }
            else
            {
                var count = index;
                var temp = _head;
                while (count > 0)
                {
                    temp = temp.next;
                    count--;
                }

                temp.next = temp.next.next;
                _size--;
            }

        }
    }
}
