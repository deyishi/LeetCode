using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
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
            var s = "1";
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
            Stack<int> stack = new Stack<int>(); // For numbers in parentheses 
            int num = 0;
            int sign = 1;
            int result = 0;
            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    num = num * 10 + c - '0';
                }else if (c == '-')
                {
                    result += sign * num;
                    num = 0;
                    sign = -1;
                }
                else if (c == '+')
                {
                    result += sign * num;
                    num = 0;
                    sign = 1;
                }
                else if(c == '(')
                {
                   stack.Push(result);
                   stack.Push(sign);
                   result = 0;
                   sign = 1;
                }
                else if(c == ')')
                {
                    result += sign * num;
                    num = 0;

                    result *= stack.Pop(); //Sign before (
                    result += stack.Pop(); // Sign will update after )
                }
            }

            if (num != 0)
            {
                result += sign * num;
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

        public bool Find132Pattern(int[] nums)
        {
            Stack<int> small = new Stack<int>();
            Stack<int> large = new Stack<int>();
            foreach (var n in nums)
            {
                if (!small.Any() ||  n <= small.Peek())
                {
                    small.Push(n);
                }
                else
                {
                    while (large.Any() && n >= large.Peek())
                    {
                        large.Pop();
                    }

                    int temp = small.Peek();
                    while (small.Count > large.Count)
                    {
                        small.Pop();
                    }

                    if (small.Any() && n > small.Peek())
                    {
                        return true;
                    }
                    small.Push(temp);
                    large.Push(n);
                }
            }

            return false;
        }

        public int[] AsteroidCollision(int[] asteroids)
        {

            Stack<int> s = new Stack<int>();
            for(int i = 0; i < asteroids.Length; i++)
            {
                int a = asteroids[i];
                if (a > 0)
                {
                    s.Push(a);
                }
                else
                {
                    if (s.Count == 0 || s.Peek() < 0)
                    {
                        s.Push(a);
                    }else if (Math.Abs(s.Peek()) <= Math.Abs(a))
                    {
                        // Remove destroyed negative asteroids.
                        if (Math.Abs(s.Peek()) < Math.Abs(a))
                        {
                            i--;
                        }

                        s.Pop();
                    }
                }
            }

            Stack<int> rev = new Stack<int>();
            while (s.Count != 0)
            {
                rev.Push(s.Pop());
            }

            return rev.ToArray();
        }

        public string CountOfAtoms(string formula)
        {
            int i = 0;
            var map = CountOfAtomsHelper(formula, ref i);
            string result = string.Empty;
            foreach (var p in map.OrderBy(x=>x.Key))
            {
                result += p.Key;
                if (p.Value > 1)
                {
                    result += p.Value;
                }
            }

            return result;
        }

        public Dictionary<string, int> CountOfAtomsHelper(string s, ref int i)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            while (i != s.Length)
            {
                if (s[i] == '(')
                {
                    i++;
                    var t = CountOfAtomsHelper(s, ref i);
                    var count = GetCount(s, ref i);
                    foreach (var p in t) {
                        if (!map.ContainsKey(p.Key))
                        {
                            map.Add(p.Key, p.Value * count);
                        }
                        else
                        {
                            map[p.Key] += p.Value * count;
                        }
                    }
                }else if (s[i] == ')')
                {
                    i++;
                    return map;
                }
                else
                {
                    string name = GetAtomName(s, ref i);
                    int n = GetCount(s, ref i);
                    if (!map.ContainsKey(name))
                    {
                        map.Add(name, n);
                    }
                    else
                    {
                        map[name] += n;
                    }
                }
            }

            return map;
        }

        public string GetAtomName(string s, ref int i)
        {
            string name = string.Empty;

            while (i < s.Length && char.IsLetter(s[i]) && (string.IsNullOrEmpty(name) || char.IsLower(s[i])))
            {
                name += s[i];
                i++;
            }

            // return name when it is ( or another element.
            return name;
        }

        public int GetCount(string s, ref int i)
        {
            string c = string.Empty;
            while (i < s.Length && char.IsDigit(s[i]))
            {
                c += s[i++];
            }

            // H, the count is 1.
            return string.IsNullOrEmpty(c) ? 1 : int.Parse(c);
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
