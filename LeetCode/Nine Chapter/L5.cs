using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    class L5
    {

        [Test]
        public void CombinationSum()
        {
            var t = new[] { 2, 3, 6, 7 };
            var n = 7;
            var r = CombinationSum(t, n);
        }
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var result = new List<IList<int>>();
            if (candidates == null || candidates.Length < 1)
            {
                return result;
            }
            Array.Sort(candidates);
            CombinationSumDFS(candidates, result, new List<int>(), 0, target);
            return result;
        }

        private void CombinationSumDFS(int[] candidates, List<IList<int>> result, List<int> currCombination, int start, int target)
        {
            if (target == 0)
            {
                result.Add(new List<int>(currCombination));
                return;
            }

            for (var i = start; i < candidates.Length; i++)
            {


                if (target < 0 || candidates[i] > target)
                {
                    break;
                }
                currCombination.Add(candidates[i]);
                CombinationSumDFS(candidates, result, currCombination, i, target - candidates[i]);
                currCombination.RemoveAt(currCombination.Count - 1);
            }
        }

        [Test]
        public void Partition()
        {

            var s = "cbbd";

            var r = Partition(s);
        }

        public IList<IList<string>> Partition(string s)
        {
            var result = new List<IList<string>>();
            if (string.IsNullOrEmpty(s))
            {
                return result;
            }

            var dp = new bool[s.Length, s.Length];
            for (var i = 0; i < s.Length; i++)
            {
                dp[i, i] = true;
            }

            for (var i = 1; i < s.Length; i++)
            {
                dp[i - 1, i] = s[i - 1] == s[i];
            }
            for (var l = 3; l <= s.Length; l++)
            {
                for (var i = 0; i < s.Length - l + 1; i++)
                {
                    var end = i + l - 1;
                    dp[i, end] = s[i] == s[end] && dp[i + 1, end - 1];
                }
            }

            PartitionHelper(s, new List<string>(), result, 0, dp);

            return result;
        }

        private void PartitionHelper(string s, List<string> list, List<IList<string>> result, int start, bool[,] map)
        {
            if (start == s.Length)
            {
                result.Add(new List<string>(list));
                return;
            }
            for (var i = start; i < s.Length; i++)
            {
                var partition = s.Substring(start, i - start + 1);
                if (map[start, i])
                {
                    list.Add(partition);
                    PartitionHelper(s, list, result, i + 1, map);
                    list.RemoveAt(list.Count - 1);
                }
            }

        }

        private bool IsPalindrome(string partition)
        {
            var start = 0;
            var end = partition.Length - 1;
            while (start < end)
            {
                if (partition[start] != partition[end])
                {
                    return false;
                }

                start++;
                end--;
            }

            return true;
        }

        [Test]
        public void LongestPalindrome()
        {
            var t = "cbbd";

            var r = LongestPalindrome(t);
        }
        public string LongestPalindrome(string s)
        {
            if (s == null || s.Length < 2)
            {
                return s;
            }

            var maxLength = 0;
            var start = 0;
            var dp = new bool[s.Length, s.Length];
            for (var i = 0; i < s.Length; i++)
            {
                dp[i, i] = true;
            }

            for (var i = 1; i < s.Length; i++)
            {
                if (s[i - 1] == s[i])
                {
                    dp[i - 1, i] = true;
                    start = i - 1;
                    maxLength = 2;
                }

            }

            for (var i = s.Length - 3; i >= 0; i--)
            {
                for (var j = i + 2; j < s.Length; j++)
                {
                    if (dp[i + 1, j - 1] && s[i] == s[j])
                    {
                        dp[i, j] = true;

                        if (j - i + 1 > maxLength)
                        {
                            maxLength = j - i + 1;
                            start = i;
                        }
                    }
                }
            }

            return s.Substring(start, maxLength);
        }

        public IList<IList<int>> Permute(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length < 1)
            {
                return result;
            }

            PermuteHelperTwo(nums, result, new List<int>(), new HashSet<int>());
            return result;
        }

        public void PermuteHelperTwo(int[] nums, List<IList<int>> result, List<int> permutation, HashSet<int> set)
        {

            if (permutation.Count == nums.Length)
            {
                result.Add(permutation);
                return;
            }

            for (var i = 0; i < nums.Length; i++)
            {
                if (set.Contains(nums[i]))
                {
                    continue;
                }

                permutation.Add(nums[i]);
                set.Add(nums[i]);
                PermuteHelperTwo(nums, result, permutation, set);
                set.Remove(nums[i]);
                permutation.RemoveAt(permutation.Count - 1);

            }
        }

        [Test]
        public void SolveNQueens()
        {
            var r = SolveNQueens(3);
        }

        public IList<IList<string>> SolveNQueens(int n)
        {
            var result = new List<IList<string>>();
            if (n <= 0)
            {
                return result;
            }

            Helper(result, new List<int>(), n);
            return result;
        }

        /// <summary>
        /// colIndexByRow[0] is the Q col index in row 0.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="colIndexByRow"></param>
        /// <param name="n"></param>
        public void Helper(List<IList<string>> result, List<int> colIndexByRow, int n)
        {
            //colIndexByRow[0] is the Q col index in row 0.
            //colIndexByRow.Count means we have reached a board that we placed n queens into n rows.
            if (colIndexByRow.Count == n)
            {
                result.Add(DrawBoard(colIndexByRow));
                return;
            }

            for (var col = 0; col < n; col++)
            {
                // Can be placed in the column.
                if (!IsValid(colIndexByRow, col))
                {
                    continue;
                }
                colIndexByRow.Add(col);
                Helper(result, colIndexByRow, n);
                colIndexByRow.RemoveAt(colIndexByRow.Count - 1);
            }
        }

        private List<string> DrawBoard(List<int> colIndexPerRow)
        {
            var r = new List<string>();
            for (var row = 0; row < colIndexPerRow.Count; row++)
            {
                var sb = new StringBuilder();
                for (var col = 0; col < colIndexPerRow.Count; col++)
                {
                    sb.Append(colIndexPerRow[row] == col ? "Q" : ".");
                }
                r.Add(sb.ToString());
            }

            return r;
        }

        private bool IsValid(List<int> cols, int nextCol)
        {
            var nextRow = cols.Count;
            for (var existRow = 0; existRow < cols.Count; existRow++)
            {
                var existCol = cols[existRow];
                //check if the column had a queen before.
                if (existCol == nextCol)
                {
                    return false;
                }

                // Total row count is also the index of the next row.
                //check if the 45° diagonal had a queen before.
                if (existRow + existCol == nextRow + nextCol)
                {
                    return false;
                }

                //check if the 135° diagonal had a queen before.
                if (existRow - existCol == nextRow - nextCol)
                {
                    return false;
                }
            }

            return true;
        }


        [Test]
        public void TotalNQueens()
        {
            var r = TotalNQueens(8);
        }
        public int TotalNQueens(int n)
        {
            var count = 0;
            Helper(ref count, new List<int>(), n);
            return count;
        }

        public void Helper(ref int count, List<int> cols, int n)
        {
            if (cols.Count == n)
            {
                count++;
                return;
            }

            for (var col = 0; col < n; col++)
            {
                if (!IsValidMove(cols, col))
                {
                    continue;
                }

                cols.Add(col);
                Helper(ref count, cols, n);
                cols.RemoveAt(cols.Count - 1);
            }
        }

        public bool IsValidMove(List<int> cols, int nextCol)
        {

            var nextRow = cols.Count;
            for (var existRow = 0; existRow < cols.Count; existRow++)
            {
                var existCol = cols[existRow];
                //check if the column had a queen before.
                if (existCol == nextCol)
                {
                    return false;
                }

                // Total row count is also the index of the next row.
                //check if the 45° diagonal had a queen before.
                if (existRow + existCol == nextRow + nextCol)
                {
                    return false;
                }

                //check if the 135° diagonal had a queen before.
                if (existRow - existCol == nextRow - nextCol)
                {
                    return false;
                }
            }

            return true;
        }


        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {

            if (beginWord.Length != endWord.Length || wordList == null || !wordList.Contains(endWord))
            {
                return 0;
            }

            var dict = new HashSet<string>();
            foreach (var word in wordList)
            {
                dict.Add(word);
            }
            var q = new Queue<string>();
            q.Enqueue(beginWord);
            var result = 1;
            var visited = new HashSet<string> { beginWord };

            while (q.Any())
            {
                result++;
                var size = q.Count;
                for (var i = 0; i < size; i++)
                {
                    var curr = q.Dequeue();
                    foreach (var nextWord in FindNextWords(curr, dict))
                    {
                        if (visited.Contains(nextWord))
                        {
                            continue;
                        }

                        if (nextWord == endWord)
                        {
                            return result;
                        }

                        q.Enqueue(nextWord);
                        visited.Add(nextWord);
                    }
                }
            }

            return 0;

        }

        private List<string> FindNextWords(string curr, HashSet<string> wordList)
        {
            var result = new List<string>();
            for (var i = 'a'; i <= 'z'; i++)
            {
                for (var j = 0; j < curr.Length; j++)
                {
                    if (curr[j] == i)
                    {
                        continue;
                    }

                    var nextWord = ReplaceChar(curr, i, j);
                    if (wordList.Contains(nextWord))
                    {
                        result.Add(nextWord);
                    }
                }
            }

            return result;
        }

        private string ReplaceChar(string word, char c, int index)
        {
            var result = word.ToCharArray();
            result[index] = c;
            return new string(result);

        }

        [Test]
        public void FindLadders()
        {
            var b = "hit";
            var e = "cog";
            var l = new[] { "hot", "dot", "dog", "lot", "log", "cog" };
            var r = FindLadders(b, e, l);
        }

        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            var result = new List<IList<string>>();
            if (beginWord.Length != endWord.Length || wordList == null || !wordList.Contains(endWord))
            {
                return result;
            }

            // Put word list into a hash set for lookup efficiency.
            var wordsDict = new HashSet<string>();
            foreach (var word in wordList)
            {
                wordsDict.Add(word);
            }

            var graph = new Dictionary<string, List<string>>();
            var distance = new Dictionary<string, int>();
            //Do BFS to find the distance for each word.
            BFS(beginWord, wordsDict, graph, distance);

            DFS(beginWord, endWord, graph, distance, result, new List<string>());

            return result;
        }

        private void DFS(string beginWord, string endWord, Dictionary<string, List<string>> graph, Dictionary<string, int> distance, List<IList<string>> result, List<string> path)
        {
            path.Add(beginWord);
            if (beginWord == endWord)
            {
                result.Add(new List<string>(path));
            }
            else
            {

                foreach (var nextWord in graph[beginWord])
                {
                    if (distance[beginWord] + 1 == distance[nextWord])
                    {
                        DFS(nextWord, endWord, graph, distance, result, path);
                    }
                }
            }
            path.RemoveAt(path.Count - 1);
        }

        private void BFS(string beginWord, HashSet<string> wordsDict, Dictionary<string, List<string>> graph, Dictionary<string, int> distanceMap)
        {

            var q = new Queue<string>();
            q.Enqueue(beginWord);
            distanceMap.Add(beginWord, 0);
            foreach (var s in wordsDict)
            {
                graph.Add(s, new List<string>());
            }

            if (!graph.ContainsKey(beginWord))
            {
                graph.Add(beginWord, new List<string>());
            }

            while (q.Any())
            {
                var current = q.Dequeue();
                foreach (var nextWord in FindNextWords(current, wordsDict))
                {
                    graph[current].Add(nextWord);
                    if (!distanceMap.ContainsKey(nextWord))
                    {
                        distanceMap.Add(nextWord, distanceMap[current] + 1);
                        q.Enqueue(nextWord);
                    }
                }
            }
        }

    }
}

