using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class StackSet
    {

        [Test]
        public void Test()
        {
            var s = "3+2*2";
            var r = Calculate(s);
        }
        /// <summary>
        /// Add all none operators to a stack.
        /// If operator, Pop two from stack generate number from operator. Save back sto stack.
        /// (a, b, c, +, -)
        /// 1. when +, Pop b and c and form new b' and put back to stack (a, b')
        /// 2. when -, Pop b' and a and form new a'
        /// 3. reach end, Pop a' as result.
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

        public int Calculate(string s)
        {
            var stack = new Stack<int>();
            int result = 0;
            int number = 0;
            int sign = 1;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (char.IsDigit(c))
                {
                    number = number * 10 + c - '0';
                }
                else if(c == '+')
                {
                    result += sign * number;
                    number = 0;
                    sign = 1;
                }
                else if(c == '-')
                {
                    result += sign * number;
                    number = 0;
                    sign = -1;
                }else if (c == '(')
                {
                    stack.Push(result);
                    stack.Push(sign);
                    sign = 1;
                    result = 0;
                }
                else if(c == ')')
                {
                    result += sign * number;
                    number = 0;
                    result *= stack.Pop();
                    result += stack.Pop();
                }
            }

            if (number != 0)
            {
                result += sign * number;
            }

            return result;
        }

        //227. Basic Calculator II
        //Scan the string from left to right, when see * or /, calculate the result and push to stack, else push numbers with sign to stack.After finishing all * and /, we do + and - operation.
        //Need to handle multiple digits number, num = num * 10 + c - '0'
        //Need to keep track of signs
        public int CalculateTwo(string s)
        {
            var num = 0;
            var stack = new Stack<int>();
            char sign = '+';
            for (var i = 0; i < s.Length; i++)
            {

                char c = s[i];
                if (char.IsDigit(c))
                {
                    num = num * 10 + c - '0';
                }

                if (c != ' ' && !char.IsDigit(c) || i == s.Length - 1)
                {
                    if (sign == '+')
                    {
                        stack.Push(num);
                    }
                    else if (sign == '-')
                    {
                        stack.Push(-num);
                    }
                    else if (sign == '*')
                    {
                        stack.Push(stack.Pop() * num);
                    }
                    else
                    {
                        stack.Push(stack.Pop() / num);
                    }

                    sign = c;
                    num = 0;
                }
            }

            var r = 0;
            while (stack.Any())
            {
                r += stack.Pop();
            }

            return r;
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
