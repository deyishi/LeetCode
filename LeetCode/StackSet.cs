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
            var s = "2*(5+5*2)/3+(6/2+8)";
            var r = CalculateThree(s);
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
                // Digit, take count of 2 digits number
                if (char.IsDigit(c))
                {
                    number = number * 10 + c - '0';
                }else if ( c == '-')
                {
                    // +, add previous number and start a new number with positive sign
                    result += sign * number;
                    number = 0;
                    sign = -1;
                }
                else if (c == '+')
                {
                    // -, add previous number and start a new number with negative sign
                    result += sign * number;
                    number = 0;
                    sign = 1;
                }
                else if(c == '(') { 
                    // Push current result and sign to stack. set result = 0 for calculating a new result inside ().
                    stack.Push(result);
                    stack.Push(sign);
                    sign = 0;
                    result = 0;
                }
                else if(c == ')')
                {
                    // Calculate the pair before )
                    result += sign * number;
                    number = 0;

                    // Value in () times sign before (). 
                    result *= stack.Pop();

                    // Add value in () to value before ()
                    result += stack.Pop();
                }
            }

            // Add last digit
            if (number != 0)
            {
                result += sign * number;
            }

            return result;
        }


        public int CalculateThree(string s)
        {
            int o1 = 1;
            int l1 = 0;
            int o2 = 1;
            int l2 = 1;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                // Digit, take count of 2 digits number
                if (char.IsDigit(c))
                {
                    int num = c - '0';
                    while (i + 1 < s.Length && char.IsDigit(s[i+1]))
                    {
                        i++;
                        num = num * 10 + s[i] - '0';
                    }

                    // If no level two operation, l2 will be current number.
                    l2 = o2 == 1 ? l2 * num : l2 / num;
                }
                else if (c == '-' || c == '+')
                {
                    // If no level two operation, l2 will be previous number.
                    l1 = l1 + o1 * l2;
                    o1 = c == '-' ? -1 : 1;
                    // Reset l2 and o2, so next number will be recorded as previous number.
                    l2 = 1;
                    o2 = 1;
                }
                else if (c == '*' || c == '/')
                {
                    o2 = c == '/' ? -1 : 1;
                }
                else if (c == '(')
                {
                    int start = i + 1;
                    int count = 0;
                    while (i < s.Length)
                    {
                        if (s[i] == '(')
                        {
                            count++;
                        }

                        if (s[i] == ')')
                        {
                            count--;
                        }

                        if (count == 0)
                        {
                            break;
                        }

                        i++;
                    }

                    int num = CalculateThree(s.Substring(start, i - start));

                    l2 = o2 == 1 ? l2 * num : l2 / num;
                }
            }



            return l1 + o1 * l2;
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
        public int[] ExclusiveTime(int n, IList<string> logs)
        {
            int[] res = new int[n];
            if(n == 0 || logs==null || !logs.Any())
            {
                return res;
            }

            Stack<int> callStack = new Stack<int>();
            int prev = 0;
            foreach (var log in logs)
            {
                string[] parts = log.Split(':');
                if (!callStack.Any())
                {
                    res[callStack.Peek()] += int.Parse(parts[2]) - prev;
                }

                prev = int.Parse(parts[2]);
                if (parts[1] == "start")
                {
                    callStack.Push(int.Parse(parts[0]));
                }
                else
                {
                    // Handle function start at 1 and end 1.
                    res[callStack.Pop()]++;
                    prev++;
                }
            }

            return res;
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
