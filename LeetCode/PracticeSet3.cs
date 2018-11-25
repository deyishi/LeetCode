using System;
using System.Collections.Generic;
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

        public int[] SearchRange(int[] nums, int target)
        {
            return null;
        }
    }
}
