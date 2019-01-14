using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class StackSet
    {
        /// <summary>
        /// Add all none operators to a stack.
        /// If operator, pop two from stack generate number from operator. Save back sto stack.
        /// (a, b, c, +, -)
        /// 1. when +, pop b and c and form new b' and put back to stack (a, b')
        /// 2. when -, pop b' and a and form new a'
        /// 3. reach end, pop a' as result.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public int EvalRPN(string[] tokens)
        {
            var s = new Stack<int>();
            var operators = "+-*/";

            foreach (var token in tokens)
            {
                if (!operators.Contains(token))
                {
                    s.Push(int.Parse(token));
                    continue;
                }

                var a = s.Pop();
                var b = s.Pop();
                if (token.Equals("+"))
                {
                    s.Push(b + a);
                }
                else if (token.Equals("-"))
                {
                    s.Push(b - a);
                }
                else if (token.Equals("*"))
                {
                    s.Push(b * a);
                }
                else
                {
                    s.Push(b / a);
                }
            }

            return s.Pop();
        }
    }

    public class MinStack
    {
        private Stack<int> _stack;
        private Stack<int> _minStack;
        /** initialize your data structure here. */
        public MinStack()
        {
            _stack = new Stack<int>();
            _minStack = new Stack<int>();
        }

        public void Push(int x)
        {
            _stack.Push(x);
            if (_minStack.Any())
            {
                if (x <= _minStack.Peek())
                {
                    _minStack.Push(x);
                }
            }
            else
            {
                _minStack.Push(x);
            }
        }

        public void Pop()
        {
            var x = _stack.Pop();
            if (_minStack.Any() && x == _minStack.Peek())
            {
                _minStack.Pop();
            }
        }

        public int Top()
        {
            return _stack.Peek();
        }

        public int GetMin()
        {
            return _minStack.Peek();
        }
    }
}
