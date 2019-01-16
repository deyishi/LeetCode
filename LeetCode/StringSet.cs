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
                            //Ended with a finished a set, update CurrentSetLength for consecutive set Length calculation. Case "()()"
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


        [Test]
        public void IsOneEditDistance()
        {
            var a = "";
            var b = "";
           var r = IsOneEditDistanceTwoPointer(a,b);
        }

        /// <summary>
        /// Check null, check if Length is same.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool IsOneEditDistance(string s, string t)
        {
            int n = Math.Min(t.Length, s.Length);
            for (var i = 0; i < n;i++) {
                if (s[i] != t[i])
                {
                    if (s.Length == t.Length)
                    {
                        //Replace current char.
                        return s.Substring(i + 1).Equals(t.Substring(i + 1));
                    }
                    if (s.Length < t.Length)
                    {
                        //Insert
                        return s.Substring(i).Equals(t.Substring(i + 1));
                    }

                    //Delete
                    return s.Substring(i + 1).Equals(t.Substring(i));
                }
            }

            //All previous chars are the same, the only possibility is deleting the end char in the longer one of s and t 
            return Math.Abs(s.Length - t.Length) == 1;
        }

        public bool IsOneEditDistanceTwoPointer(string s, string t)
        {
            if (s == null || t == null)
            {
                return s == t;
            }

            if (t.Equals(s))
            {
                return false;
            }

            int m = s.Length;
            int n = t.Length;
            if (Math.Abs(m - n) > 1)
            {
                return false;
            }

            int i = 0;
            int j = 0;
            int count = 0;

            while (i < m && j < n)
            {
                if (s[i] == t[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    count++;
                    if (count > 1)
                    {
                        return false;
                    }

                    if (m == n)
                    {
                        i++;
                        j++;
                    }
                    else if (m > n)
                    {
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                }
            }

            // Check last char.
            count += m - i + n - j;
            return count == 1;
        }
    }
}
