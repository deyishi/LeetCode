using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    class StringSet
    {
        [Test]
        public void Test()
        {
            var t = int.Parse("011");
            var s = "(()()";

            var l = new List<string>{ "12:01", "00:13"};
            var r = IsNumber("e9");
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

        public int CompareVersion(string version1, string version2)
        {
            var v1 = version1.Split('.');
            var v2 = version2.Split('.');
            
            var i = 0;
            var j = 0;
            while (i < v1.Length || j < v2.Length)
            {
                // Check case like 1 , 1.1. For shorter version string use 0 here.
                var a  = i < v1.Length ? int.Parse(v1[i]) : 0;
                var b = j < v2.Length ? int.Parse(v2[j]) : 0;

                var r = a.CompareTo(b);
                if (r != 0)
                {
                    return r;
                }

                i++;
                j++;
            }

            return 0;
        }

        public int FindMinDifference(IList<string> timePoints)
        {
            var mark = new bool[60 * 24];
            var max = int.MinValue;
            var min = int.MaxValue;
            foreach (var t in timePoints)
            {
                var a = t.Split(':');
                var hours = int.Parse(a[0]);
                var minutes = int.Parse(a[1]);
                var currentTime = hours * 60 + minutes;
                if (mark[currentTime])
                {
                    return 0;
                }

                min = Math.Min(currentTime, min);
                max = Math.Max(currentTime, max);
                mark[currentTime] = true;
            }

            //Check first point, it can go either way. max to min we use total - max + min.
            var minDiff = Math.Min(max - min, 1440 - max + min);
            var prev = min;

            //search between min + 1 and max time point.
            for (var i = min + 1; i <= max; i++)
            {
                if (mark[i])
                {
                    minDiff = Math.Min(minDiff, i - prev);
                    prev = i;
                }
            }

            return minDiff;
        }

        //65. Valid Number
        //Remove space
        //Number exist, number exist after e, e exist, decimal point exist.
        //    Cannot have.after or multiple .
        //    Cannot have e without number exist or multiple e
        //+ or - sign must either be before any number or be placed after e.
        //    Return if there are number and if there are e then also check number after e.
        public bool IsNumber(string s)
        {
            // Remove space
            s = s.Trim();

            bool decimalPoint = false;
            bool exponentialSymbol = false;
            bool numberSeen = false;
            bool numberAfterE = true; 

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] <= '9' && s[i] >= '0')
                {
                    // Saw a number.
                    numberSeen = true;
                    numberAfterE = true;
                }else if (s[i] == '.')
                {
                    // Cannot have . after or multiple .
                    if (exponentialSymbol || decimalPoint)
                    {
                        return false;
                    }

                    decimalPoint = true;
                }else if (s[i] == 'e')
                {
                    // Cannot have e without number exist or multiple e
                    if (exponentialSymbol || !numberSeen)
                    {
                        return false;
                    }
                    // Saw exponential symbol.
                    numberAfterE = false;
                    exponentialSymbol = true;
                }
                else if(s[i] == '-' || s[i] == '+')
                {
                    // Either first of number can have sign or the place after e.
                    if (i != 0 && s[i-1] != 'e')
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            //Needs to make sure there are numbers and if there are e then number after e.
            return numberSeen && numberAfterE;
        }
    }
}
