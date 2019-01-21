using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    class DynamicProgrammingSet
    {


        /// <summary>
        ///       1
        ///    2     3
        /// Find min from bottom to top.
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            if (triangle == null || triangle.Count == 0 || triangle[0].Count == 0)
            {
                return 0;
            }

            for (var level = triangle.Count - 2; level >= 0; level--)
            {
                for (var i = 0; i < triangle[level].Count; i++)
                {
                    triangle[level][i] = Math.Min(triangle[level + 1][i], triangle[level + 1][i + 1]) + triangle[level][i];
                }
            }

            return triangle[0][0];
        }


        /// <summary>
        /// Check all the substring of s, see if they are in the words. dp[substring length 2] is true if 0-2 or 0-1 and 1-2 in the words.
        /// Record the state at 0-1 and check 1-2.
        /// State[j] and check string[i, i-j], if true then state[i] is true.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <returns></returns>
        public bool WordBreak(string s, IList<string> wordDict)
        {
            if (string.IsNullOrEmpty(s) || wordDict == null || wordDict.Count == 0)
            {
                return false;
            }


            var set = new HashSet<string>();
            foreach (var w in wordDict)
            {
                set.Add(w);
            }
            var dp = new bool[s.Length + 1];
            dp[0] = true;
            // I is the substring length.
            for (int i = 1; i <= s.Length; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (dp[j] && set.Contains(s.Substring(j, i - j)))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }

        public IList<string> WordBreakTwo(string s, IList<string> wordDict)
        {
            if (string.IsNullOrEmpty(s) || wordDict == null || wordDict.Count == 0)
            {
                return new List<string>();
            }

            var found = new Dictionary<string, List<string>>();

            var set = new HashSet<string>();
            foreach (var word in wordDict)
            {
                set.Add(word);
            }

            return WordBreakHelper(s, set, found);

        }

        private List<string> WordBreakHelper(string s, HashSet<string> wordDict, Dictionary<string, List<string>> found)
        {
            if (found.ContainsKey(s))
            {
                return found[s];
            }

            //Reaches end
            if (s.Length == 0)
            {
                return null;
            }

            var res = new List<string>();
            for (int i = 1; i <= s.Length; i++)
            {
                var word = s.Substring(0, i);
                if (wordDict.Contains(word))
                {
                    var partResults = WordBreakHelper(s.Substring(i), wordDict, found);

                    if (partResults == null)
                    {
                        res.Add(word);
                    }
                    else
                    {
                        foreach (var part in partResults)
                        {
                            res.Add(word + " " + part);
                        }
                    }
                }
            }

            found.Add(s, res);
            return res;
        }

        /// <summary>
        /// DP track each index's max and min since number can be negative.
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MaxProduct(int[] nums)
        {
            int[] max = new int[nums.Length];
            int[] min = new int[nums.Length];

            min[0] = max[0] = nums[0];

            int result = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                var current = nums[i];
                if (current > 0)
                {
                    max[i] = Math.Max(current, max[i - 1] * current);
                    min[i] = Math.Min(current, min[i - 1] * current);
                }else if (current < 0)                     
                {                                          
                    max[i] = Math.Max(current, min[i - 1] * current);
                    min[i] = Math.Min(current, max[i - 1] * current);
                }
                else
                {
                    max[i] = current;
                    min[i] = current;
                }

                result = Math.Max(result, max[i]);
            }

            return result;
        }
    }
}
