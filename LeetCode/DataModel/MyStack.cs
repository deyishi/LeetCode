using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.DataModel
{

    //Use a temp queue to store current queue values, empty current queue and push new element to current queue, then move temp queue items back to current queue.
    public class MyStack
    {
        private readonly Queue<int> _queue;
        private readonly Queue<int> _temp;
        /** Initialize your data structure here. */
        public MyStack()
        {
            _queue = new Queue<int>();
            _temp = new Queue<int>();
        }

        /** Push element x onto stack. */
        public void Push(int x)
        {
            while (_queue.Any())
            {
                _temp.Enqueue(_queue.Dequeue());
            }

            _queue.Enqueue(x);

            while (_temp.Any())
            {
                _queue.Enqueue(_temp.Dequeue());
            }
        }

        /** Removes the element on top of the stack and returns that element. */
        public int Pop()
        {
            return _queue.Dequeue();
        }

        /** Get the top element. */
        public int Top()
        {
            return _queue.Peek();
        }

        /** Returns whether the stack is empty. */
        public bool Empty()
        {
            return _queue.Count == 0;
        }
    }
}
