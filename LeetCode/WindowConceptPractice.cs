using System.Collections.Generic;
using NUnit.Framework;

namespace LeetCode
{
    public class WindowConceptPractice
    {
        [Test]
        public void FindSubstring()
        {
            var s = "barfoothefoobarman";

            var n = new[] { "foo", "bar" };

            var r = FindSubstringThree(s, n);
        }
        public IList<int> FindSubstring(string s, string[] words)
        {
            var result = new List<int>();

            if (string.IsNullOrEmpty(s) || words == null || words.Length < 1)
            {
                return result;
            }
            var tmp = new Dictionary<string, int>();
            var map = new Dictionary<string, int>();
            var sLength = s.Length;
            var wordSize = words[0].Length;
            var wordCount = words.Length;
            // Count each word's occurence in words. 
            foreach (var t in words)
            {
                if (map.ContainsKey(t))
                {
                    map[t] += 1;
                }
                else
                {
                    map.Add(t, 1);
                }
            }

            for (var i = 0; i <= sLength - wordCount * wordSize; i++)
            {
                tmp.Clear();
                int j;
                for (j = 0; j < wordCount; j++)
                {
                    var word = s.Substring(i + j * wordSize, wordSize);

                    if (!map.ContainsKey(word))
                    {
                        break;
                    }

                    // Contain the word
                    // Check if the word is seen already
                    if (tmp.ContainsKey(word) && tmp[word] == map[word])
                    {
                        break;
                    }
                    // Increase seen time.
                    if (tmp.ContainsKey(word))
                    {
                        tmp[word] += 1;
                    }
                    else
                    {
                        tmp.Add(word, 1);
                    }
                }
                if (j == wordCount)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        // Travel all the words combinations to maintain a window
        // there are wl(word len) times travel
        // each time, n/wl words, mostly 2 times travel for each word
        // one left side of the window, the other right side of the window
        // so, time complexity O(wl * 2 * N/wl) = O(2N)
        public IList<int> FindSubstringTwo(string s, string[] words)
        {
            var result = new List<int>();

            if (string.IsNullOrEmpty(s) || words == null || words.Length < 1)
            {
                return result;
            }
            var eachWordCount = new Dictionary<string, int>();
            var sLength = s.Length;
            var wordSize = words[0].Length;
            var wordCount = words.Length;

            // Count each word's occurence in words. 
            foreach (var t in words)
            {
                if (eachWordCount.ContainsKey(t))
                {
                    eachWordCount[t] += 1;
                }
                else
                {
                    eachWordCount.Add(t, 1);
                }
            }

            for (var j = 0; j < wordSize; j++)
            {
                var eachWordSeenCount = new Dictionary<string, int>();
                var num = 0;
                for (var i = j; i <= sLength - wordCount * wordSize; i += wordSize)
                {
                    var hasRemoved = false;

                    // Check every wordSize * wordCount range.
                    while (num < wordCount)
                    {
                        var word = s.Substring(i + num * wordSize,  wordSize);
                        if (eachWordCount.ContainsKey(word))
                        {
                            if (eachWordSeenCount.ContainsKey(word))
                            {
                                eachWordSeenCount[word] += 1;
                            }
                            else
                            {
                                eachWordSeenCount.Add(word,1);
                            }

                            if (eachWordSeenCount[word] > eachWordCount[word])
                            {
                                hasRemoved = true;
                                int removeNum = 0;
                                while (eachWordSeenCount[word] > eachWordCount[word])
                                {
                                    var firstWord = s.Substring(i + removeNum * wordSize, wordSize);
                                    eachWordSeenCount[firstWord] -= 1;
                                    removeNum++;
                                }
                                num = num - removeNum + 1; //加 1 是因为我们把当前单词加入到了 HashMap 2 中
                                i = i + (removeNum - 1) * wordSize; //这里依旧是考虑到了最外层的 for 循环，看情况二的解释
                                break;
                            }
                        }
                        else
                        {
                            eachWordSeenCount.Clear();
                            i = i + num * wordSize;
                            num = 0;
                            break;
                        }
                        num++;
                    }
                    if (num == wordCount)
                    {
                        result.Add(i);
                        if (!hasRemoved)
                        {
                            var firstWord = s.Substring(i, wordSize);
                            eachWordSeenCount[firstWord] -= 1;
                            num -= 1;
                        }
                    }
                }
            }

            return result;
        }

        public IList<int> FindSubstringThree(string s, string[] words)
        {
            var result = new List<int>();

            if (string.IsNullOrEmpty(s) || words == null || words.Length < 1)
            {
                return result;
            }

            var eachWordCount = new Dictionary<string, int>();
            var sLength = s.Length;
            var wordSize = words[0].Length;
            var wordCount = words.Length;

            // Count each word's occurence in words. 
            foreach (var t in words)
            {
                if (eachWordCount.ContainsKey(t))
                {
                    eachWordCount[t] += 1;
                }
                else
                {
                    eachWordCount.Add(t, 1);
                }
            }

            for (var i = 0; i < wordSize; ++i)
            {
                var currWindowStart = i;
                var count = 0;
                var eachSeenWordCount = new Dictionary<string, int>();
                for (var j = i; j <= sLength - wordSize; j+=wordSize) {
                    var word = s.Substring(j, wordSize);
                    if (eachWordCount.ContainsKey(word))
                    {
                        // valid word, accumulate results
                        if (eachSeenWordCount.ContainsKey(word))
                        {
                            eachSeenWordCount[word] += 1;
                        }
                        else
                        {
                            eachSeenWordCount.Add(word, 1);
                        }
                        count++;

                        while (eachSeenWordCount[word] > eachWordCount[word])
                        {
                            var firstWord = s.Substring(currWindowStart, wordSize);
                            eachSeenWordCount[firstWord]--;
                            count--;
                            currWindowStart += wordSize;
                        }

                        if (count == wordCount)
                        {
                            result.Add(currWindowStart);
                            var firstWord = s.Substring(currWindowStart, wordSize);
                            eachSeenWordCount[firstWord]--;
                            count--;
                            currWindowStart += wordSize;
                        }
                    }
                    else
                    {
                        eachSeenWordCount.Clear();
                        count = 0;
                        currWindowStart = j + wordSize;
                    }
                }
            }

            return result;
        }
    }
}
