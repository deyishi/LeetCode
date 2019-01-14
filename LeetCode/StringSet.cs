using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    class StringSet
    {
        [Test]
        public void LongestValidParentheses()
        {
            var s = "(()()";
            var r = LongestValidParentheses(s);
        }

        public int LongestValidParentheses(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            //Result, Stack to track position of '(', CurrentSetLength(Length of current finished parentheses using all the '(' in the Stack).
            var result = 0;
            var currentSetCount = 0; 
            var locationStack = new Stack<int>();

            //Loop through parentheses string
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    locationStack.Push(i);
                }
                else
                {
                    //Break point of consecutive parentheses, reset CurrentSetLength. Case '())()'
                    if (!locationStack.Any())
                    {
                        currentSetCount = 0;
                    }
                    else
                    {
                        var matchedPosition = locationStack.Pop();
                        var matchedLength = i - matchedPosition + 1;

                        if (!locationStack.Any())
                        {
                            //Ended with a finished a set, update CurrentSetLength for consecutive set length calculation. Case "()()"
                            currentSetCount += matchedLength;
                            result = Math.Max(result, currentSetCount);
                        }
                        else
                        {
                            //Ended with an unfinished set, current i - Stack.Peek (the starting position of the current set) to get CurrentSetLength. Case'(()()'.
                            matchedLength = i - locationStack.Peek();
                            result = Math.Max(result, matchedLength);
                        }
                    }
                }
            }

            return result;
        }
    }
}
