﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    class DynamicProgrammingSet
    {
        [Test]
        public void Test()
        {
            var s = "rabbbit";
            var t = "rabbit";
            var r = NumDistinct(s, t);
        }

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
                }
                else if (current < 0)
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

        public int Rob(int[] nums)
        {
            //[1,10,3,1]
            var evenSum = 0;
            var oddSum = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                {
                    evenSum = Math.Max(oddSum + nums[i], nums[i]);
                }
                else
                {
                    oddSum = Math.Max(evenSum + nums[i], nums[i]);
                }
            }

            return Math.Max(evenSum, oddSum);
        }
        // Use dp to track i-2 + current (steal from i-2 house plus current) and i-1 (steal from previous house).
        public int RobTwo(int[] nums)
        {
            if (nums == null)
            {
                return 0;
            }

            if (nums.Length == 1)
            {
                return nums[0];
            }

            var dp = new int[nums.Length];
            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);
            var max = dp[1];
            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
                max = Math.Max(dp[i], max);
            }

            return max;
        }

        public int RobTwoOptimized(int[] nums)
        {
            if (nums == null)
            {
                return 0;
            }

            if (nums.Length == 1)
            {
                return nums[0];
            }

            int prevMax = 0; // Max to the two house ago (i-2)
            int currMax = 0; // Max to last stolen house (i-1)
            foreach (var t in nums)
            {
                var temp = currMax;
                currMax = Math.Max(prevMax + t, currMax);
                prevMax = temp;
            }

            return currMax;
        }

        public int RobCircle(int[] nums)
        {
            if (nums.Length == 1) return nums[0];
            // Check starting first house and skip last house. Check skipping second house and finishing last house.
            return Math.Max(RobCircleHelper(nums, 0, nums.Length - 2), RobCircleHelper(nums, 1, nums.Length - 1));
        }

        public int RobCircleHelper(int[] nums, int start, int end)
        {
            int rob = 0, notRob = 0;
            for (int j = start; j <= end; j++)
            {
                var temp = Math.Max(notRob, rob);
                rob = notRob + nums[j];
                notRob = temp;
            }
            return Math.Max(rob, notRob);
        }


        public int Rob(TreeNode root)
        {
            var result = RobHelper(root);
            return Math.Max(result[0], result[1]);
        }

        public int[] RobHelper(TreeNode root)
        {
            if (root == null)
            {
                return new int[2];
            }

            var left = RobHelper(root.left);
            var right = RobHelper(root.right);

            // Children value.
            var notRob = Math.Max(left[0], left[1]) + Math.Max(right[0], right[1]);

            // Not rob children value with current value. 
            var rob = left[0] + right[0] + root.val;

            return new[] { notRob, rob };
        }

        public int MaximalSquare(char[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return 0;
            }

            int r = matrix.GetLength(0);
            int c = matrix.GetLength(1);
            int[,] dp = new int[r, c];
            int result = 0;

            // Populate top most row.
            for (var i = 0; i < c; i++)
            {
                dp[0, i] = matrix[0, 1] - '0';
                result = Math.Max(result, dp[0, i]);
            }


            // Populate left most col.
            for (var i = 1; i < r; i++)
            {
                dp[i, 0] = matrix[i, 0] - '0';
                result = Math.Max(dp[i, 0], result);
            }


            // dp [i,j] is 1 + checking if [i,j] position's left, right and 45 degree are all 1.
            for (var i = 1; i < r; i++)
            {
                for (var j = 1; j < c; j++)
                {
                    if (matrix[i, j] == '1')
                    {
                        dp[i, j] = Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1])) + 1;
                        result = Math.Max(dp[i, j], result);
                    }
                }
            }

            return result * result;
        }

        //10. Regular Expression Matching
        //dp[s.length + 1, p.length + 1] to track char count.If dp[3, 3] is true, then s[0-2] matches p[0-2].
        //Assuming p won't start with star, check p[1] to p[n] see if match to s[0] for a*, a*b* or a*b*c*... dp[0,j] == dp[0,j-2]
        //If current char match, check previous s and p matches, 45 degree up dp[i - 1, j - 1].
        //If current char '*', ignore char at s[i - 1] if p[j - 2] == '.' or p[j - 2] == s[i - 1], then compare s[0, i - 2] with p[0, j](dp[i, j] = dp[i - 1, j]), else compare two position above treat a* as ''.
        public bool IsMatch(string s, string p)
        {
            // dp[s.length + 1, p.length +1] since we use dp to track char count. If dp[3,3] is true, then s[0-2] matches p[0-2].
            var dp = new bool[s.Length + 1, p.Length + 1];

            // 0 chars matches to 0 chars
            dp[0, 0] = true;
            // Assuming p won't start with star, check p[1] to p[n] see if match to s[0] due to case such as a*, a*b* or a*b*c*... dp[0,j] == dp[0,j-2]
            for (var i = 1; i < dp.GetLength(1); i++)
            {
                if (p[i - 1] == '*')
                {
                    dp[0, i] = dp[0, i - 2];
                }
            }

            for (var sLength = 1; sLength < dp.GetLength(0); sLength++)
            {
                for (var pLength = 1; pLength < dp.GetLength(1); pLength++)
                {
                    if (p[pLength - 1] == s[sLength - 1] || p[pLength - 1] == '.')
                    {
                        // If current char match, check previous s and p matches, 45 degree up in the dp grid[i-1,j-1].
                        dp[sLength, pLength] = dp[sLength - 1, pLength - 1];
                    }
                    else if (p[pLength - 1] == '*')
                    {
                        if (p[pLength - 2] == '.' || p[pLength - 2] == s[sLength - 1])
                        {
                            //Ignore char at s[i - 1] if p[j - 2] == '.' or p[j - 2] == s[i - 1], then compare s[0, i - 2] with p[0, j] : dp[i, j] = dp[i - 1, j].
                            dp[sLength, pLength] = dp[sLength, pLength - 2] || dp[sLength - 1, pLength];
                        }
                        else
                        {
                            // Can only check two positions up for case like acb, aeb*
                            dp[sLength, pLength] = dp[sLength, pLength - 2];
                        }
                    }
                }
            }

            return dp[s.Length, p.Length];
        }

        public bool IsMatchWildCard(string s, string p)
        {
            var dp = new bool[s.Length + 1, p.Length + 1];

            dp[0, 0] = true;

            for (var i = 1; i < dp.GetLength(1); i++)
            {
                if (p[i-1] == '*')
                {
                    dp[0,i] = dp[0,i - 1];
                }
                else
                {
                    break;
                }
            }


            for (var i = 1; i < dp.GetLength(0); i++)
            {
                for (var j = 1; j < dp.GetLength(1); j++)
                {
                    if (p[j - 1] == '?' || p[j - 1] == s[i - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (p[j - 1] == '*')
                    {
                        dp[i, j] = dp[i - 1, j] || dp[i, j - 1];
                    }
                }
            }

            return dp[s.Length, p.Length];
        }

        public int MinCost(int[,] costs)
        {
            if (costs == null || costs.GetLength(0) == 0 || costs.GetLength(1) == 0)
            {
                return 0;
            }

            // If painting current house red, total cost is the min of painting previous house the other color plus cost of painting current house red.
            // Move to next house and update last red house cost with current red house.
            // In the end, min of current house red, blue or green.

            // paintCurrentRed = min(paintPreviousGreen,paintPreviousBlue) + costs[i+1][0]
            int lastR = costs[0, 0];
            int lastG = costs[0, 1];
            int lastB = costs[0, 2];

            for (int i = 1; i < costs.GetLength(0); i ++)
            {
                int currentR = Math.Min(lastB, lastG) + costs[i, 0];
                int currentG = Math.Min(lastB, lastR) + costs[i, 1];
                int currentB = Math.Min(lastR, lastG) + costs[i, 2];

                lastR = currentR;
                lastG = currentG;
                lastB = currentB;
            }

            return Math.Min(Math.Min(lastR, lastB), lastG);
        }

        public int NumWays(int n, int k)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            var sameColor = k;
            var diffColor = k * (k - 1);

            for (var i = 2; i < n; i++)
            {
                var temp = diffColor;
                diffColor = (sameColor + diffColor) * (k - 1);
                sameColor = temp;
            }

            return sameColor + diffColor;
        }

        public int MinDistance(string word1, string word2)
        {
            //DP starts at 0 to include empty staring case. So we use word length + 1.
            var n = word1.Length +1;
            var m = word2.Length +1;

            //Handle word1 or word2 empty. 
            var dp = new int[n, m];

            for (int i = 1; i < n; i++)
            {
                dp[i, 0] = i;
            }

            for (int i = 1; i < m; i++)
            {
                dp[0,i] = i;
            }

         
            for (var i = 1; i < n;i++) {
                for (var j = 1; j < m; j++) {
                    if (word1[i-1] == word2[j-1])
                    {
                        // No action needed if same.
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        // Three actions' state, Replace(i-1,j-1), Insert(i-1, j), Delete (i, j-1)
                        // Take the action state with least operations
                        dp[i, j] = Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1])) + 1;
                    }
                }
            }

            return dp[n, m];
        }

        public int NumSquares(int n)
        {
            var dp = new int[n + 1];
            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = int.MaxValue;
            }
            dp[0] = 0;
            for (var i = 1; i <= n; i++)
            {
                int sqrt = (int)Math.Sqrt(i);

                // If the number is already a perfect square,
                // then dp[number] can be 1 directly. This is
                // just a optimization for this DP solution.
                if (sqrt * sqrt == i)
                {
                    dp[i] = 1;
                }
                else
                {

                    // To get the value of dp[n], we should choose the min
                    // value from:
                    //     dp[n - 1] + 1,
                    //     dp[n - 4] + 1,
                    //     dp[n - 9] + 1,
                    //     dp[n - 16] + 1
                    //     and so on...
                    for (int j = 1; j <= sqrt; j++)
                    {
                        int dif = i - j * j;
                        dp[i] = Math.Min(dp[i], dp[dif] + 1);
                    }
                }
            }

            return dp[n];
        }

        public int MaximalRectangle(char[,] matrix)
        {
            // Height[i] record the current number of continuous of '1' in column i 
            // Left[i] record the left most row index j which satisfies that for any index k from j to i, height[k] >= height[i]
            // Right[i] record the right most row index j which satisfies that for any index k from i to j, height[k] >= height[i]
            // Max area for i is Height[i] * (Right[i] - Left[i] + 1).
            // Need left boundary (j+1) and right boundary (j-1) to update Left[i] and Right[i].
            // Default Right[] int.Max. 
            var m = matrix.GetLength(0);
            var n = matrix.GetLength(1);
            var heights = new int[n];
            var right = new int[n];
            var left = new int[n];
            var maxArea = 0;
            for (var i = 0; i < right.Length; i++)
            {
                right[i] = int.MaxValue;
            }

            // Go through each row
            for (var i = 0; i < m; i++)
            {
                var rb = n - 1;
                for (int j = n-1; j >= 0; j--)
                {
                    if (matrix[i,j] == '1')
                    {
                        heights[j]++;
                        right[j] = Math.Min(right[j], rb);
                    }
                    else
                    {
                        heights[j] = 0;
                        right[j] = n - 1;
                        rb = j - 1;
                    }
                }

                var lb = 0;
                for (var j = 0; j < n;j++) {
                    if (matrix[i, j] == '1')
                    {
                        left[j] = Math.Max(left[j], lb);
                        maxArea = Math.Max((right[j] - left[j] + 1) * heights[j], maxArea);
                    }
                    else
                    {
                        left[j] = 0;
                        lb = j + 1;
                    }
                }
            }

            return maxArea;
        }

        public int StockOne(int[] prices)
        {
            if (prices == null || prices.Length < 1)
            {
                return 0;
            }

            var min = prices[0];
            var max = 0;
            for (var i = 1; i < prices.Length; i++)
            {
                if (prices[i] < min)
                {
                    min = prices[i];
                }
                else
                {
                    max = Math.Max(prices[i] - min, max);
                }
            }

            return max;
        }

        public int StockTwo(int[] prices)
        {
            if (prices == null || prices.Length < 1)
            {
                return 0;
            }

            var profit = 0;
            for (var i = 1; i < prices.Length; i++)
            {
                if (prices[i] > prices[i-1])
                {
                    profit += prices[i] - prices[i - 1];
                }
            }

            return profit;
        }

        public int StockThree(int[] prices)
        {
            if (prices == null || prices.Length < 1)
            {
                return 0;
            }

            var k = 3;
            var dp = new int[k, prices.Length];


            for (var t = 1; t < k; t++) {
                for (var d = 1; d<prices.Length;d++)
                {

                    var noTransaction = dp[t, d - 1];

                    var maxProfitToToday = 0;
                    for (var cd = 0; cd < d; cd++)
                    {
                        maxProfitToToday = Math.Max(prices[d] - prices[cd] + dp[t - 1, cd], maxProfitToToday);
                    }

                    dp[t, d] = Math.Max(noTransaction, maxProfitToToday);
                }
            }

            return dp[k-1, prices.Length-1];
        }

        public int StockThreeOptimized(int[] prices)
        {
            if (prices == null || prices.Length < 1)
            {
                return 0;
            }

            var k = 3;
            var dp = new int[k, prices.Length];


            for (var t = 1; t < k; t++)
            {
                var maxDiff = -prices[0];
                for (var d = 1; d < prices.Length; d++)
                {
                    dp[t, d] = Math.Max(dp[t, d - 1], prices[d] + maxDiff);
                    maxDiff = Math.Max(maxDiff, dp[t - 1, d] - prices[d]);
                }
            }

            return dp[k - 1, prices.Length - 1];
        }

        public int StockWithCooldown(int[] prices)
        {
            if (prices == null || prices.Length < 2)
            {
                return 0;
            }

            var day = prices.Length;

            var buy = new int[day];
            var sell = new int[day];

            buy[0] = -prices[0];
            buy[1] = Math.Max(-prices[0], -prices[1]);
            sell[1] = Math.Max(buy[0] + prices[1], 0);
            for (var i = 2; i < day; i++)
            {
                var price = prices[i];
                buy[i] = Math.Max(buy[i - 1], sell[i - 2] - price);
                sell[i] = Math.Max(sell[i - 1], buy[i - 1] + price);
            }

            return sell[day - 1];
        }


        public bool IsInterleave(string s1, string s2, string s3)
        {
            if (s1.Length + s2.Length != s3.Length)
            {
                return false;
            }

            var dp = new bool[s1.Length + 1, s2.Length + 1];
            dp[0, 0] = true;
            for (var i = 1; i <= s2.Length; i++)
            {
                dp[0, i] = s2[i - 1] == s3[i - 1] && dp[0, i - 1];
            }

            for (var i = 1; i <= s1.Length; i++)
            {
                dp[i, 0] = s1[i - 1] == s3[i - 1] && dp[i - 1, 0];
            }

            for (var i = 1; i <= s1.Length; i++)
            {
                for (var j = 1; j <= s2.Length; j++)
                {

                    dp[i, j] = dp[i - 1, j] && s1[i - 1] == s3[i + j - 1] ||
                               dp[i, j - 1] && s2[j - 1] == s3[i + j - 1];
                }
            }

            return dp[s1.Length, s2.Length];
        }

        public int NumDistinct(string s, string t)
        {
            var dp = new int[t.Length + 1, s.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                dp[0, i] = 1;
            }

            for (int i = 1; i <= t.Length; i++)
            {
                for (int j = 1; j <= s.Length; j++)
                {
                    if (t[i-1] == s[j-1])
                    {
                        // Case rab and rabb, dp[i-1, j-1] count subsequence using [ra] -> [rab], then [b] -> [b] at later index
                        // dp[i,j-1] count subsequence using [rab] -> [rab], then remove b at later index..
                        dp[i, j] = dp[i-1, j-1] + dp[i, j-1];
                    }
                    else
                    {
                        //If t[i-1] != s[j-1] doesn't match, we have the same number of distinct subsequence as we had without adding the new char.
                        dp[i, j] = dp[i, j - 1];
                    }
                }
            }

            return dp[t.Length, s.Length];
        }
    }
}
