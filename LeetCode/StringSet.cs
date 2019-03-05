using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.Nine_Chapter;
using NUnit.Framework;
using NUnit.Framework.Api;

namespace LeetCode
{
    class StringSet
    {
        [Test]
        public void Test()
        {


            var s = "adcba";
            var t = "ABC";

            var r = CanConstruct("a", "b");
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
        //68. Text Justification
        //Find how many words can fit into each line.
        //    Find how many spaces are needed for each line.
        //    Line length to track word length plus one space and next line start word index.
        //    Loop through words until line length is over max line length which means we added enough words, then next line start word index should be pointting at the index of this line's last word plus one.
        //    Add this line's first word. Then find number of separators number by this line start word index minutes next line start word index - 1. ("1.A B 2.C") Line start index is 0, next line start index is 2, 2-0-1, means we need one separator for current line.
        //    If separator == 0 means this line has only one word, just add the space.
        //    If next line start index equals words count, means we can fit the rest of the words into this line.
        //    If separator != 0 and have words remaining, we need to find out how many spaces to put between each word. Use max length - line length (contains words length + 1 space for each word) / separator, this gives a round down count of spaces needed between each word.
        //    For case that(max length is 16, total words length is 9 with 3 words and 2 separator), we will need 7 spaces.But(16 - 9)/2 is 3, we are missing one space so we need(16-9)%2 to find the remaining spaces and evenly distribute them for each line separator, so first line separator may get 4 then second get 3.
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            var result = new List<string>();

            var currentLineStartWordIndex = 0;
            while (currentLineStartWordIndex < words.Length)
            {
                var currentLineLength = words[currentLineStartWordIndex].Length;
                var nextLineStartWordIndex = currentLineStartWordIndex + 1;

                while (nextLineStartWordIndex < words.Length )
                {
                    if(currentLineLength + words[nextLineStartWordIndex].Length + 1 > maxWidth) {
                        break;
                    }
                    currentLineLength += words[nextLineStartWordIndex].Length + 1;
                    nextLineStartWordIndex++;
                }

                var currentLine = new StringBuilder();
                currentLine.Append(words[currentLineStartWordIndex]);
                var separators = nextLineStartWordIndex - currentLineStartWordIndex - 1;
                if (nextLineStartWordIndex == words.Length || separators == 0)
                {
                    for (var i = currentLineStartWordIndex + 1; i < nextLineStartWordIndex; i++)
                    {
                        currentLine.Append(" " + words[i]);
                    }

                    for (var i = currentLine.Length; i < maxWidth; i++)
                    {
                        currentLine.Append(" ");
                    }
                }
                else
                {
                    var separatorSpaces = (maxWidth - currentLineLength) / separators;
                    var r = (maxWidth - currentLineLength) % separators;

                    for (var i = currentLineStartWordIndex + 1; i < nextLineStartWordIndex; i++) {
                        for (var j = separatorSpaces; j > 0; j--)
                        {
                            currentLine.Append(" ");
                        }

                        if (r > 0)
                        {
                            currentLine.Append(" ");
                            r--;
                        }

                        currentLine.Append(" " + words[i]);
                    }
                }
                result.Add(currentLine.ToString());
                currentLineStartWordIndex = nextLineStartWordIndex;
            }

            return result;
        }
        public IList<IList<string>> GroupStrings(string[] strings)
        {
            var map = new Dictionary<string, IList<string>>();
            foreach (var s in strings)
            {
                string key = GetShiftKey(s);

                if (!map.TryGetValue(key, out var list))
                {
                    list = new List<string>();
                    map.Add(key,list);
                }
                list.Add(s);

            }

            return map.Values.ToList();
        }

        private string GetShiftKey(string s)
        {
            string shiftKey = "";
            for (var i = 1; i < s.Length; i++)
            {
                 shiftKey += (s[i] - s[i - 1] + 26) % 26;
            }

            return shiftKey;
        }

        public bool IsIsomorphic(string s, string t)
        {
            // Map char from s to char from t using two int[256] array.
            int[] m1 = new int[256];
            int[] m2 = new int[256];

            // Loop through char of both s and t, compare their value in the map, if they are different then they are not 1 to 1 mapping.
            for (int i = 0; i < s.Length; i++)
            {
                if (m1[s[i]] != m2[t[i]])
                {
                    return false;
                }

                m1[s[i]] = i;
                m2[t[i]] = i;
            }

            return true;
        }

        // Use two hash map patternToWord and wordToPattern to check pattern and str one to one match relationship.
        public bool WordPattern(string pattern, string str)
        {
            var words = str.Split(' ');
            if (words.Length != pattern.Length)
            {
                return false;
            }

            var patternToWord = new Dictionary<char, string>();
            var wordToPattern = new Dictionary<string, char>();

            for (int i = 0; i < pattern.Length; i++)
            {
                var c = pattern[i];
                var word = words[i];
                if (!patternToWord.ContainsKey(c) && !wordToPattern.ContainsKey(word))
                {
                    patternToWord.Add(c, word);
                    wordToPattern.Add(word, c);
                }else if (patternToWord.ContainsKey(c) && !patternToWord[c].Equals(word))
                {
                    return false;
                }else if (wordToPattern.ContainsKey(word) && wordToPattern[word] != c)
                {
                    return false;
                }
            }

            return true;
        }

