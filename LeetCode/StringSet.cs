﻿using System;
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


        [Test]
        public void ReverseWords()
        {
            var s = " 1";
            var r = ReverseWordsInPlace(s);
        }

        /// <summary>
        /// Split string by space.
        /// Loop from the last word and append to string builder.
        /// Condition check if word is empty then append space if word isn't the first in string builder.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReverseWords(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return "";
            }

            var a = s.Split(' ');
            var sb = new StringBuilder();
            for (var i = a.Length - 1; i >= 0; i--)
            {
                // Handle empty string before space.
                if (!a[i].Equals(""))
                {
                    //If not first word, append space.
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }

                    // Append word.
                    sb.Append(a[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// In place solution: reverse the whole string, then reverse word. For c
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReverseWordsInPlace(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            var a = s.ToCharArray();

            ReverseString(a, 0, s.Length - 1);

            int i = 0;
            int spaceIndex = 0;
            while (i < a.Length)
            {
                if (a[i] != ' ')
                {
                    if (spaceIndex != 0)
                    {
                        a[spaceIndex] = ' ';
                        spaceIndex++;
                    }

                    int j = i;
                    while (j < a.Length && a[j] != ' ')
                    {
                        a[spaceIndex++] = a[j++];
                    }

                    ReverseString(a, i, j - 1);
                    i = j;
                }

                i++;
            }

            return new string(a).Remove(i, s.Length - i);
        }

        public void ReverseString(char[] s, int start, int end)
        {
            while (start < end)
            {
                var temp = s[start];
                s[start] = s[end];
                s[end] = temp;
                start++;
                end--;
            }
        }
    }
}
