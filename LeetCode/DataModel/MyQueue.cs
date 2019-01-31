using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class MyQueue
    {

        private readonly Stack<int> input;

        private readonly Stack<int> output;
        /** Initialize your data structure here. */
        public MyQueue()
        {
            input = new Stack<int>();
            output = new Stack<int>();
        }

        /** Push element x to the back of queue. */
        public void Push(int x)
        {
            input.Push(x);
        }

        /** Removes the element from in front of queue and returns that element. */
        public int Pop()
        {
            if (!output.Any())
            {
                while (input.Any())
                {
                    output.Push(input.Pop());
                }
            }
            return output.Pop();
        }

        /** Get the front element. */
        public int Peek()
        {
            if (!output.Any())
            {
                while (input.Any())
                {
                    output.Push(input.Pop());
                }
            }

            return output.Peek();
        }

        /** Returns whether the queue is empty. */
        public bool Empty()
        {
            return output.Count == 0 && input.Count == 0;
        }
    }

}