        public IList<string> GeneratePossibleNextMoves(string s)
        {
            var a = s.ToCharArray();
            var result = new List<string>();
            for (var i = 1; i < a.Length; i ++) {
                if (a[i] == a[i-1])
                {
                    if (a[i] == '+')
                    {
                        a[i] = a[i - 1] = '-';
                        result.Add(new string(a));
                        a[i] = a[i - 1] = '+';

                    }
                    else
                    {
                        a[i] = a[i - 1] = '+';
                        result.Add(new string(a));
                        a[i] = a[i - 1] = '-';
                    }
                }
            }

            return result;
        }

        private Dictionary<string, bool> _scrambleMap = new Dictionary<string, bool>();
        public bool IsScramble(string s1, string s2)
        {

            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) || s1.Length != s2.Length)
            {
                return false;
            }

            var key = s1 + "#" + s2;
            if (_scrambleMap.ContainsKey(key))
            {
                return _scrambleMap[key];
            }

            if (s1.Length == 1 && s1.Equals(s2))
            {
                return true;
            }

            if (new string(s1.OrderBy(x => x).ToArray()) != new string(s2.OrderBy(x => x).ToArray()))
            {
                return false;
            }

            var n = s1.Length;
            for (var i = 1 ; i < s1.Length; i++)
            {
                var s1Left = s1.Substring(0, i);
                var s1Right = s1.Substring(i, n- i);
                var s2Left = s2.Substring(0, i);
                var s2Right = s2.Substring(i, n- i);
                var a = s2.Substring(n - i, i);
                var b = s2.Substring(0, n - i);
                if (IsScramble(s1Left, s2Left) && IsScramble(s1Right, s2Right) ||
                    IsScramble(s1Left,a) && IsScramble(s1Right,b))
                {
                    _scrambleMap.Add(key, true);
                    return true;
                }
            }

            _scrambleMap.Add(key, false);
            return false;
        }

        public int Read(char[] buf, int n)
        {
            int index = 0;
            char[] r4 = new char[4];
            int c = 4;
            while (c == 4 && index < n)
            {
                c = Read4(r4);
                for (int i = 0; i < c && index < n; i++)
                {
                    buf[index++] = r4[i];
                }
            }

            return index;
        }


        private int bp = 0;
        private int bl = 0;
        private char[] buff = new char[4];
      
        public int ReadTwo(char[] buf, int n)
        {
            // buff pointer: index of outputted char in current Read4
            // buff len: len of current Read4, if 0 no more reading.
            // index: index of populated outputted char array.
            // buff pointer is incremented at the same time as index.
            // Keep doing Read4 until we finished reading file or reached n.
            // Whatever is read is gone. 'abc', calling read 3 returns 'abc' then calling read 1 returns nothing.
            // At read 3, bi is at 3 and len is at 3. At read 1, we call Read4 again since we have consumed all the read chars and need to read more from the file.
            int i = 0;
            while (i < n)
            {
                if (bp == bl)
                {
                    // reset buff pointer
                    bp = 0;
                    bl = Read4(buff);
                    if (bl == 0)
                    {
                        break;
                    }
                }

                buf[i++] = buff[bp++];
            }

            return i;
        }


        private int Read4(char[] r4)
        {
            return 4;
        }

        public char[] RemoveChar(char[] s, char c)
        {
            int cIndex = 0;
            for (var i = 0; i < s.Length; i++) {
                if (s[i] != c)
                {
                    var temp = s[cIndex];
                    s[cIndex] = s[i];
                    s[i] = temp;
                    cIndex++;
                }
            }

            while (cIndex < s.Length)
            {
                s[cIndex++] = '0';
            }
            return s;
        }

        public string ShortestPalindrome(string s)
        {

            // Find break point when s is not a palindrome. "aab", position 2, since aa is palindrome.
            // "aacbcaaa", position 7, since "aacbcaa" is palindrome.
            int j = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == s[j])
                {
                    j++;
                }
            }

            if (j == s.Length)
            {
                return s;
            }

            string suffix = s.Substring(j);
            var suffixArray = suffix.ToCharArray();
            Array.Reverse(suffixArray);
            string prefix = new string(suffixArray);
            string mid = ShortestPalindrome(s.Substring(0, j));
            string ans = prefix + mid + suffix;
            return ans;
        }

        public bool CanConstruct(string ransomNote, string magazine)
        {
            int[] available = new int[26];

            foreach (var c in magazine)
            {
                available[c - 'a']++;
            }

            foreach (var c in ransomNote)
            {
                available[c - 'a']--;
                if (available[c - 'a'] < 0)
                {
                    return false;
                }
            }

            return true;
        }


    }
}
