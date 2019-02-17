using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class SortSet
    {
        public int MaximumGap(int[] nums)
        {
            Array.Sort(nums);
            int maxGap = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                maxGap = Math.Max(maxGap, nums[i] - nums[i - 1]);
            }

            return maxGap;
        }
    }
}
