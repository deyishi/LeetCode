using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class PracticeSet3
    {

        [Test]
        public void Divide()
        {

            var r = DivideTwo(-2147483648, -1);
        }

        public int Divide(int dividend, int divisor)
        {

            // Sign
            var negative = (dividend < 0) ^ (divisor < 0);

            //Limit
            long lDividend = Math.Abs((long) dividend);
            long lDivisor = Math.Abs((long) divisor);


            // case 0 
            if (dividend == 0 || lDividend < lDivisor)
            {
                return 0;
            }

            var result = DivideHelper(lDividend, lDivisor);

            if (result > int.MaxValue)
            {
                return negative ? int.MinValue : int.MaxValue;
            }

            return negative ? (int) -result : (int) result;

        }

        public int DivideTwo(int dividend, int divisor)
        {
            // Sign
            var negative = (dividend < 0) ^ (divisor < 0);

            //Limit
            long lDividend = Math.Abs((long)dividend);
            long lDivisor = Math.Abs((long)divisor);

            // case 0 
            if (lDividend == 0 || lDividend < lDivisor)
            {
                return 0;
            }

            long result = 0;
            while (lDividend >= lDivisor)
            {
                long sum = lDivisor;
                long multiple = 1;
                while (sum << 1 <= lDividend)
                {
                    sum  <<= 1;
                    multiple <<= 1;
                }
                lDividend -= sum;
                result += multiple;
            }

            if (result > int.MaxValue)
            {
                return negative ? int.MinValue : int.MaxValue;
            }

            return negative ? (int)-result : (int)result;
        }

        private long DivideHelper(long lDividend, long lDivisor)
        {
            if (lDividend < lDivisor)
            {
                return 0;
            }

            var sum = lDivisor;
            long multiple = 1;

            while (sum << 1 <= lDividend)
            {
                sum = sum << 1;
                multiple += multiple;
            }

            return multiple + DivideHelper(lDividend - sum, lDivisor);
        }

        [Test]
        public void Search()
        {
            var n = new[] {3, 1};
            var r = Search(n, 1);
        }
        public int Search(int[] nums, int target)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            var l = 0;
            var r = nums.Length - 1;

            while (l <= r)
            {

                var m = (l + r) / 2;
                if (target == nums[m]) return m;
                if (nums[m] >= nums[l])
                {
                    if (target >= nums[l] && target <= nums[m])
                    {
                        r = m - 1;
                    }
                    else
                    {
                        l = m + 1;
                    }
                }
                else
                {
                    if (target >= nums[m] && target <= nums[r])
                    {
                        l = m + 1;
                    }
                    else
                    {
                        r = m - 1;
                    }
                }
            }


            return -1;
        }



        public int Read(char[] buf, int n)
        {
            int index = 0;
            char[] r4 = new char[4];
            while (index < n)
            {
                // Place holder
                //int c = read4(r4);
                var c = 4;
                for (int i = 0; i < c && index < n; i++)
                {
                    buf[index++] = r4[i];
                }
                if (c < 4) break;
            }

            return index;
        }

        [Test]
        public void ConvertToTitle()
        {
            var n = 28;
            var r = ConvertToTitle(n);
        }

        public string ConvertToTitle(int n)
        {
            var result = "";
            if (n == 0)
            {
                return result;
            }

            while (n > 0)
            {
                // Start at A so we need n--;
                n--;
                result = (char) ('A' + n % 26) + result;
                n /= 26;
            }

            return result;
        }

        public string ConvertToTitleTwo(int n)
        {
            return n == 0 ? "" : ConvertToTitle(--n / 26) + (char)('A' + (n % 26));
        }

        [Test]
        public void TwoSum()
        {
            var t = new TwoSum();

            t.Add(3);
            t.Add(5);

            var r = t.Find(6);
        }

    }

    public class TwoSum
    {

        private Dictionary<int, int> dict;
        /** Initialize your data structure here. */
        public TwoSum()
        {
            dict = new Dictionary<int, int>();
        }

        /** Add the number to an internal data structure.. */
        public void Add(int number)
        {
            if (dict.ContainsKey(number))
            {
                dict[number]++;
            }
            else
            {
                dict.Add(number, 1);
            }
        }

        /** Find if there exists any pair of numbers which sum is equal to the value. */
        public bool Find(int value)
        {
            foreach (var num in dict.Keys)
            {
                var t = value - num;

                if (t != num && dict.ContainsKey(t) ||  t == num  && dict[t] > 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsValidSudoku(char[,] board)
        {
            if (board == null || board.GetLength(0) != 9 || board.GetLength(1) != 9)
            {
                return false;
            }

            var set = new HashSet<string>();
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var num = board[i, j];
                    if (num != '.')
                    {
                        if (!set.Add(num + " in row " + i) || !set.Add(num + " in col " + j) || !set.Add(num + " in block" + i / 3 + j / 3))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        [Test]
        public void CombinationSum()
        {
           var n = new []{10, 1, 2, 7, 6, 1, 5};
            var t = 8;
            var r = CombinationSum(n, t);
        }
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var result = new List<IList<int>>();

            if (candidates == null || candidates.Length < 1)
            {
                return result;
            }

            var currCombination = new List<int>();
            CombinationSumDFS(candidates, result, currCombination, 0, target);

            return result;
        }

        private void CombinationSumDFS(int[] candidates, List<IList<int>> result, List<int> currCombination, int start, int remain)
        {
            if (remain == 0) result.Add(new List<int>(currCombination));
            else
            {
                for (var i = start; i < candidates.Length; i++)
                {
                    if (i > start && candidates.ToList().IndexOf(candidates[i]) != i)
                    {
                        continue;
                    }

                    if (remain < 0) return;
                    currCombination.Add(candidates[i]);
                    CombinationSumDFS(candidates, result, currCombination, i + 1, remain - candidates[i]); 
                    currCombination.RemoveAt(currCombination.Count - 1);
                }
            }
        }

        [Test]
        public void Permute()
        {
            var n = new[] {1, 2, 3, 4};
            var r = PermuteTwo(n);
        }

        public IList<IList<int>> Permute(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length < 1)
            {
                return result;
            }

            PermuteHelper(nums, new List<int>(), result);
            return result;
        }

        public void PermuteHelper(int[] nums, List<int> curr, List<IList<int>> result)
        {
            if (curr.Count == nums.Length)
            {
                result.Add(new List<int>(curr));
                return;
            }

            foreach (var t in nums)
            {
                if (curr.Contains(t))
                {
                    continue;
                }

                curr.Add(t);
                PermuteHelper(nums, curr, result);
                curr.RemoveAt(curr.Count - 1);
            }
        }


        public IList<IList<int>> PermuteTwo(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length < 1)
            {
                return result;
            }

            PermuteHelperTwo(nums, result, 0);
            return result;
        }

        public void PermuteHelperTwo(int[] nums, List<IList<int>> result, int index)
        {
            if (index == nums.Length)
            {
                result.Add(new List<int>(nums));
                return;
            }

            for (int i = index; i < nums.Length; i++)
            {

                Swap(nums, i, index);
                PermuteHelperTwo(nums, result, index + 1);
                Swap(nums, i, index);
            }
        }

        private void Swap(int[] nums, int m, int n)
        {
            int temp = nums[m];
            nums[m] = nums[n];
            nums[n] = temp;
        }

        [Test]
        public void PermuteUnique()
        {
            var n = new int[] { 1,2,2};

            var r = PermuteUnique(n);
        }
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length < 1)
            {
                return result.ToList();
            }

            PermuteUniqueHelper(nums, result, 0);
            return result.ToList();
        }

        public void PermuteUniqueHelper(int[] nums, List<IList<int>> result, int index)
        {
            if (index == nums.Length)
            {
                result.Add(new List<int>(nums));
                return;
            }

            var alreadySaw = new HashSet<int>();
            for (int i = index; i < nums.Length; i++)
            {
                if (alreadySaw.Contains(nums[i]))
                {
                    continue;
                }

                alreadySaw.Add(nums[i]);
                Swap(nums, i, index);
                PermuteUniqueHelper(nums, result, index + 1);
                Swap(nums, i, index);
            }
        }

        public string GetPermutation(int n, int k)
        {
            if (n == 0)
            {
                return "";
            }

            if (n == 1)
            {
                return n.ToString();
            }
            var nums = new int[n];
            for (var i = 0; i < n; i++)
            {
                nums[i] = i + 1;
            }

            var lastSetPermCount = 1;
            for (var i = 1; i < n; i++)
            {
                lastSetPermCount *= i;
            }

            var index = nums[k / lastSetPermCount];

            //var nk = k - lastSetPermCount * index;
            //for (var i = ) {
            //}

            return "";
        }
    }
}
