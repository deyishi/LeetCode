using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class TwoPointersSet
    {

        [Test]
        public void LengthOfLongestSubstring()
        {
            var s = "abcabcbb";

            var r = LengthOfLongestSubstring(s);
        }

        /// <summary>
        /// 0.Loop through s.length.
        /// 1.Find a qualified result starting i and j (remember j), j stops at j+1 have duplicate.
        /// 2.Record result (j-i) using Math.Max(result, j-i) for each i.
        /// 3.Increment i and remove s[i] in the map, the result won't count due to Math.Max() during the time while j+1 is still the duplicate.
        /// 4.I is incremented to a point where j+1 is no longer duplicate, then increment j to reach another result. Check result.length at this i.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            var result = int.MinValue;
            var map = new int[256];
            var j = 0;
            for (var i = 0; i < s.Length; i++)
            {
                while (j < s.Length && map[s[j]] < 1)
                {
                    map[s[j]]++;
                    j++;
                }

                result = Math.Max(result, j - i);
                map[s[i]]--;
            }

            return result;
        }


        [Test]
        public void MinWindow()
        {
            var s = "ADOBECODEBANC";
            var t = "ABC";

            var r = MinWindow(s, t);
        }


        /// <summary>
        /// Two char[] array, one for current string char (start empty), one for target string char (scan target string).
        /// Two counters C and K, one for unique char from target string found in current substring (when current string char map[char] == targetmap[char], increase count), one for unique char found in target string (ex. Target ABBC, unique 3).
        /// Loop through string and increment or decrement two counter and left and right point for result substring index.
        /// At each i, if  two counters are equal, there is a window found for this i and record that to result.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public string MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(t))
            {
                return "";
            }

            var map = new int[256];
            var targetMap = new int[256];
            var found = 0;
            var target = 0;
            foreach (var c in t)
            {
                targetMap[c]++;
                if (targetMap[c] == 1)
                {
                    target++;
                }
            }

            int ansStartIndex = -1, ansEndIndex = -1;

            int r = 0;

            for (var l =0; l < s.Length;l++)
            {
                while (r < s.Length && found < target)
                {
                    map[s[r]]++;
                    if (map[s[r]] == targetMap[s[r]])
                    {
                        found++;
                    }
                    r++;
                }

                if (found == target)
                {
                    if (ansStartIndex == -1 || r-l < ansEndIndex-ansStartIndex)
                    {
                        ansEndIndex = r;
                        ansStartIndex = l;
                    }
                }

                map[s[l]]--;
                if (map[s[l]] == targetMap[s[l]] -1)
                {
                    found--;
                }
            }

            if (ansStartIndex == -1)
            {
                return "";
            }

            return s.Substring(ansStartIndex, ansEndIndex-ansStartIndex);
        }

        [Test]
        public void FindSubstring()
        {
            var s = "barfoothefoobarman";
            var word = new[] { "word", "good", "best", "word" };
            var t = FindSubstring(s, word);
        }

        /// <summary>
        /// Create two dict <string, int> for current substring and target, count each word's occurrence.
        /// Loop through each substring starting i, check all the words in i by sub(i + currentWordsCount * wordLength, wordLength), currentWordsCount < wordCounts.
        /// Another loop to loop through each word starting i.
        /// Three case:
        /// Current string doesn't have target words.
        /// Increment counter.
        /// Current string has too many words.
        /// When loop when and counter is same as target, record i.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public IList<int> FindSubstring(string s, string[] words)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(s) || words==null || words.Length == 0)
            {
                return result;
            }

            var wordLength = words[0].Length;
            var wordsCount = words.Length;
            var windowLength = wordLength * wordsCount;

            if (s.Length < windowLength)
            {
                return result;
            }

            var found = new Dictionary<string, int>();
            var target = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!target.ContainsKey(word))
                {
                    target.Add(word, 1);
                }
                else
                {
                    target[word]++;
                }
            }

            for (int i = 0; i <= s.Length - windowLength; i++)
            {
                int currentWordsCount;
                found.Clear();
                for (currentWordsCount = 0; currentWordsCount < wordsCount; currentWordsCount++)
                {
                    var startIndex = i + currentWordsCount * wordLength;
                    var sub = s.Substring(startIndex, wordLength);

                    // Current string doesn't have target words.
                    if (!target.ContainsKey(sub))
                    {
                        break;
                    }


                    // Add to map.
                    if (found.ContainsKey(sub))
                    {
                        found[sub]++;
                    }
                    else
                    {
                        found.Add(sub, 1);
                    }

                    //Current string has too many words.
                    if (found[sub] > target[sub])
                    {
                        break;
                    }
                }

                if (currentWordsCount == wordsCount)
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }
}
