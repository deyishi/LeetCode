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
    }
}
